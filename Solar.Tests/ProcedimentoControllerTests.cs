using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Solar.API.Controllers;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Procedimento;
using Solar.Application.Interfaces;

namespace Solar.Tests;

public class ProcedimentoControllerTests
{
    private readonly Mock<IProcedimentoServices> _mockProcedimentoService;
    private readonly ProcedimentosController _controller;

    public ProcedimentoControllerTests()
    {
        _mockProcedimentoService = new Mock<IProcedimentoServices>();
        _controller = new ProcedimentosController(_mockProcedimentoService.Object);
    }

    [Fact]
    public async Task GetProcedimentoById_ComIdValido_RetornaOkComProcedimento()
    {
        // Arrange
        var idProcedimento = Guid.NewGuid();
        var expectedProcedimentoResponse = new ProcedimentoResponse
        {
            Id = idProcedimento,
            Descricao = "Instalação de Painéis Solares",
            Concluido = false,
            DataConclusao = null,
            ProjetoId = Guid.NewGuid()
        };

        _mockProcedimentoService
            .Setup(s => s.GetById(idProcedimento))
            .ReturnsAsync(expectedProcedimentoResponse);

        // Act
        var result = await _controller.GetById(idProcedimento);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProcedimento = Assert.IsType<ProcedimentoResponse>(okResult.Value);
        Assert.Equal(idProcedimento, returnedProcedimento.Id);
        Assert.Equal("Instalação de Painéis Solares", returnedProcedimento.Descricao);
    }

    [Fact]
    public async Task GetProcedimentoById_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var mensagemErro = "Procedimento não encontrado";

        _mockProcedimentoService
            .Setup(s => s.GetById(idInexistente))
            .ThrowsAsync(new NotFoundException(mensagemErro));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.GetById(idInexistente)
        );

        Assert.Equal(mensagemErro, exception.Message);
    }

    [Fact]
    public async Task GetAll_RetornaListaDeProcedimentos()
    {
        // Arrange
        var procedimentosEsperados = new List<ProcedimentoResponse>
        {
            new ProcedimentoResponse { Id = Guid.NewGuid(), Descricao = "Procedimento 1", Concluido = false, DataConclusao = null, ProjetoId = Guid.NewGuid() },
            new ProcedimentoResponse { Id = Guid.NewGuid(), Descricao = "Procedimento 2", Concluido = true, DataConclusao = DateTime.Now, ProjetoId = Guid.NewGuid() },
            new ProcedimentoResponse { Id = Guid.NewGuid(), Descricao = "Procedimento 3", Concluido = false, DataConclusao = null, ProjetoId = Guid.NewGuid() }
        };

        _mockProcedimentoService
            .Setup(s => s.GetProcedimentos())
            .ReturnsAsync(procedimentosEsperados);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProcedimentos = Assert.IsType<List<ProcedimentoResponse>>(okResult.Value);
        Assert.Equal(3, returnedProcedimentos.Count);
        Assert.Equal("Procedimento 1", returnedProcedimentos[0].Descricao);
    }

    [Fact]
    public async Task Create_ComDadosValidos_RetornaCreatedAtAction()
    {
        // Arrange
        var projetoId = Guid.NewGuid();
        var createRequest = new CreateProcedimentoRequest
        {
            Descricao = "Novo Procedimento",
            ProjetoId = projetoId
        };

        var procedimentoCriado = new CreateProcedimentoResponse
        {
            Id = Guid.NewGuid(),
            Descricao = "Novo Procedimento",
            Concluido = false,
            DataConclusao = null,
            ProjetoId = projetoId
        };

        _mockProcedimentoService
            .Setup(s => s.Create(createRequest))
            .ReturnsAsync(procedimentoCriado);

        // Act
        var mockValidator = new Mock<FluentValidation.IValidator<CreateProcedimentoRequest>>();
        
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateProcedimentoRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await _controller.Create(createRequest, mockValidator.Object);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(ProcedimentosController.GetById), createdResult.ActionName);
        Assert.NotNull(createdResult.Value);
        var returnedProcedimento = Assert.IsType<CreateProcedimentoResponse>(createdResult.Value);
        Assert.Equal("Novo Procedimento", returnedProcedimento.Descricao);
    }

    [Fact]
    public async Task Update_ComIdValidoEDadosValidos_RetornaOkComProcedimentoAtualizado()
    {
        // Arrange
        var idProcedimento = Guid.NewGuid();
        var projetoId = Guid.NewGuid();
        var updateRequest = new UpdateProcedimentoRequest
        {
            Descricao = "Procedimento Atualizado",
            Concluido = true
        };

        var procedimentoAtualizado = new UpdateProcedimentoResponse
        {
            Id = idProcedimento,
            Descricao = "Procedimento Atualizado",
            Concluido = true,
            DataConclusao = DateTime.Now,
            ProjetoId = projetoId
        };

        _mockProcedimentoService
            .Setup(s => s.Update(idProcedimento, updateRequest))
            .ReturnsAsync(procedimentoAtualizado);

        // Act
        var result = await _controller.Update(idProcedimento, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProcedimento = Assert.IsType<UpdateProcedimentoResponse>(okResult.Value);
        Assert.Equal(idProcedimento, returnedProcedimento.Id);
        Assert.Equal("Procedimento Atualizado", returnedProcedimento.Descricao);
    }

    [Fact]
    public async Task Update_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var updateRequest = new UpdateProcedimentoRequest
        {
            Descricao = "Procedimento Atualizado",
            Concluido = true
        };

        _mockProcedimentoService
            .Setup(s => s.Update(idInexistente, updateRequest))
            .ThrowsAsync(new NotFoundException("Procedimento não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Update(idInexistente, updateRequest)
        );
    }

    [Fact]
    public async Task Delete_ComIdValido_RetornaOkComProcedimentoDeletado()
    {
        // Arrange
        var idProcedimento = Guid.NewGuid();
        var procedimentoDeletado = new ProcedimentoResponse
        {
            Id = idProcedimento,
            Descricao = "Procedimento a Deletar",
            Concluido = false,
            DataConclusao = null,
            ProjetoId = Guid.NewGuid()
        };

        _mockProcedimentoService
            .Setup(s => s.RemoveAsync(idProcedimento))
            .ReturnsAsync(procedimentoDeletado);

        // Act
        var result = await _controller.Delete(idProcedimento);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProcedimento = Assert.IsType<ProcedimentoResponse>(okResult.Value);
        Assert.Equal(idProcedimento, returnedProcedimento.Id);
        Assert.Equal("Procedimento a Deletar", returnedProcedimento.Descricao);
    }

    [Fact]
    public async Task Delete_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();

        _mockProcedimentoService
            .Setup(s => s.RemoveAsync(idInexistente))
            .ThrowsAsync(new NotFoundException("Procedimento não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Delete(idInexistente)
        );
    }
}

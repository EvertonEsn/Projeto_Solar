using Microsoft.AspNetCore.Mvc;
using Moq;
using Solar.API.Controllers;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Projeto;
using Solar.Application.Interfaces;

namespace Solar.Tests;

public class ProjetoControllerTests
{
    private readonly Mock<IProjetoServices> _mockProjetoService;
    private readonly ProjetosController _controller;

    public ProjetoControllerTests()
    {
        _mockProjetoService = new Mock<IProjetoServices>();
        _controller = new ProjetosController(_mockProjetoService.Object);
    }

    [Fact]
    public async Task GetProjetoById_ComIdValido_RetornaOkComProjeto()
    {
        // Arrange
        var idProjeto = Guid.NewGuid();
        var expectedProjetoResponse = new ProjetoResponse
        {
            Id = idProjeto,
            Nome = "Projeto Solar Residencial",
            Localizacao = "São Paulo, SP",
            DataInicio = DateTime.Now,
            DataFinal = DateTime.Now.AddMonths(3),
            ValorTotal = 50000m,
            ClienteId = Guid.NewGuid(),
            LiderTecnicoId = Guid.NewGuid()
        };

        _mockProjetoService
            .Setup(s => s.GetById(idProjeto))
            .ReturnsAsync(expectedProjetoResponse);

        // Act
        var result = await _controller.GetById(idProjeto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProjeto = Assert.IsType<ProjetoResponse>(okResult.Value);
        Assert.Equal(idProjeto, returnedProjeto.Id);
        Assert.Equal("Projeto Solar Residencial", returnedProjeto.Nome);
    }

    [Fact]
    public async Task GetProjetoById_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var mensagemErro = "Projeto não encontrado";

        _mockProjetoService
            .Setup(s => s.GetById(idInexistente))
            .ThrowsAsync(new NotFoundException(mensagemErro));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.GetById(idInexistente)
        );

        Assert.Equal(mensagemErro, exception.Message);
    }

    [Fact]
    public async Task GetAll_RetornaListaDeProjetos()
    {
        // Arrange
        var projetosEsperados = new List<ProjetoResponse>
        {
            new ProjetoResponse { Id = Guid.NewGuid(), Nome = "Projeto 1", Localizacao = "Local 1", DataInicio = DateTime.Now, DataFinal = DateTime.Now.AddMonths(1), ValorTotal = 10000m, ClienteId = Guid.NewGuid(), LiderTecnicoId = Guid.NewGuid() },
            new ProjetoResponse { Id = Guid.NewGuid(), Nome = "Projeto 2", Localizacao = "Local 2", DataInicio = DateTime.Now, DataFinal = DateTime.Now.AddMonths(2), ValorTotal = 20000m, ClienteId = Guid.NewGuid(), LiderTecnicoId = Guid.NewGuid() },
            new ProjetoResponse { Id = Guid.NewGuid(), Nome = "Projeto 3", Localizacao = "Local 3", DataInicio = DateTime.Now, DataFinal = DateTime.Now.AddMonths(3), ValorTotal = 30000m, ClienteId = Guid.NewGuid(), LiderTecnicoId = Guid.NewGuid() }
        };

        _mockProjetoService
            .Setup(s => s.GetProjetos())
            .ReturnsAsync(projetosEsperados);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProjetos = Assert.IsType<List<ProjetoResponse>>(okResult.Value);
        Assert.Equal(3, returnedProjetos.Count);
        Assert.Equal("Projeto 1", returnedProjetos[0].Nome);
    }

    [Fact]
    public async Task Create_ComDadosValidos_RetornaCreatedAtAction()
    {
        // Arrange
        var createRequest = new CreateProjetoRequest
        {
            Nome = "Novo Projeto",
            Localizacao = "Rio de Janeiro, RJ",
            DataInicio = DateTime.Now,
            ValorTotal = 75000m,
            ClienteId = Guid.NewGuid(),
            LiderTecnicoId = Guid.NewGuid()
        };

        var projetoCriado = new CreateProjetoResponse
        {
            Id = Guid.NewGuid(),
            Nome = "Novo Projeto",
            Localizacao = "Rio de Janeiro, RJ",
            DataInicio = DateTime.Now,
            ValorTotal = 75000m,
            ClienteId = Guid.NewGuid(),
            LiderTecnicoId = Guid.NewGuid()
        };

        _mockProjetoService
            .Setup(s => s.Create(createRequest))
            .ReturnsAsync(projetoCriado);

        // Act
        var mockValidator = new Mock<FluentValidation.IValidator<CreateProjetoRequest>>();
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateProjetoRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await _controller.Create(createRequest, mockValidator.Object);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(ProjetosController.GetById), createdResult.ActionName);
        Assert.NotNull(createdResult.Value);
        var returnedProjeto = Assert.IsType<CreateProjetoResponse>(createdResult.Value);
        Assert.Equal("Novo Projeto", returnedProjeto.Nome);
    }

    [Fact]
    public async Task Update_ComIdValidoEDadosValidos_RetornaOkComProjetoAtualizado()
    {
        // Arrange
        var idProjeto = Guid.NewGuid();
        var updateRequest = new UpdateProjetoRequest
        {
            Nome = "Projeto Atualizado",
            Localizacao = "Belo Horizonte, MG",
            DataInicio = DateTime.Now,
            DataFinal = DateTime.Now.AddMonths(12),
            ValorTotal = 100000m,
            LiderTecnicoId = Guid.NewGuid()
        };

        var projetoAtualizado = new UpdateProjetoResponse
        {
            Id = idProjeto,
            Nome = "Projeto Atualizado",
            Localizacao = "Belo Horizonte, MG",
            DataInicio = DateTime.Now,
            DataFinal = DateTime.Now.AddMonths(12),
            ValorTotal = 100000m,
            LiderTecnicoId = Guid.NewGuid()
        };

        _mockProjetoService
            .Setup(s => s.Update(idProjeto, updateRequest))
            .ReturnsAsync(projetoAtualizado);

        // Act
        var mockValidator = new Mock<FluentValidation.IValidator<UpdateProjetoRequest>>();
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateProjetoRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var result = await _controller.Update(idProjeto, updateRequest, mockValidator.Object);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProjeto = Assert.IsType<UpdateProjetoResponse>(okResult.Value);
        Assert.Equal(idProjeto, returnedProjeto.Id);
        Assert.Equal("Projeto Atualizado", returnedProjeto.Nome);
    }

    [Fact]
    public async Task Update_ComIdInvalido_RetornaNotFound()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var updateRequest = new UpdateProjetoRequest
        {
            Nome = "Projeto Atualizado",
            Localizacao = "Belo Horizonte, MG",
            DataInicio = DateTime.Now,
            DataFinal = DateTime.Now.AddMonths(12),
            ValorTotal = 100000m,
            LiderTecnicoId = Guid.NewGuid()
        };

        _mockProjetoService
            .Setup(s => s.Update(idInexistente, updateRequest))
            .ThrowsAsync(new NotFoundException("Cliente não encontrado"));

        // Act
        var mockValidator = new Mock<FluentValidation.IValidator<UpdateProjetoRequest>>();
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<UpdateProjetoRequest>(), default))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Update(idInexistente, updateRequest, mockValidator.Object)
        );
    }

    [Fact]
    public async Task Delete_ComIdValido_RetornaOkComProjetoDeletado()
    {
        // Arrange
        var idProjeto = Guid.NewGuid();
        var projetoDeletado = new ProjetoResponse
        {
            Id = idProjeto,
            Nome = "Projeto a Deletar",
            Localizacao = "Salvador, BA",
            DataInicio = DateTime.Now,
            DataFinal = DateTime.Now.AddMonths(2),
            ValorTotal = 45000m,
            ClienteId = Guid.NewGuid(),
            LiderTecnicoId = Guid.NewGuid()
        };

        _mockProjetoService
            .Setup(s => s.RemoveAsync(idProjeto))
            .ReturnsAsync(projetoDeletado);

        // Act
        var result = await _controller.Delete(idProjeto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedProjeto = Assert.IsType<ProjetoResponse>(okResult.Value);
        Assert.Equal(idProjeto, returnedProjeto.Id);
        Assert.Equal("Projeto a Deletar", returnedProjeto.Nome);
    }

    [Fact]
    public async Task Delete_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();

        _mockProjetoService
            .Setup(s => s.RemoveAsync(idInexistente))
            .ThrowsAsync(new NotFoundException("Projeto não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Delete(idInexistente)
        );
    }
}

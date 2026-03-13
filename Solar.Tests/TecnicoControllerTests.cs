using Microsoft.AspNetCore.Mvc;
using Moq;
using Solar.API.Controllers;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Tecnico;
using Solar.Application.Interfaces;

namespace Solar.Tests;

public class TecnicoControllerTests
{
    private readonly Mock<ITecnicoServices> _mockTecnicoService;
    private readonly TecnicosController _controller;

    public TecnicoControllerTests()
    {
        _mockTecnicoService = new Mock<ITecnicoServices>();
        _controller = new TecnicosController(_mockTecnicoService.Object);
    }

    [Fact]
    public async Task GetTecnicoById_ComIdValido_RetornaOkComTecnico()
    {
        // Arrange
        var idTecnico = Guid.NewGuid();
        var expectedTecnicoResponse = new TecnicoResponse
        {
            Id = idTecnico,
            Nome = "João da Silva",
            Cargo = "Eletricista Sênior"
        };

        _mockTecnicoService
            .Setup(s => s.GetById(idTecnico))
            .ReturnsAsync(expectedTecnicoResponse);

        // Act
        var result = await _controller.GetById(idTecnico);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedTecnico = Assert.IsType<TecnicoResponse>(okResult.Value);
        Assert.Equal(idTecnico, returnedTecnico.Id);
        Assert.Equal("João da Silva", returnedTecnico.Nome);
    }

    [Fact]
    public async Task GetTecnicoById_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var mensagemErro = "Técnico não encontrado";

        _mockTecnicoService
            .Setup(s => s.GetById(idInexistente))
            .ThrowsAsync(new NotFoundException(mensagemErro));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.GetById(idInexistente)
        );

        Assert.Equal(mensagemErro, exception.Message);
    }

    [Fact]
    public async Task GetAll_RetornaListaDeTecnicos()
    {
        // Arrange
        var tecnicosEsperados = new List<TecnicoResponse>
        {
            new TecnicoResponse { Id = Guid.NewGuid(), Nome = "Técnico 1", Cargo = "Eletricista" },
            new TecnicoResponse { Id = Guid.NewGuid(), Nome = "Técnico 2", Cargo = "Encanador" },
            new TecnicoResponse { Id = Guid.NewGuid(), Nome = "Técnico 3", Cargo = "Pintor" }
        };

        _mockTecnicoService
            .Setup(s => s.GetTecnicos())
            .ReturnsAsync(tecnicosEsperados);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedTecnicos = Assert.IsType<List<TecnicoResponse>>(okResult.Value);
        Assert.Equal(3, returnedTecnicos.Count);
        Assert.Equal("Técnico 1", returnedTecnicos[0].Nome);
    }

    [Fact]
    public async Task Create_ComDadosValidos_RetornaCreatedAtAction()
    {
        // Arrange
        var createRequest = new CreateTecnicoRequest
        {
            Nome = "Novo Técnico",
            Cargo = "Eletricista Pleno"
        };

        var tecnicoCriado = new CreateTecnicoResponse
        {
            Id = Guid.NewGuid(),
            Nome = "Novo Técnico",
            Cargo = "Eletricista Pleno"
        };

        _mockTecnicoService
            .Setup(s => s.Create(createRequest))
            .ReturnsAsync(tecnicoCriado);

        // Act
        var result = await _controller.Create(createRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

        Assert.Equal(nameof(TecnicosController.GetById), createdResult.ActionName);
        Assert.NotNull(createdResult.Value);
        var returnedTecnico = Assert.IsType<CreateTecnicoResponse>(createdResult.Value);
        Assert.Equal("Novo Técnico", returnedTecnico.Nome);
    }

    [Fact]
    public async Task Update_ComIdValidoEDadosValidos_RetornaOkComTecnicoAtualizado()
    {
        // Arrange
        var idTecnico = Guid.NewGuid();
        var updateRequest = new UpdateTecnicoRequest
        {
            Nome = "Técnico Atualizado",
            Cargo = "Gerente Técnico"
        };

        var tecnicoAtualizado = new UpdateTecnicoResponse
        {
            Id = idTecnico,
            Nome = "Técnico Atualizado",
            Cargo = "Gerente Técnico"
        };

        _mockTecnicoService
            .Setup(s => s.Update(idTecnico, updateRequest))
            .ReturnsAsync(tecnicoAtualizado);

        // Act
        var result = await _controller.Update(idTecnico, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedTecnico = Assert.IsType<UpdateTecnicoResponse>(okResult.Value);
        Assert.Equal(idTecnico, returnedTecnico.Id);
        Assert.Equal("Técnico Atualizado", returnedTecnico.Nome);
    }

    [Fact]
    public async Task Update_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var updateRequest = new UpdateTecnicoRequest
        {
            Nome = "Técnico Atualizado",
            Cargo = "Gerente Técnico"
        };

        _mockTecnicoService
            .Setup(s => s.Update(idInexistente, updateRequest))
            .ThrowsAsync(new NotFoundException("Técnico não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Update(idInexistente, updateRequest)
        );
    }

    [Fact]
    public async Task Delete_ComIdValido_RetornaOkComTecnicoDeletado()
    {
        // Arrange
        var idTecnico = Guid.NewGuid();
        var tecnicoDeletado = new TecnicoResponse
        {
            Id = idTecnico,
            Nome = "Técnico a Deletar",
            Cargo = "Aprendiz"
        };

        _mockTecnicoService
            .Setup(s => s.RemoveAsync(idTecnico))
            .ReturnsAsync(tecnicoDeletado);

        // Act
        var result = await _controller.Delete(idTecnico);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var returnedTecnico = Assert.IsType<TecnicoResponse>(okResult.Value);
        Assert.Equal(idTecnico, returnedTecnico.Id);
        Assert.Equal("Técnico a Deletar", returnedTecnico.Nome);
    }

    [Fact]
    public async Task Delete_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();

        _mockTecnicoService
            .Setup(s => s.RemoveAsync(idInexistente))
            .ThrowsAsync(new NotFoundException("Técnico não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Delete(idInexistente)
        );
    }
}

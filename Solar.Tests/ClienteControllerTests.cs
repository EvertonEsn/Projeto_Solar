using Microsoft.AspNetCore.Mvc;
using Moq;
using Solar.API.Controllers;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Cliente;
using Solar.Application.Interfaces;

namespace Solar.Tests;

public class ClienteControllerTests
{
    private readonly Mock<IClienteServices> _mockClienteService;
    private readonly ClientesController _controller;
    
    public ClienteControllerTests()
    {
        _mockClienteService = new Mock<IClienteServices>();
        _controller = new ClientesController(_mockClienteService.Object);
    }

    [Fact]
    public async Task GetClienteById_ComIdValido_RetornaOkComCliente()
    {
        var idClienteResponse = new Guid();

        var expectedClienteResponse = new ClienteResponse
        {
            Id = idClienteResponse,
            Nome = "Everton Silva",
            Email = "ever@gmail.com"
        };
        
        _mockClienteService
            .Setup(s => s.GetById(idClienteResponse))
            .ReturnsAsync(expectedClienteResponse);

        var result = await _controller.GetById(idClienteResponse);

        // Verifica que o resultado não é nulo
        Assert.NotNull(result);
        
        // Extrai o valor do ActionResult
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedClienteResponse = Assert.IsType<ClienteResponse>(okResult.Value);
        Assert.Equal(idClienteResponse, returnedClienteResponse.Id);
        Assert.Equal("Everton Silva", returnedClienteResponse.Nome);
    }
    
    [Fact]
    public async Task GetClienteById_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var mensagemErro = "Cliente não encontrado";

        _mockClienteService
            .Setup(s => s.GetById(idInexistente))
            .ThrowsAsync(new NotFoundException(mensagemErro));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.GetById(idInexistente)
        );

        Assert.Equal(mensagemErro, exception.Message);
    }

    [Fact]
    public async Task GetAll_RetornaListaDeClientes()
    {
        // Arrange
        var clientesEsperados = new List<ClienteResponse>
        {
            new ClienteResponse { Id = Guid.NewGuid(), Nome = "Cliente 1", Email = "cliente1@email.com" },
            new ClienteResponse { Id = Guid.NewGuid(), Nome = "Cliente 2", Email = "cliente2@email.com" },
            new ClienteResponse { Id = Guid.NewGuid(), Nome = "Cliente 3", Email = "cliente3@email.com" }
        };

        _mockClienteService
            .Setup(s => s.GetClientes())
            .ReturnsAsync(clientesEsperados);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedClientes = Assert.IsType<List<ClienteResponse>>(okResult.Value);
        Assert.Equal(3, returnedClientes.Count);
        Assert.Equal("Cliente 1", returnedClientes[0].Nome);
    }

    [Fact]
    public async Task Create_ComDadosValidos_RetornaCreatedAtAction()
    {
        // Arrange
        var createRequest = new CreateClienteRequest
        {
            Nome = "Novo Cliente",
            Email = "novocliente@email.com"
        };

        var clienteCriado = new CreateClienteResponse
        {
            Id = Guid.NewGuid(),
            Nome = "Novo Cliente",
            Email = "novocliente@email.com"
        };

        _mockClienteService
            .Setup(s => s.Create(createRequest))
            .ReturnsAsync(clienteCriado);

        // Act
        var result = await _controller.Create(createRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        
        Assert.Equal(nameof(ClientesController.GetById), createdResult.ActionName);
        Assert.NotNull(createdResult.Value);
        var returnedCliente = Assert.IsType<CreateClienteResponse>(createdResult.Value);
        Assert.Equal("Novo Cliente", returnedCliente.Nome);
        Assert.Equal("novocliente@email.com", returnedCliente.Email);
    }

    [Fact]
    public async Task Update_ComIdValidoEDadosValidos_RetornaOkComClienteAtualizado()
    {
        // Arrange
        var idCliente = Guid.NewGuid();
        var updateRequest = new UpdateClienteRequest
        {
            Nome = "Cliente Atualizado",
            Email = "atualizado@email.com"
        };

        var clienteAtualizado = new UpdateClienteResponse
        {
            Id = idCliente,
            Nome = "Cliente Atualizado",
            Email = "atualizado@email.com"
        };

        _mockClienteService
            .Setup(s => s.Update(idCliente, updateRequest))
            .ReturnsAsync(clienteAtualizado);

        // Act
        var result = await _controller.Update(idCliente, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedCliente = Assert.IsType<UpdateClienteResponse>(okResult.Value);
        Assert.Equal(idCliente, returnedCliente.Id);
        Assert.Equal("Cliente Atualizado", returnedCliente.Nome);
    }

    [Fact]
    public async Task Update_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();
        var updateRequest = new UpdateClienteRequest
        {
            Nome = "Cliente Atualizado",
            Email = "atualizado@email.com"
        };

        _mockClienteService
            .Setup(s => s.Update(idInexistente, updateRequest))
            .ThrowsAsync(new NotFoundException("Cliente não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Update(idInexistente, updateRequest)
        );
    }

    [Fact]
    public async Task Delete_ComIdValido_RetornaOkComClienteDeletado()
    {
        // Arrange
        var idCliente = Guid.NewGuid();
        var clienteDeletado = new ClienteResponse
        {
            Id = idCliente,
            Nome = "Cliente a Deletar",
            Email = "deletar@email.com"
        };

        _mockClienteService
            .Setup(s => s.RemoveAsync(idCliente))
            .ReturnsAsync(clienteDeletado);

        // Act
        var result = await _controller.Delete(idCliente);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        
        var returnedCliente = Assert.IsType<ClienteResponse>(okResult.Value);
        Assert.Equal(idCliente, returnedCliente.Id);
        Assert.Equal("Cliente a Deletar", returnedCliente.Nome);
    }

    [Fact]
    public async Task Delete_ComIdInvalido_LancaNotFoundException()
    {
        // Arrange
        var idInexistente = Guid.NewGuid();

        _mockClienteService
            .Setup(s => s.RemoveAsync(idInexistente))
            .ThrowsAsync(new NotFoundException("Cliente não encontrado"));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => _controller.Delete(idInexistente)
        );
    }
}
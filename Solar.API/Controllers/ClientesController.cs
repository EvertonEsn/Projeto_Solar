using Microsoft.AspNetCore.Mvc;
using Solar.Application.DTOs.Cliente;
using Solar.Application.Interfaces;

namespace Solar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteServices _clienteServices;

    public ClientesController(IClienteServices clienteServices)
    {
        _clienteServices = clienteServices;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> GetAll()
    {
        var clientes = await _clienteServices.GetClientes();
        
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteResponse>> GetById(Guid id)
    {

        var cliente = await _clienteServices.GetById(id);
        
        return Ok(cliente);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateClienteResponse>> Create(CreateClienteRequest request)
    {
        var clienteCriado = await _clienteServices.Create(request);
        
        return CreatedAtAction(nameof(GetById), new { id = clienteCriado.Id }, clienteCriado);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateClienteResponse>> Update(Guid id, UpdateClienteRequest request)
    {
        var clienteAtualizado = await _clienteServices.Update(id, request);
        
        return Ok(clienteAtualizado);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteResponse>> Delete(Guid id)
    {
        var clienteDeletado = await _clienteServices.RemoveAsync(id);
        
        return Ok(clienteDeletado);
    }
}

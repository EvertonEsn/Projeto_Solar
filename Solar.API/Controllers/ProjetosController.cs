using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Projeto;
using Solar.Application.Interfaces;

namespace Solar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetosController : ControllerBase
{
    private readonly IProjetoServices _projetoServices;

    public ProjetosController(IProjetoServices projetoServices)
    {
        _projetoServices = projetoServices;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProjetoResponse>>> GetAll()
    {
        var projetos = await _projetoServices.GetProjetos();
        
        return Ok(projetos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjetoResponse>> GetById(Guid id)
    {
        var projeto = await _projetoServices.GetById(id);
        
        return Ok(projeto);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProjetoResponse>> Create(CreateProjetoRequest request, [FromServices] IValidator<CreateProjetoRequest> validator)
    {
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid) throw new AppValidationException(result.Errors);
        
        var projetoCriado = await _projetoServices.Create(request);
        
        return CreatedAtAction(nameof(GetById), new { id = projetoCriado.Id }, projetoCriado);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateProjetoResponse>> Update(Guid id, UpdateProjetoRequest request, [FromServices] IValidator<UpdateProjetoRequest> validator)
    {
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid) throw new AppValidationException(result.Errors);
        
        var projetoAtualizado = await _projetoServices.Update(id, request);
        
        if (projetoAtualizado is null)
            return NotFound(new { message = "Projeto não encontrado" });
        
        return Ok(projetoAtualizado);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjetoResponse>> Delete(Guid id)
    {
        var projetoDeletado = await _projetoServices.RemoveAsync(id);
        
        return Ok(projetoDeletado);
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Procedimento;
using Solar.Application.Interfaces;
using Solar.Application.Validation.ProcedimentoValidator;

namespace Solar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcedimentosController : ControllerBase
{
    private readonly IProcedimentoServices _procedimentoServices;

    public ProcedimentosController(IProcedimentoServices procedimentoServices, ILogger<ProcedimentosController> logger)
    {
        _procedimentoServices = procedimentoServices;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProcedimentoResponse>>> GetAll()
    {
        var procedimentos = await _procedimentoServices.GetProcedimentos();
        
        return Ok(procedimentos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProcedimentoResponse>> GetById(Guid id)
    {
        var procedimento = await _procedimentoServices.GetById(id);
        
        return Ok(procedimento);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateProcedimentoResponse>> Create(CreateProcedimentoRequest request, [FromServices] IValidator<CreateProcedimentoRequest> validator)
    {
        var result = await validator.ValidateAsync(request);
        
        if (!result.IsValid) throw new AppValidationException(result.Errors);
        
        var procedimentoCriado = await _procedimentoServices.Create(request);
        
        return CreatedAtAction(nameof(GetById), new { id = procedimentoCriado.Id }, procedimentoCriado);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateProcedimentoResponse>> Update(Guid id, UpdateProcedimentoRequest request)
    {
        var procedimentoAtualizado = await _procedimentoServices.Update(id, request);
        
        return Ok(procedimentoAtualizado);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProcedimentoResponse>> Delete(Guid id)
    {
        var procedimentoDeletado = await _procedimentoServices.RemoveAsync(id);
        
        return Ok(procedimentoDeletado);
    }
}

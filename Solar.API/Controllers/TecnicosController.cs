using Microsoft.AspNetCore.Mvc;
using Solar.Application.DTOs.Tecnico;
using Solar.Application.Interfaces;

namespace Solar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TecnicosController : ControllerBase
{
    private readonly ITecnicoServices _tecnicoServices;

    public TecnicosController(ITecnicoServices tecnicoServices)
    {
        _tecnicoServices = tecnicoServices;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TecnicoResponse>>> GetAll()
    {
        var tecnicos = await _tecnicoServices.GetTecnicos();
        
        return Ok(tecnicos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TecnicoResponse>> GetById(Guid id)
    {
        var tecnico = await _tecnicoServices.GetById(id);
        
        return Ok(tecnico);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateTecnicoResponse>> Create(CreateTecnicoRequest request)
    {
        var tecnicoCriado = await _tecnicoServices.Create(request);
        
        return CreatedAtAction(nameof(GetById), new { id = tecnicoCriado.Id }, tecnicoCriado);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateTecnicoResponse>> Update(Guid id, UpdateTecnicoRequest request)
    {
        var tecnicoAtualizado = await _tecnicoServices.Update(id, request);
        
        return Ok(tecnicoAtualizado);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TecnicoResponse>> Delete(Guid id)
    {
        var tecnicoDeletado = await _tecnicoServices.RemoveAsync(id);
        
        return Ok(tecnicoDeletado);
    }
}

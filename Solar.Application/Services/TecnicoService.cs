using AutoMapper;
using Solar.Application.DTOs.Tecnico;
using Solar.Application.Interfaces;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;

namespace Solar.Application.Services;

public class TecnicoService : ITecnicoServices
{
    private readonly ITecnicoRepository _tecnicoRepository;
    private readonly IMapper _mapper;

    public TecnicoService(ITecnicoRepository tecnicoRepository, IMapper mapper)
    {
        _tecnicoRepository = tecnicoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TecnicoResponse>> GetTecnicos()
    {
        var tecnicosEntities = await _tecnicoRepository.GetTecnicosAsync();
        return _mapper.Map<IEnumerable<TecnicoResponse>>(tecnicosEntities);
    }

    public async Task<TecnicoResponse?> GetById(Guid id)
    {
        var tecnicoEntity = await _tecnicoRepository.GetByIdAsync(id);
        
        if (tecnicoEntity == null)
        {
            return null;
        }
        
        return _mapper.Map<TecnicoResponse>(tecnicoEntity);
    }

    public async Task<CreateTecnicoResponse> Create(CreateTecnicoRequest tecnicoRequest)
    {
        var tecnico = new Tecnico(tecnicoRequest.Nome, tecnicoRequest.Cargo);
        
        var tecnicoEntity = await _tecnicoRepository.CreateAsync(tecnico);
        
        return _mapper.Map<CreateTecnicoResponse>(tecnicoEntity);
    }

    public async Task<UpdateTecnicoResponse?> Update(Guid id, UpdateTecnicoRequest tecnicoRequest)
    {
        var tecnico = await _tecnicoRepository.GetByIdAsync(id);

        if (tecnico is null) return null;
        
        tecnico.Update(tecnicoRequest.Nome, tecnicoRequest.Cargo);

        await _tecnicoRepository.UpdateAsync(tecnico);
        
        return _mapper.Map<UpdateTecnicoResponse>(tecnico);
    }

    public async Task<TecnicoResponse?> RemoveAsync(Guid id)
    {
        var tecnicoEntity = await _tecnicoRepository.GetByIdAsync(id);
        
        if (tecnicoEntity is null) return null;

        await _tecnicoRepository.RemoveAsync(tecnicoEntity);
        
        return _mapper.Map<TecnicoResponse>(tecnicoEntity);
    }
}

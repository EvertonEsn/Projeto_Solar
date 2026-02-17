using AutoMapper;
using Solar.Application.DTOs.Procedimento;
using Solar.Application.Interfaces;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;

namespace Solar.Application.Services;

public class ProcedimentoService : IProcedimentoServices
{
    private readonly IProcedimentoRepository _procedimentoRepository;
    private readonly IMapper _mapper;

    public ProcedimentoService(IProcedimentoRepository procedimentoRepository, IMapper mapper)
    {
        _procedimentoRepository = procedimentoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProcedimentoResponse>> GetProcedimentos()
    {
        var procedimentosEntities = await _procedimentoRepository.GetProcedimentosAsync();
        return _mapper.Map<IEnumerable<ProcedimentoResponse>>(procedimentosEntities);
    }

    public async Task<ProcedimentoResponse?> GetById(Guid id)
    {
        var procedimentoEntity = await _procedimentoRepository.GetByIdAsync(id);
        
        if (procedimentoEntity == null)
        {
            return null;
        }
        
        return _mapper.Map<ProcedimentoResponse>(procedimentoEntity);
    }

    public async Task<CreateProcedimentoResponse> Create(CreateProcedimentoRequest procedimentoRequest)
    {
        var procedimento = new Procedimento(procedimentoRequest.Descricao, procedimentoRequest.ProjetoId);
        
        var procedimentoEntity = await _procedimentoRepository.CreateAsync(procedimento);
        
        return _mapper.Map<CreateProcedimentoResponse>(procedimentoEntity);
    }

    public async Task<UpdateProcedimentoResponse?> Update(Guid id, UpdateProcedimentoRequest procedimentoRequest)
    {
        var procedimento = await _procedimentoRepository.GetByIdAsync(id);

        if (procedimento is null) return null;
        
        procedimento.Update(procedimentoRequest.Descricao, procedimentoRequest.Concluido);

        await _procedimentoRepository.UpdateAsync(procedimento);
        
        return _mapper.Map<UpdateProcedimentoResponse>(procedimento);
    }

    public async Task<ProcedimentoResponse?> RemoveAsync(Guid id)
    {
        var procedimentoEntity = await _procedimentoRepository.GetByIdAsync(id);
        
        if (procedimentoEntity is null) return null;

        await _procedimentoRepository.RemoveAsync(procedimentoEntity);
        
        return _mapper.Map<ProcedimentoResponse>(procedimentoEntity);
    }
}

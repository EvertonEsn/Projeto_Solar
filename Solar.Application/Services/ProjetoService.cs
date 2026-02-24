using AutoMapper;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Projeto;
using Solar.Application.Interfaces;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;

namespace Solar.Application.Services;

public class ProjetoService : IProjetoServices
{
    private readonly IProjetoRepository _projetoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ITecnicoRepository _tecnicoRepository;
    private readonly IMapper _mapper;

    public ProjetoService(IProjetoRepository projetoRepository, IMapper mapper, IClienteRepository clienteRepository, ITecnicoRepository tecnicoRepository)
    {
        _projetoRepository = projetoRepository;
        _clienteRepository = clienteRepository;
        _tecnicoRepository = tecnicoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjetoResponse>> GetProjetos()
    {
        var projetosEntities = await _projetoRepository.GetProjetosAsync();
        
        return _mapper.Map<IEnumerable<ProjetoResponse>>(projetosEntities);
    }

    public async Task<ProjetoResponse?> GetById(Guid id)
    {
        var projetoEntity = await _projetoRepository.GetByIdAsync(id);
        
        if (projetoEntity == null)
        {
            throw new NotFoundException("Projeto nao encontrado");
        }
        
        return _mapper.Map<ProjetoResponse>(projetoEntity);
    }

    public async Task<CreateProjetoResponse> Create(CreateProjetoRequest projetoRequest)
    {
        var projeto = new Projeto(
            projetoRequest.Nome, 
            projetoRequest.Localizacao,
            projetoRequest.ValorTotal, 
            projetoRequest.ClienteId, 
            projetoRequest.LiderTecnicoId,
            projetoRequest.DataInicio
        );
        
        var projetoEntity = await _projetoRepository.CreateAsync(projeto);
        
        return _mapper.Map<CreateProjetoResponse>(projetoEntity);
    }

    public async Task<UpdateProjetoResponse?> Update(Guid id, UpdateProjetoRequest projetoRequest)
    {
        var projeto = await _projetoRepository.GetByIdAsync(id);

        if (projeto is null) throw new NotFoundException("Projeto nao encontrado");
        
        projeto.Update(
            projetoRequest.Nome,
            projetoRequest.Localizacao,
            projetoRequest.DataInicio,
            projetoRequest.DataFinal,
            projetoRequest.ValorTotal,
            projetoRequest.LiderTecnicoId
        );

        await _projetoRepository.UpdateAsync(projeto);
        
        return _mapper.Map<UpdateProjetoResponse>(projeto);
    }

    public async Task<ProjetoResponse?> RemoveAsync(Guid id)
    {
        var projetoEntity = await _projetoRepository.GetByIdAsync(id);
        
        if (projetoEntity is null) throw new NotFoundException("Projeto nao encontrado");

        await _projetoRepository.RemoveAsync(projetoEntity);
        
        return _mapper.Map<ProjetoResponse>(projetoEntity);
    }
}

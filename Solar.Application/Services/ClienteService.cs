using System.Globalization;
using AutoMapper;
using Solar.API.Exceptions;
using Solar.Application.DTOs.Cliente;
using Solar.Application.Interfaces;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;

namespace Solar.Application.Services;

public class ClienteService : IClienteServices
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ClienteResponse>> GetClientes()
    {
        var clientesEntities = await _clienteRepository.GetClientesAsync();
        
        return _mapper.Map<IEnumerable<ClienteResponse>>(clientesEntities);
    }

    public async Task<ClienteResponse?> GetById(Guid id)
    {
        var clienteEntity = await _clienteRepository.GetByIdAsync(id);
        
        if (clienteEntity == null)
        {
            throw new NotFoundException("Cliente nao encontrado");
        }
        
        return _mapper.Map<ClienteResponse>(clienteEntity);
    }

    public async Task<CreateClienteResponse> Create(CreateClienteRequest clienteRequest)
    {
        var cliente = new Cliente(clienteRequest.Nome, clienteRequest.Email);
        
        var clienteEntity = await _clienteRepository.CreateAsync(cliente);
        
        return _mapper.Map<CreateClienteResponse>(clienteEntity);
    }

    public async Task<UpdateClienteResponse?> Update(Guid id, UpdateClienteRequest clienteRequest)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null) throw new NotFoundException("Cliente nao encontrado");
        
        cliente.Update(clienteRequest.Nome, clienteRequest.Email);

        await _clienteRepository.UpdateAsync(cliente);
        
        return _mapper.Map<UpdateClienteResponse>(cliente);
    }

    public async Task<ClienteResponse?> RemoveAsync(Guid id)
    {
        var clienteEntity = await _clienteRepository.GetByIdAsync(id);
        
        if (clienteEntity is null) throw new NotFoundException("Cliente nao encontrado");

        await _clienteRepository.RemoveAsync(clienteEntity);
        
        return _mapper.Map<ClienteResponse>(clienteEntity);
    }
}
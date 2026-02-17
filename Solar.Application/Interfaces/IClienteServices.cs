using Solar.Application.DTOs.Cliente;

namespace Solar.Application.Interfaces;

public interface IClienteServices
{
    Task<IEnumerable<ClienteResponse>> GetClientes();
    
    Task<ClienteResponse?> GetById(Guid id);
    
    Task<CreateClienteResponse> Create(CreateClienteRequest clienteRequest);
    
    Task<UpdateClienteResponse?> Update(Guid id, UpdateClienteRequest clienteRequest);
    
    Task<ClienteResponse?> RemoveAsync(Guid id);
}
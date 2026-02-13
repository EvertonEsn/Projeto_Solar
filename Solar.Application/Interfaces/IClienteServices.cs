using Solar.Application.DTOs.Cliente;

namespace Solar.Application.Interfaces;

public interface IClienteServices
{
    Task<IEnumerable<ClienteResponse>> GetClientes();
    
    Task<ClienteResponse?> GetById(int? id);
    
    Task<CreateClienteResponse> Create(CreateClienteRequest cliente);
    
    Task<UpdateClienteResponse> Update(int? id, UpdateClienteRequest cliente);
    
    Task<ClienteResponse> RemoveAsync(int? id);
}
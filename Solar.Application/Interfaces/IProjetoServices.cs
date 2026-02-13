using Solar.Application.DTOs.Projeto;

namespace Solar.Application.Interfaces;

public interface IProjetoServices
{
    Task<IEnumerable<ProjetoResponse>> GetProjetos();
    
    Task<ProjetoResponse?> GetById(Guid? id);
    
    Task<CreateProjetoResponse> Create(CreateProjetoRequest projeto);
    
    Task<UpdateProjetoResponse> Update(Guid? id, UpdateProjetoRequest projeto);
    
    Task<ProjetoResponse> RemoveAsync(Guid? id);
}

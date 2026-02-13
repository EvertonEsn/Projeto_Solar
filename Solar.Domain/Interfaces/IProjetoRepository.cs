using Solar.Domain.Entities;

namespace Solar.Domain.Interfaces;

public interface IProjetoRepository
{
    Task<IEnumerable<Projeto>> GetProjetosAsync();
    
    Task<Projeto?> GetByIdAsync(Guid? id);
    
    Task<Projeto> CreateAsync(Projeto projeto);
    
    Task<Projeto> UpdateAsync(Projeto projeto);
    
    Task<Projeto> RemoveAsync(Projeto projeto);
}

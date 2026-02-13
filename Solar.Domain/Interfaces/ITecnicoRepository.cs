using Solar.Domain.Entities;

namespace Solar.Domain.Interfaces;

public interface ITecnicoRepository
{
    Task<IEnumerable<Tecnico>> GetTecnicosAsync();
    
    Task<Tecnico?> GetByIdAsync(Guid? id);
    
    Task<Tecnico> CreateAsync(Tecnico tecnico);
    
    Task<Tecnico> UpdateAsync(Tecnico tecnico);
    
    Task<Tecnico> RemoveAsync(Tecnico tecnico);
}

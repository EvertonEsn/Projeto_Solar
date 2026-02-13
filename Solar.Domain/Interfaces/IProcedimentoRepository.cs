using Solar.Domain.Entities;

namespace Solar.Domain.Interfaces;

public interface IProcedimentoRepository
{
    Task<IEnumerable<Procedimento>> GetProcedimentosAsync();
    
    Task<Procedimento?> GetByIdAsync(Guid? id);
    
    Task<Procedimento> CreateAsync(Procedimento procedimento);
    
    Task<Procedimento> UpdateAsync(Procedimento procedimento);
    
    Task<Procedimento> RemoveAsync(Procedimento procedimento);
}

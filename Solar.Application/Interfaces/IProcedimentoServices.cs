using Solar.Application.DTOs.Procedimento;

namespace Solar.Application.Interfaces;

public interface IProcedimentoServices
{
    Task<IEnumerable<ProcedimentoResponse>> GetProcedimentos();
    
    Task<ProcedimentoResponse?> GetById(Guid? id);
    
    Task<CreateProcedimentoResponse> Create(CreateProcedimentoRequest procedimento);
    
    Task<UpdateProcedimentoResponse> Update(Guid? id, UpdateProcedimentoRequest procedimento);
    
    Task<ProcedimentoResponse> RemoveAsync(Guid? id);
}

using Solar.Application.DTOs.Tecnico;

namespace Solar.Application.Interfaces;

public interface ITecnicoServices
{
    Task<IEnumerable<TecnicoResponse>> GetTecnicos();
    
    Task<TecnicoResponse?> GetById(Guid id);
    
    Task<CreateTecnicoResponse> Create(CreateTecnicoRequest tecnico);
    
    Task<UpdateTecnicoResponse?> Update(Guid id, UpdateTecnicoRequest tecnicoRequest);
    
    Task<TecnicoResponse?> RemoveAsync(Guid id);
}

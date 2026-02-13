using Microsoft.EntityFrameworkCore;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;
using Solar.Infrastructure.Context;

namespace Solar.Infrastructure.Repositories;

public class TecnicoRepository : ITecnicoRepository
{
    private readonly ApplicationDbContext _context;

    public TecnicoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tecnico>> GetTecnicosAsync()
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Tecnico?> GetByIdAsync(Guid? id)
    {
        return await _context.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tecnico> CreateAsync(Tecnico tecnico)
    {
        _context.Add(tecnico);
        await _context.SaveChangesAsync();
        return tecnico;
    }

    public async Task<Tecnico> UpdateAsync(Tecnico tecnico)
    {
        _context.Update(tecnico);
        await _context.SaveChangesAsync();
        return tecnico;
    }

    public async Task<Tecnico> RemoveAsync(Tecnico tecnico)
    {
        _context.Remove(tecnico);
        await _context.SaveChangesAsync();
        return tecnico;
    }
}

using Microsoft.EntityFrameworkCore;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;
using Solar.Infrastructure.Context;

namespace Solar.Infrastructure.Repositories;

public class ProjetoRepository : IProjetoRepository
{
    private readonly ApplicationDbContext _context;

    public ProjetoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Projeto>> GetProjetosAsync()
    {
        return await _context.Projetos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.LiderTecnico)
            .ToListAsync();
    }

    public async Task<Projeto?> GetByIdAsync(Guid? id)
    {
        return await _context.Projetos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.LiderTecnico)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Projeto> CreateAsync(Projeto projeto)
    {
        _context.Add(projeto);
        await _context.SaveChangesAsync();
        return projeto;
    }

    public async Task<Projeto> UpdateAsync(Projeto projeto)
    {
        _context.Update(projeto);
        await _context.SaveChangesAsync();
        return projeto;
    }

    public async Task<Projeto> RemoveAsync(Projeto projeto)
    {
        _context.Remove(projeto);
        await _context.SaveChangesAsync();
        return projeto;
    }
}

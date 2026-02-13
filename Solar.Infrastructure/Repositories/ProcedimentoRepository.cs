using Microsoft.EntityFrameworkCore;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;
using Solar.Infrastructure.Context;

namespace Solar.Infrastructure.Repositories;

public class ProcedimentoRepository : IProcedimentoRepository
{
    private readonly ApplicationDbContext _context;

    public ProcedimentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Procedimento>> GetProcedimentosAsync()
    {
        return await _context.Procedimentos
            .AsNoTracking()
            .Include(p => p.Projeto)
            .ToListAsync();
    }

    public async Task<Procedimento?> GetByIdAsync(Guid? id)
    {
        return await _context.Procedimentos
            .AsNoTracking()
            .Include(p => p.Projeto)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Procedimento> CreateAsync(Procedimento procedimento)
    {
        _context.Add(procedimento);
        await _context.SaveChangesAsync();
        return procedimento;
    }

    public async Task<Procedimento> UpdateAsync(Procedimento procedimento)
    {
        _context.Update(procedimento);
        await _context.SaveChangesAsync();
        return procedimento;
    }

    public async Task<Procedimento> RemoveAsync(Procedimento procedimento)
    {
        _context.Remove(procedimento);
        await _context.SaveChangesAsync();
        return procedimento;
    }
}

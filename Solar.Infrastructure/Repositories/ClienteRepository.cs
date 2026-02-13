using Microsoft.EntityFrameworkCore;
using Solar.Domain.Entities;
using Solar.Domain.Interfaces;
using Solar.Infrastructure.Context;

namespace Solar.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(Guid? id)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        _context.Update(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> RemoveAsync(Cliente cliente)
    {
        _context.Remove(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }
}

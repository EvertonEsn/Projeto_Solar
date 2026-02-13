using Microsoft.EntityFrameworkCore;
using Solar.Domain.Entities;

namespace Solar.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Procedimento> Procedimentos { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext)
            .Assembly);
    }
}
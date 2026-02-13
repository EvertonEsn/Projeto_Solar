using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solar.Domain.Interfaces;
using Solar.Infrastructure.Context;
using Solar.Infrastructure.Repositories;

namespace Solar.CrossCutting.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
        services.AddScoped<ITecnicoRepository, TecnicoRepository>();

        return services;
    }
}
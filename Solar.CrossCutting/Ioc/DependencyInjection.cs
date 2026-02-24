using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Solar.Application.DTOs.Mapping;
using Solar.Application.Interfaces;
using Solar.Application.Services;
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
                new MySqlServerVersion(new Version(8, 0, 0)),
                mySqlOptions => 
                {
                    // 👇 Isso faz o C# tentar conectar de novo caso o banco ainda esteja "acordando"
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, 
                        maxRetryDelay: TimeSpan.FromSeconds(10), 
                        errorNumbersToAdd: null);
                }
                ));

        // Registrar Repositórios
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProjetoRepository, ProjetoRepository>();
        services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
        services.AddScoped<ITecnicoRepository, TecnicoRepository>();

        // Registrar Services
        services.AddScoped<IClienteServices, ClienteService>();
        services.AddScoped<IProjetoServices, ProjetoService>();
        services.AddScoped<IProcedimentoServices, ProcedimentoService>();
        services.AddScoped<ITecnicoServices, TecnicoService>();

        // Registrar AutoMapper
        services.AddAutoMapper(typeof(ClienteDtoMapping));
        services.AddAutoMapper(typeof(ProcedimentoDtoMapping));
        services.AddAutoMapper(typeof(ProjetoDtoMapping));
        services.AddAutoMapper(typeof(TecnicoDtoMapping));

        // Registrar FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static void ApplyDatabaseMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                // Aqui usamos o seu ApplicationDbContext
                var context = services.GetRequiredService<ApplicationDbContext>();
                
                // Como o EnableRetryOnFailure foi configurado lá em cima, 
                // este Migrate já vai tentar reconectar automaticamente caso o MySQL demore!
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations no banco de dados.");
                throw; 
            }
        }
    }
}

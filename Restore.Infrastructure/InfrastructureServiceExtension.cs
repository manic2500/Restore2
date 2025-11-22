using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Restore.Infrastructure.Persistence.Repositories;
using Restore.Infrastructure.Persistence.DbContexts;
using Restore.Infrastructure.Persistence.UnitOfWork;
using Restore.Application.Interfaces;


namespace Restore.Infrastructure;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork>(sp =>
        {
            var contexts = new List<DbContext>
            {
                sp.GetRequiredService<StoreDbContext>()
            };

            return new UnitOfWork(contexts);
        });
        //services.AddScoped<IContextService, ContextService>();

        services.AddDbContext<StoreDbContext>((serviceProvider, options) =>
        {
            var connection = serviceProvider.GetRequiredService<DbConnection>();
            options.UseNpgsql(connection).UseSnakeCaseNamingConvention();
        });
        services.AddScoped<IProductRepository, ProductRepository>();


        return services;
    }
}

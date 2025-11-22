using Microsoft.Extensions.DependencyInjection;
using Restore.Application.UseCases.Products;

namespace Restore.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IGetProductUseCase, GetProductUseCase>();
        return services;
    }
}


/* services.AddTransient<IGetInventoriesByNameUseCase, GetInventoriesByNameUseCase>();
services.AddTransient<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
services.AddTransient<IUpdateInventoryUseCase, UpdateInventoryUseCase>(); */


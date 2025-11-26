using Microsoft.Extensions.DependencyInjection;
using Restore.Application.Baskets.UseCases;
using Restore.Application.Products.UseCases;


namespace Restore.Application;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Product
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IGetProductUseCase, GetProductUseCase>();

        // Basket
        services.AddScoped<ICreateBasketUseCase, CreateBasketUseCase>();
        services.AddScoped<IClearBasketUseCase, ClearBasketUseCase>();

        // Basket Item
        services.AddScoped<IGetBasketUseCase, GetBasketUseCase>();
        services.AddScoped<IAddBasketItemUseCase, AddBasketItemUseCase>();
        services.AddScoped<IRemoveBasketItemUseCase, RemoveBasketItemUseCase>();

        services.AddScoped<IDecreaseBasketItemUseCase, DecreaseBasketItemUseCase>();
        services.AddScoped<IIncreaseBasketItemUseCase, IncreaseBasketItemUseCase>();

        return services;
    }
}


/* services.AddTransient<IGetInventoriesByNameUseCase, GetInventoriesByNameUseCase>();
services.AddTransient<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
services.AddTransient<IUpdateInventoryUseCase, UpdateInventoryUseCase>(); */


using Restore.Application.Products.DTOs;
using Restore.Domain.Entities;

namespace Restore.Application.Products.Mappers;


public static class ProductMapper
{
    // ENTITY → DTO
    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.Xid,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            PictureUrl = product.PictureUrl,
            Type = product.Type,
            Brand = product.Brand,
            QuantityInStock = product.QuantityInStock
        };
    }

    public static ProductDto[] ToDtoList(this Product[] products)
    {
        if (products is null) return [];

        var result = new ProductDto[products.Length];

        for (int i = 0; i < products.Length; i++)
        {
            result[i] = products[i].ToDto();
        }
        return result;
    }

    // DTO → ENTITY (for creating new products)
    public static Product ToEntity(this ProductDto dto)
    {
        return new Product
        {
            Xid = dto.ProductId,   // You may choose to ignore this if generated in DB/app
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            PictureUrl = dto.PictureUrl,
            Type = dto.Type,
            Brand = dto.Brand,
            QuantityInStock = dto.QuantityInStock
        };
    }

    // DTO → ENTITY (update existing entity)
    public static void UpdateEntity(this ProductDto dto, Product product)
    {
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.PictureUrl = dto.PictureUrl;
        product.Type = dto.Type;
        product.Brand = dto.Brand;
        product.QuantityInStock = dto.QuantityInStock;

        // Important: Normally you DO NOT allow DTO to update PublicId or Id.
        // They are typically immutable identifiers.
    }
}
using Restore.Application.Baskets.DTOs;
using Restore.Application.Baskets.Helpers;
using Restore.Application.Baskets.Interfaces;
using Restore.Application.Baskets.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Application.Vouchers.Interfaces;
using Restore.Domain.Entities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IGetBasketUseCase
{
    Task<BasketDto> ExecuteAsync(Guid basketXid);
}

public class GetBasketUseCase(
    IBasketRepository basketRepo,
    IProductRepository productRepo,
    ITaxSettingRepository taxRepo,
    IDeliverySettingRepository deliveryRepo,
    IVoucherRepository voucherRepo,
    IRemoveVoucherUseCase removeVoucher
    ) : IGetBasketUseCase
{
    public async Task<BasketDto> ExecuteAsync(Guid basketId)
    {
        var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        // 1. Load product details for items
        var productIds = basket.Items.Select(i => i.ProductXid).ToList(); // Get all product IDs from the basket items
        var products = await productRepo.GetByXidsAsync(productIds); // Fetch live product data - returns List<Product>
        var productDict = products.ToDictionary(p => p.ExtId); // Build a lookup dictionary: Xid → Product

        // 2. Convert items to DTO
        var dto = BasketMapper.ToDto(basket, productDict);

        // 3. Load tax + delivery settings
        var tax = await taxRepo.GetSingleOrThrowAsync(errorMessage: "Tax information is not available");
        var delivery = await deliveryRepo.GetSingleOrThrowAsync(errorMessage: "Delivery information is not available");

        // 4. Load voucher if applied        
        bool hasVoucher = !string.IsNullOrWhiteSpace(basket.VoucherCode);// Check if a voucher is applied

        Voucher? voucher = null;
        if (hasVoucher)
        {
            voucher = await voucherRepo.GetSingleOrThrowAsync(v => v.Code == basket.VoucherCode, errorMessage: "Invalid Coupon");
        }

        // 5. Use calculator
        var calc = new BasketPriceCalculator(tax, delivery, voucher);

        dto.Tax = calc.CalculateTax(dto.SubTotal);
        dto.Shipping = calc.CalculateShipping(dto.SubTotal);
        dto.Discount = calc.CalculateDiscount(dto.SubTotal);

        //6. Handle voucher eligibility
        bool voucherInvalid = dto.Discount == 0 && hasVoucher;

        if (voucherInvalid)
        {
            await removeVoucher.ExecuteAsync(basketId); // persist change
            basket.VoucherCode = null;
            dto.AppliedVoucher = null;
        }
        else
        {
            dto.AppliedVoucher = basket.VoucherCode;
        }

        return dto;

    }
}

/* 

var basketDto = new BasketDto
{
    Xid = basket.Xid,
    Items = [.. basket.Items.Select(i =>
    {
        var product = products.Single(p => p.Xid == i.ProductXid);
        return new BasketItemDto
        {
            Product = product.ToDto(),
            Quantity = i.Quantity
        };
    })]
}; 

 */


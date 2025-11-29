using Restore.Application.Baskets.DTOs;
using Restore.Application.Baskets.Interfaces;
using Restore.Application.Products.Interfaces;
using Restore.Application.Vouchers.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;


namespace Restore.Application.Baskets.UseCases;

public interface IApplyVoucherUseCase
{
    Task<MethodResult<BasketDto>> ExecuteAsync(Guid basketId, string voucherCode);
}

public class ApplyVoucherUseCase(
    IBasketRepository basketRepo,
    IProductRepository productRepo,
    IVoucherRepository voucherRepo,
    IGetBasketUseCase getBasketUseCase,
    IUnitOfWork uow) : IApplyVoucherUseCase
{
    public async Task<MethodResult<BasketDto>> ExecuteAsync(Guid basketId, string voucherCode)
    {
        // 1. Load basket
        /* var basket = await basketRepo.GetByExIdAsync(basketId);
        if (basket is null)
            return Result.NotFound<BasketDto>($"Basket with id '{basketId}' not found."); */
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error<BasketDto>(basketResult.Status, basketResult.Error!);
        var basket = basketResult.Data;

        // 2. Load voucher
        var voucher = await voucherRepo.GetByCodeAsync(voucherCode);
        if (voucher is null)
            return Result.ValidationError<BasketDto>("Voucher not found.");

        // 3. Validate voucher state
        if (!voucher.IsActive)
            return Result.ValidationError<BasketDto>("Voucher is not active.");

        var now = DateTime.UtcNow;
        if (now < voucher.StartDate || now > voucher.EndDate)
            return Result.ValidationError<BasketDto>("Voucher is expired.");

        if (voucher.UsageLimit.HasValue && voucher.UsageCount >= voucher.UsageLimit.Value)
            return Result.ValidationError<BasketDto>("Voucher usage limit reached.");

        // 4. Compute basket subtotal (in cents)
        var productIds = basket.Items.Select(i => i.ProductExtId).ToList();
        var products = await productRepo.GetByXidsAsync(productIds);

        // Build lookup
        var lookup = products.ToDictionary(p => p.ExtId);

        // Validate all products exist
        foreach (var item in basket.Items)
        {
            if (!lookup.TryGetValue(item.ProductExtId, out _))
                return Result.NotFound<BasketDto>($"Product '{item.ProductExtId}' not found.");
        }

        var subtotal = basket.Items.Sum(i =>
            i.Quantity * lookup[i.ProductExtId].Price
        );

        if (subtotal < voucher.MinOrderAmount)
            return Result.ValidationError<BasketDto>(
                $"Minimum order amount for this voucher is {MoneyFormatter.Format(voucher.MinOrderAmount)}."
            );

        // 5. Attach voucher to basket
        basket.VoucherCode = voucher.Code;

        // 6. Save changes
        await uow.SaveChangesAsync();

        // 7. Reuse GetBasketUseCase for consistent result structure
        var result = await getBasketUseCase.ExecuteAsync(basketId);

        if (!result.Success)
            return Result.Error<BasketDto>(result.Status, result.Error!);

        return Result.Ok(result.Data);
    }
}

/* public class ApplyVoucherUseCase(
    IBasketRepository basketRepo,
    IProductRepository productRepo,
    IVoucherRepository voucherRepo,
    IGetBasketUseCase getBasketUseCase,
    IUnitOfWork uow) : IApplyVoucherUseCase
{
    public async Task<MethodResult<BasketDto>> ExecuteAsync(Guid basketId, string voucherCode)
    {
        // 1. Load basket
        var basket = await basketRepo.GetByExIdAsync(basketId)
            ?? throw new BasketNotFoundException(basketId);

        // 2. Load voucher
        var voucher = await voucherRepo.GetByCodeAsync(voucherCode)
            ?? throw new BusinessException("Voucher not found.");

        // 3. Validate voucher state
        if (!voucher.IsActive)
            throw new BusinessException("Voucher is not active.");

        if (DateTime.UtcNow < voucher.StartDate || DateTime.UtcNow > voucher.EndDate)
            throw new BusinessException("Voucher is expired.");

        if (voucher.UsageLimit.HasValue && voucher.UsageCount >= voucher.UsageLimit.Value)
            throw new BusinessException("Voucher usage limit reached.");

        // 4. Compute basket subtotal (in cents)
        var productIds = basket.Items.Select(i => i.ProductXid).ToList();
        var products = await productRepo.GetByXidsAsync(productIds);
        var lookup = products.ToDictionary(p => p.ExtId);

        var subtotal = basket.Items.Sum(i =>
            i.Quantity * lookup[i.ProductXid].Price
        );

        if (subtotal < voucher.MinOrderAmount)
            throw new BusinessException($"Minimum order amount for this voucher is {MoneyFormatter.Format(voucher.MinOrderAmount)}.");

        // 5. Attach voucher to basket
        basket.VoucherCode = voucher.Code;

        // 6. Save changes
        await uow.SaveChangesAsync();

        // 7. Reuse GetBasketUseCase logic (recommended)
        return await getBasketUseCase.ExecuteAsync(basketId);

    }
}

 */
using Restore.Application.Baskets.DTOs;
using Restore.Application.Baskets.Interfaces;
using Restore.Application.Products.Interfaces;
using Restore.Application.Vouchers.Interfaces;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IApplyVoucherUseCase
{
    Task<BasketDto> ExecuteAsync(Guid basketId, string voucherCode);
}

public class ApplyVoucherUseCase(
    IBasketRepository basketRepo,
    IProductRepository productRepo,
    IVoucherRepository voucherRepo,
    IGetBasketUseCase getBasketUseCase,
    IUnitOfWork uow) : IApplyVoucherUseCase
{
    public async Task<BasketDto> ExecuteAsync(Guid basketId, string voucherCode)
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


namespace Restore.Domain.Entities;

public enum DiscountType
{
    Percentage = 1,
    Flat = 2
}

/* 

In ApplyVoucherUseCase you would branch logic:
decimal discountValue = voucher.DiscountType switch
{
    DiscountType.Flat => voucher.DiscountAmount / 100m,
    DiscountType.Percentage => subtotal * (voucher.DiscountAmount / 100m),
    _ => 0
};

 */
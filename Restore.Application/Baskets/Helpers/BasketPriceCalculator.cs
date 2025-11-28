using Restore.Domain.Entities;

namespace Restore.Application.Baskets.Helpers;

public class BasketPriceCalculator(TaxSetting tax, DeliverySetting delivery, Voucher? voucher)
{
    /// <summary>
    /// Calculates tax amount in cents based on subtotal.
    /// Supports fractional tax percentages (e.g., 8.25%).
    /// </summary>
    public long CalculateTax(long subtotalInCents)
    {
        decimal taxRate = tax.TaxPercentage / 100m;            // e.g. 8.25 → 0.0825
        decimal raw = subtotalInCents * taxRate;               // convert cents → decimal math
        return (long)Math.Round(raw, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Calculates shipping charge in cents.
    /// Returns 0 if subtotal meets the free-shipping threshold.
    /// </summary>
    public long CalculateShipping(long subtotalInCents)
    {
        if (subtotalInCents >= delivery.FreeShippingThreshold)
            return 0;

        return delivery.FlatFee; // already cents
    }

    /// <summary>
    /// Calculates voucher discount in cents.
    /// Supports Flat or Percentage types and MaxDiscountAmount.
    /// Returns 0 if voucher is invalid or does not apply.
    /// </summary>
    public long CalculateDiscount(long subtotalInCents)
    {
        if (voucher is null) return 0;
        if (!voucher.IsActive) return 0;

        var now = DateTime.UtcNow;
        if (now < voucher.StartDate || now > voucher.EndDate)
            return 0;

        if (subtotalInCents < voucher.MinOrderAmount)
            return 0;

        if (voucher.UsageLimit.HasValue && voucher.UsageCount >= voucher.UsageLimit)
            return 0;

        // Use normal switch statement (allows full block logic)
        switch (voucher.DiscountType)
        {
            case DiscountType.Flat:
                return voucher.DiscountAmount;

            case DiscountType.Percentage:
                {
                    // Calculate percentage discount safely in cents
                    long calculated = subtotalInCents * voucher.DiscountAmount / 100;

                    if (voucher.MaxDiscountAmount.HasValue)
                        return Math.Min(calculated, voucher.MaxDiscountAmount.Value);

                    return calculated;
                }

            default:
                return 0;
        }
    }

}

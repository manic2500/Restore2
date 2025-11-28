using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.DTOs;

using System.ComponentModel.DataAnnotations;

public record VoucherRequest : IValidatableObject
{
    [Required(ErrorMessage = "Code is required.")]
    [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters.")]
    public string Code { get; init; } = null!;

    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string? Description { get; init; }

    [Required(ErrorMessage = "DiscountType is required.")]
    public DiscountType DiscountType { get; init; }

    [Range(1, long.MaxValue, ErrorMessage = "DiscountAmount must be greater than zero.")]
    public long DiscountAmount { get; init; }   // in cents or percentage

    public long MaxDiscountAmount { get; init; }   // in cents or percentage

    [Range(0, long.MaxValue, ErrorMessage = "MinOrderAmount cannot be negative.")]
    public long MinOrderAmount { get; init; } = 0;

    [Range(1, int.MaxValue, ErrorMessage = "UsageLimit must be greater than zero.")]
    public int? UsageLimit { get; init; }

    [Required(ErrorMessage = "StartDate is required.")]
    public DateTime StartDate { get; init; }

    [Required(ErrorMessage = "EndDate is required.")]
    public DateTime EndDate { get; init; }

    // -------------------------------
    // Custom cross-field / conditional validation
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Percentage voucher requires MaxDiscountAmount > 0
        if (DiscountType == DiscountType.Percentage && MaxDiscountAmount <= 0)
        {
            yield return new ValidationResult(
                "MaxDiscountAmount is required and must be greater than zero for percentage vouchers.",
                [nameof(MaxDiscountAmount)]
            );
        }

        // Flat voucher: MaxDiscountAmount cannot be negative
        if (DiscountType == DiscountType.Flat && MaxDiscountAmount < 0)
        {
            yield return new ValidationResult(
                "MaxDiscountAmount cannot be negative for flat vouchers.",
                [nameof(MaxDiscountAmount)]
            );
        }

        // DiscountAmount validation for percentage vouchers (1–100)
        if (DiscountType == DiscountType.Percentage && (DiscountAmount <= 0 || DiscountAmount > 100))
        {
            yield return new ValidationResult(
                "DiscountAmount must be between 1 and 100 for percentage vouchers.",
                [nameof(DiscountAmount)]
            );
        }

        // StartDate must be before EndDate
        if (StartDate >= EndDate)
        {
            yield return new ValidationResult(
                "StartDate must be earlier than EndDate.",
                [nameof(StartDate), nameof(EndDate)]
            );
        }

        // Optional: StartDate cannot be in the past
        if (StartDate < DateTime.UtcNow.Date)
        {
            yield return new ValidationResult(
                "StartDate cannot be in the past.",
                [nameof(StartDate)]
            );
        }
    }
}

/* public record VoucherRequest(
    string Code,
    string? Description,
    DiscountType DiscountType,
    long DiscountAmount,   // // in cents for Flat OR in percentage for Percentage type
    long MaxDiscountAmount,   // // in cents for Flat OR in percentage for Percentage type
    long MinOrderAmount,   // in cents
    int? UsageLimit,
    DateTime StartDate,
    DateTime EndDate
); */

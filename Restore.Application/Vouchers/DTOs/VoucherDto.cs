using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.DTOs;

public record VoucherDto(
    Guid Xid,
    string Code,
    string? Description,
    DiscountType DiscountType,
    long DiscountAmount,
    long? MaxDiscountAmount,
    long MinOrderAmount,
    bool IsActive,
    int? UsageLimit,
    int UsageCount,
    DateTime StartDate,
    DateTime EndDate
);

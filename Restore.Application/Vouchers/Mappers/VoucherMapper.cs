using System;
using Restore.Application.Vouchers.DTOs;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.Mappers;

public static class VoucherMapper
{

    public static VoucherDto ToDto(this Voucher voucher)
    {
        return new VoucherDto(
            voucher.Xid,
            voucher.Code,
            voucher.Description,
            voucher.DiscountType,
            voucher.DiscountAmount,
            voucher.MaxDiscountAmount,
            voucher.MinOrderAmount,
            voucher.IsActive,
            voucher.UsageLimit,
            voucher.UsageCount,
            voucher.StartDate,
            voucher.EndDate
        );
    }

    public static Voucher ToEntity(this VoucherRequest request)
    {
        return new Voucher
        {
            Code = request.Code,
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountAmount = request.DiscountAmount,
            MaxDiscountAmount = request.MaxDiscountAmount,
            MinOrderAmount = request.MinOrderAmount,
            UsageLimit = request.UsageLimit,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
    }

    // DTO → ENTITY (update existing entity)
    public static void UpdateEntity(this VoucherRequest request, Voucher voucher)
    {
        voucher.Code = request.Code;
        voucher.Description = request.Description;
        voucher.DiscountAmount = request.DiscountAmount;
        voucher.DiscountType = request.DiscountType;
        voucher.MaxDiscountAmount = request.MaxDiscountAmount;
        voucher.MinOrderAmount = request.MinOrderAmount;
        voucher.UsageLimit = request.UsageLimit;
        voucher.StartDate = request.StartDate;
        voucher.EndDate = request.EndDate;
    }

}

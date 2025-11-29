using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.UseCases;

public class UpdateVoucherUseCase(IVoucherRepository repo, IUnitOfWork uow) : IUpdateVoucherUseCase
{
    public async Task<MethodResult<VoucherDto>> ExecuteAsync(VoucherRequest voucherRequest, Guid ExtId)
    {
        /* var existing = await repo.GetByExIdAsync(xid)
                       ?? throw new KeyNotFoundException("Voucher not found."); */

        var result = await repo.GetRequiredResultAsync(ExtId);
        if (!result.Success)
            return Result.Error<VoucherDto>(result.Status, result.Error!);

        var voucher = result.Data;

        // Update allowed fields
        voucherRequest.UpdateEntity(voucher);

        // Domain rules
        if (voucher.DiscountType == DiscountType.Percentage &&
            voucher.MaxDiscountAmount is null)
            throw new ArgumentException("Percentage vouchers require MaxDiscountAmount.");

        if (voucher.EndDate <= voucher.StartDate)
            throw new ArgumentException("EndDate must be after StartDate.");

        await uow.SaveChangesAsync();

        return Result.Ok(voucher.ToDto());
    }
}

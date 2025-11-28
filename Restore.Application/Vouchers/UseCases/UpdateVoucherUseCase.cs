using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.UseCases;

public class UpdateVoucherUseCase(IVoucherRepository repo, IUnitOfWork uow) : IUpdateVoucherUseCase
{
    public async Task<VoucherDto> ExecuteAsync(VoucherRequest voucherRequest, Guid xid)
    {
        var existing = await repo.GetByExIdAsync(xid)
                       ?? throw new KeyNotFoundException("Voucher not found.");

        // Update allowed fields
        voucherRequest.UpdateEntity(existing);

        // Domain rules
        if (existing.DiscountType == DiscountType.Percentage &&
            existing.MaxDiscountAmount is null)
            throw new ArgumentException("Percentage vouchers require MaxDiscountAmount.");

        if (existing.EndDate <= existing.StartDate)
            throw new ArgumentException("EndDate must be after StartDate.");

        await uow.SaveChangesAsync();

        return existing.ToDto();
    }
}

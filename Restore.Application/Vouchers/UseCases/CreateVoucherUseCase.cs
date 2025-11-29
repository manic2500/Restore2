using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.UseCases;

public class CreateVoucherUseCase(IVoucherRepository voucherRepo, IUnitOfWork uow) : ICreateVoucherUseCase
{
    public async Task<MethodResult<VoucherDto>> ExecuteAsync(VoucherRequest request)
    {
        // Basic domain rules
        if (request.DiscountType == DiscountType.Percentage && request.DiscountAmount == 0)
            return Result.ValidationError<VoucherDto>("Percentage vouchers require MaxDiscountAmount.");

        if (request.EndDate <= request.StartDate)
            return Result.ValidationError<VoucherDto>("EndDate must be after StartDate.");

        // check if code already exists
        var existing = await voucherRepo.GetByCodeAsync(request.Code);
        if (existing != null)
            return Result.ValidationError<VoucherDto>($"Voucher code '{request.Code}' already exists.");

        var voucher = request.ToEntity();

        await voucherRepo.AddAsync(voucher);
        await uow.SaveChangesAsync();

        return Result.Ok(voucher.ToDto());
    }
}

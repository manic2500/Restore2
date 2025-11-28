using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.UseCases;

public class CreateVoucherUseCase(IVoucherRepository voucherRepo, IUnitOfWork uow) : ICreateVoucherUseCase
{
    public async Task<VoucherDto> ExecuteAsync(VoucherRequest request)
    {
        // Basic domain rules
        if (request.DiscountType == DiscountType.Percentage && request.DiscountAmount == 0)
            throw new ArgumentException("Percentage vouchers require MaxDiscountAmount.");

        if (request.EndDate <= request.StartDate)
            throw new ArgumentException("EndDate must be after StartDate.");

        // check if code already exists
        var existing = await voucherRepo.GetByCodeAsync(request.Code);
        if (existing != null)
            throw new BusinessException($"Voucher code '{request.Code}' already exists.");

        var voucher = request.ToEntity();

        await voucherRepo.AddAsync(voucher);
        await uow.SaveChangesAsync();

        return voucher.ToDto();
    }
}

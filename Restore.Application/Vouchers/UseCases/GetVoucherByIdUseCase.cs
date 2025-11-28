using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Vouchers.UseCases;

public class GetVoucherByIdUseCase(IVoucherRepository repo) : IGetVoucherByIdUseCase
{
    public async Task<VoucherDto?> ExecuteAsync(Guid xid)
    {
        var voucher = await repo.GetByExIdAsync(xid) ?? throw new VoucherNotFoundException(xid);

        return voucher.ToDto();
    }
}

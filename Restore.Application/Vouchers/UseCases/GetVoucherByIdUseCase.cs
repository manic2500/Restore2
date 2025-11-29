using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Utilities;

namespace Restore.Application.Vouchers.UseCases;

public class GetVoucherByIdUseCase(IVoucherRepository repo) : IGetVoucherByIdUseCase
{
    public async Task<MethodResult<VoucherDto>> ExecuteAsync(Guid ExtId)
    {
        //var voucher = await repo.GetByExIdAsync(xid) ?? throw new VoucherNotFoundException(xid);
        var result = await repo.GetRequiredResultAsync(ExtId);
        if (!result.Success)
            return Result.Error<VoucherDto>(result.Status, result.Error!);

        var voucher = result.Data;

        return Result.Ok(voucher.ToDto());
    }
}

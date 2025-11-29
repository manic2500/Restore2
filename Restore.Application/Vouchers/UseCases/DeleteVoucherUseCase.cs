using Restore.Application.Vouchers.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;

namespace Restore.Application.Vouchers.UseCases;

public class DeleteVoucherUseCase(IVoucherRepository repo, IUnitOfWork uow) : IDeleteVoucherUseCase
{

    public async Task<MethodResult> ExecuteAsync(Guid ExtId)
    {
        //var voucher = await repo.GetByExIdAsync(xid) ?? throw new VoucherNotFoundException(xid);
        var result = await repo.GetRequiredResultAsync(ExtId);
        if (!result.Success)
            return Result.Error(result.Status, result.Error!);

        var voucher = result.Data;

        voucher.IsDeleted = true;

        await repo.DeleteAsync(ExtId);

        await uow.SaveChangesAsync();

        return Result.Ok();
    }
}

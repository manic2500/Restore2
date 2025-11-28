using Restore.Application.Vouchers.Interfaces;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Vouchers.UseCases;

public class DeleteVoucherUseCase(IVoucherRepository repo, IUnitOfWork uow) : IDeleteVoucherUseCase
{

    public async Task ExecuteAsync(Guid xid)
    {
        var voucher = await repo.GetByExIdAsync(xid) ?? throw new VoucherNotFoundException(xid);

        voucher.IsDeleted = true;

        await repo.DeleteAsync(xid);

        await uow.SaveChangesAsync();
    }
}

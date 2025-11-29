using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.DTOs;
using Restore.Common.Utilities;


namespace Restore.Application.Vouchers.UseCases;

public class ListVouchersUseCase(IVoucherRepository repo) : IListVouchersUseCase
{
    public async Task<MethodResult<List<VoucherDto>>> ExecuteAsync()
    {
        var vouchers = await repo.GetAllAsync();

        var items = vouchers.Select(v => v.ToDto()).ToList();

        return Result.Ok(items);
    }
}

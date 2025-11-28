using Restore.Application.Vouchers.DTOs;
using Restore.Application.Vouchers.Interfaces;
using Restore.Application.Vouchers.Mappers;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.UseCases;

public class ListVouchersUseCase(IVoucherRepository repo) : IListVouchersUseCase
{
    public async Task<List<VoucherDto>> ExecuteAsync()
    {
        var vouchers = await repo.GetAllAsync();

        var items = vouchers.Select(v => v.ToDto()).ToList();

        return items;
    }
}

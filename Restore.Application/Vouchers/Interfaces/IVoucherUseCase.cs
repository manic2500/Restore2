using Restore.Application.Vouchers.DTOs;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.Interfaces;

public interface ICreateVoucherUseCase
{
    Task<VoucherDto> ExecuteAsync(VoucherRequest request);
}

public interface IGetVoucherByIdUseCase
{
    Task<VoucherDto?> ExecuteAsync(Guid xid);
}

public interface IListVouchersUseCase
{
    Task<List<VoucherDto>> ExecuteAsync();
}

public interface IUpdateVoucherUseCase
{
    Task<VoucherDto> ExecuteAsync(VoucherRequest voucher, Guid xid);
}

public interface IDeleteVoucherUseCase
{
    Task ExecuteAsync(Guid xid);
}


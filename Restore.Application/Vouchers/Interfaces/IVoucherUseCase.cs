using Restore.Application.Vouchers.DTOs;
using Restore.Common.DTOs;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.Interfaces;

public interface ICreateVoucherUseCase
{
    Task<MethodResult<VoucherDto>> ExecuteAsync(VoucherRequest request);
}

public interface IGetVoucherByIdUseCase
{
    Task<MethodResult<VoucherDto>> ExecuteAsync(Guid xid);
}

public interface IListVouchersUseCase
{
    Task<MethodResult<List<VoucherDto>>> ExecuteAsync();
}

public interface IUpdateVoucherUseCase
{
    Task<MethodResult<VoucherDto>> ExecuteAsync(VoucherRequest voucher, Guid xid);
}

public interface IDeleteVoucherUseCase
{
    Task<MethodResult> ExecuteAsync(Guid xid);
}


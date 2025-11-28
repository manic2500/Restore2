using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Vouchers.Interfaces;

public interface IVoucherRepository : IGenericRepository<Voucher>
{
    Task<Voucher?> GetByCodeAsync(string voucherCode);
}

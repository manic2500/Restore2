using Microsoft.EntityFrameworkCore;
using Restore.Application.Vouchers.Interfaces;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;

namespace Restore.Infrastructure.Persistence.Repositories;

public class VoucherRepository(StoreDbContext context) :
GenericRepository<Voucher, StoreDbContext>(context),
IVoucherRepository
{
    public async Task<Voucher?> GetByCodeAsync(string voucherCode)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Code == voucherCode);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Restore.Application.Baskets.Interfaces;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;

namespace Restore.Infrastructure.Persistence.Repositories;

public class TaxSettingRepository(StoreDbContext context) :
GenericRepository<TaxSetting, StoreDbContext>(context),
ITaxSettingRepository
{

}

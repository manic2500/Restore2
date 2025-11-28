using Restore.Application.Baskets.Interfaces;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.DbContexts;

namespace Restore.Infrastructure.Persistence.Repositories;

public class DeliverySettingRepository(StoreDbContext context) :
GenericRepository<DeliverySetting, StoreDbContext>(context),
IDeliverySettingRepository
{

}

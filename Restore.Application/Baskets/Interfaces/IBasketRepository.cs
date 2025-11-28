using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Baskets.Interfaces;

public interface IBasketRepository : IGenericRepository<Basket>
{

}
public interface ITaxSettingRepository : IGenericRepository<TaxSetting>
{

}
public interface IDeliverySettingRepository : IGenericRepository<DeliverySetting>
{

}

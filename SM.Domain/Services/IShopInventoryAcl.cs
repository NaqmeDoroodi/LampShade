using System.Collections.Generic;
using SM.Domain.OrderAgg;

namespace SM.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> items);
    }
}

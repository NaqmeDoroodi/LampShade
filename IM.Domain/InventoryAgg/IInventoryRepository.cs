using System.Collections.Generic;
using Framework.Domain;
using IM.Application.Contracts.Inventory.Models;

namespace IM.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        Inventory GetInventory(long productId);
        EditInventory GetInventoryDetails(long id);
        List<InventoryViewModel> Search (InventorySearchModel searchModel);
        List<OperationViewModel> GetOperationsLog(long inventoryId);
    }
}

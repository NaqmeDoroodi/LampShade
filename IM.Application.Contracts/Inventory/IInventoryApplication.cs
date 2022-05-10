using System.Collections.Generic;
using Framework.Application;
using IM.Application.Contracts.Inventory.Models;

namespace IM.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory inventory);
        OperationResult Edit(EditInventory inventory);
        OperationResult Increase( IncreaseInventory inventory);
        OperationResult Reduce(ReduceInventory inventory);
        OperationResult Reduce(List<ReduceInventory> inventories);
        EditInventory GetInventoryDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<OperationViewModel> GetOperationsLog(long inventoryId);
    }
}

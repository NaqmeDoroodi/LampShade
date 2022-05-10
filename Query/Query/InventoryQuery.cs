using System.Linq;
using IM.Infrastructure.EFCore;
using Query.Contracts.Inventory;
using SM.Infrastructure.EFCore;

namespace Query.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }


        public StockStatus CheckStock(IsStock command)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);

            if (inventory == null || inventory.CalcCurrentCnt() < command.Count)
            {
                var product = _shopContext.Products.Select(x => new {x.Id, x.Name}).FirstOrDefault(x => x.Id == command.ProductId)?.Name;
                return new StockStatus
                {
                    IsInStatus = false,
                    ProductName = product,
                };
            }

            return new StockStatus
            {
                IsInStatus = true,
            };
        }
    }
}

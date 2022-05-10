using System.Collections.Generic;
using System.Linq;
using IM.Application.Contracts.Inventory;
using IM.Application.Contracts.Inventory.Models;
using SM.Domain;
using SM.Domain.OrderAgg;
using SM.Domain.Services;

namespace SM.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _application;
        public ShopInventoryAcl(IInventoryApplication application)
        {
            _application = application;
        }


        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var itemsToReduce = items.Select(item => new ReduceInventory(item.ProductId, item.OrderId, item.Count, "خرید مشتری")).ToList();
            return _application.Reduce(itemsToReduce).IsSuccessful;
        }
    }
}

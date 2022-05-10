using System.Collections.Generic;
using System.Linq;
using AM.Infrastructure.EFCore;
using Framework.Application;
using Framework.Infrastructure;
using IM.Application.Contracts.Inventory.Models;
using IM.Domain.InventoryAgg;
using SM.Infrastructure.EFCore;

namespace IM.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : BaseRepository<long, Inventory>, IInventoryRepository
    {
        #region inj

        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        #endregion

        public Inventory GetInventory(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetInventoryDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = id,
                ProductId = x.ProductId,
                UnitePrice = x.UnitePrice,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
            var query = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitePrice = x.UnitePrice,
                IsInStock = x.IsInStock,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCnt = x.CalcCurrentCnt(),
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.IsInStock)
                query = query.Where(x => !x.IsInStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }

        public List<OperationViewModel> GetOperationsLog(long inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(x => new {x.Id, x.Fullname}).ToList();
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            var operations = inventory.Operations.Select(x => new OperationViewModel
            {
                Id = x.Id,
                OperatorId = x.OperatorId,
                OrderId = x.OrderId,
                Count = x.Count,
                Desc = x.Desc,
                CurrentCnt = x.CurrentCnt,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
            }).OrderByDescending(x=> x.Id).ToList();

            operations.ForEach(operation => operation.Operator = accounts.FirstOrDefault(x=>x.Id == operation.OperatorId)?.Fullname);

            return operations;
        }
    }
}
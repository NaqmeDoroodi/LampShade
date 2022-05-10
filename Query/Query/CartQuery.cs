using System;
using System.Collections.Generic;
using System.Linq;
using DM.Infrastructure.EFCore;
using Framework.Application;
using Framework.Infrastructure;
using IM.Infrastructure.EFCore;
using Query.Contracts.Order;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace Query.Query
{
    public class CartQuery : ICartQuery
    {
        #region inj

        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        private readonly InventoryContext _inventoryContext;
        public CartQuery(DiscountContext discountContext, IAuthHelper authHelper, InventoryContext inventoryContext)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
            _inventoryContext = inventoryContext;
        }

        #endregion

        public Cart ComputeCart(List<CartItem> items)
        {
            var cart = new Cart();
            var currentAccountRole = _authHelper.AccountRole();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsDeleted)
                .Select(x => new {x.DiscRate, x.ProductId})
                .ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscRate, x.ProductId })
                .ToList();
            var inventory = _inventoryContext.Inventory
                .Where(x => x.IsInStock)
                .Select(x => new {x.ProductId, x.UnitePrice})
                .ToList();

            if (currentAccountRole == Roles.Colleague)
            {
                foreach (var item in items)
                {
                    var discount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (discount != null) 
                        item.DiscRate = discount.DiscRate;
                    item.UnitePrice = inventory.FirstOrDefault(x => x.ProductId == item.Id)!.UnitePrice;
                    item.ItemTotalPrice = item.UnitePrice * item.Count;
                    item.ItemDiscountPrice = (item.ItemTotalPrice * item.DiscRate) / 100;
                    item.ItemPayPrice = item.ItemTotalPrice - item.ItemDiscountPrice;

                    cart.Add(item);
                }
            }
            else
            {
                foreach (var item in items)
                {
                    var discount = customerDiscounts.FirstOrDefault(x => x.ProductId == item.Id);
                    if (discount != null)   
                        item.DiscRate = discount.DiscRate;
                    item.UnitePrice = inventory.FirstOrDefault(x => x.ProductId == item.Id)!.UnitePrice;
                    item.ItemTotalPrice = item.UnitePrice * item.Count;
                    item.ItemDiscountPrice = (item.ItemTotalPrice * item.DiscRate) / 100;
                    item.ItemPayPrice = item.ItemTotalPrice - item.ItemDiscountPrice;

                    cart.Add(item);
                }
            }

            return cart;
        }
    }
}

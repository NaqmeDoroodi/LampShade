using System.Collections.Generic;
using System.Linq;
using AM.Infrastructure.EFCore;
using Framework.Application;
using Framework.Infrastructure;
using SM.Application.Contract.Order.Models;
using SM.Domain.OrderAgg;

namespace SM.Infrastructure.EFCore.Repositories
{
    public class OrderRepository : BaseRepository<long,Order>,IOrderRepository
    {
        #region inj

        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        #endregion


        public double GetAmountBy(long id)
        {
            return _context.Orders.Select(x => new {x.Id, x.PayPrice}).FirstOrDefault(x => x.Id == id)?.PayPrice ?? 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new {x.Id, x.Fullname}).ToList();
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountPrice = x.DiscountPrice,
                CreationDate = x.CreationDate.ToFarsi(),
                IsCanceled = x.IsCanceled,
                PayPrice = x.PayPrice,
                IsPayed = x.IsPayed,
                PaymentMethodId = x.PaymentMethodId,
                RefId = x.RefId,
                TotalPrice = x.TotalPrice,
                TrackingNum = x.TrackingNum,
            }).Where(x=>x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).ToList();

            orders.ForEach(order =>
            {
                order.AccountName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetMethodBy(order.PaymentMethodId).Name;
            });

            return orders;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);

            if (order == null) return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                Count = x.Count,
                DiscRate = x.DiscRate,
            }).ToList();

            items.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return items;
        }
    }
}

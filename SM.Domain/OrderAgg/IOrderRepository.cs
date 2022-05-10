using System.Collections.Generic;
using Framework.Domain;
using SM.Application.Contract.Order.Models;

namespace SM.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<long, Order>
    {
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
    }
}
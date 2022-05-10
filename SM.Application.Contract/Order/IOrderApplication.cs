using System.Collections.Generic;
using SM.Application.Contract.Order.Models;

namespace SM.Application.Contract.Order
{
    public interface IOrderApplication
    {
        double GetAmountBy(long id);
        long PlaceOrder(Cart cart);
        string PaymentSucceeded(long orderId, long refId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long orderId);
        void Cancel(long id);
    }
}

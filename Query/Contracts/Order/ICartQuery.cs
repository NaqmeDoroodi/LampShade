using System.Collections.Generic;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace Query.Contracts.Order
{
    public interface ICartQuery
    {
        Cart ComputeCart(List<CartItem> items);
    }
}

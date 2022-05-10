using SM.Application.Contract.Order.Models;

namespace SM.Application.Contract.Order
{
    public interface ICartService
    {
        void Set(Cart cart);
        Cart Get();
    }
}

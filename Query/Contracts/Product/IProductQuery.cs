using System.Collections.Generic;
using SM.Application.Contract.Order;
using SM.Application.Contract.Order.Models;

namespace Query.Contracts.Product
{
    public interface IProductQuery
    { 
        ProductQueryModel GetProduct(string slug);
        List<ProductQueryModel> GetLatestProducts();
        List<CartItem> CheckInventoryStatus(List<CartItem> cartItems);
        List<ProductQueryModel> Search(string searchString);
    }
}

using System.Collections.Generic;
using Framework.Domain;
using SM.Application.Contract.Product.Models;

namespace SM.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long,Product>
    {
        EditProduct GetProduct(long id);
        Product GetWithCategory(long id);
        List<ProductViewModel> Search(ProductSearchModel product);
        List<ProductViewModel> GetProducts();
    }
}

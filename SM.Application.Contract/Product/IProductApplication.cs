using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Product.Models;

namespace SM.Application.Contract.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct product);
        EditProduct GetProduct(long id);
        OperationResult Edit(EditProduct product);
        List<ProductViewModel> Search(ProductSearchModel product);
        List<ProductViewModel> GetProducts();
    }
}

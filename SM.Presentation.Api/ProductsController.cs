using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Product;

namespace SM.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQuery _query;
        public ProductsController(IProductQuery query)
        {
            _query = query;
        }


        [HttpGet]
        public List<ProductQueryModel> GetProducts()
        {
            return _query.GetLatestProducts();
        }
    }
}

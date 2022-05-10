using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Product;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        #region inj

        private readonly IProductQuery _query;

        public LatestArrivalsViewComponent(IProductQuery query)
        {
            _query = query;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var products = _query.GetLatestProducts();
            return View(products);
        }
    }
}

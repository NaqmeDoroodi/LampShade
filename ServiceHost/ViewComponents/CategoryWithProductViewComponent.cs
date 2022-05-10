using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Category;

namespace ServiceHost.ViewComponents
{
    public class CategoryWithProductViewComponent : ViewComponent
    {
        #region inj

        private readonly ICategoryQuery _query;

        public CategoryWithProductViewComponent(ICategoryQuery query)
        {
            _query = query;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var categories = _query.GetCategoriesWithProducts();
            return View(categories);
        }
    }
}

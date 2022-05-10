using Microsoft.AspNetCore.Mvc;
using Query.Contracts.Category;

namespace ServiceHost.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        #region inj

        private readonly ICategoryQuery _query;

        public CategoryViewComponent(ICategoryQuery query)
        {
            _query = query;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var category = _query.GetCategories();
            return View(category);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Query;
using Query.Contracts.ArticleCategory;
using Query.Contracts.Category;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent  : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICategoryQuery _productCategoryQuery;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery, ICategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetCategoryForMenu(),
                ProductCategories = _productCategoryQuery.GetCategoriesForMenu(),
            };
            return View(result);
        }
    }
}

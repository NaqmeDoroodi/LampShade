using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Category;

namespace ServiceHost.Pages
{
    public class CategoryModel : PageModel
    {
        #region inj

        private readonly ICategoryQuery _query;

        public CategoryModel(ICategoryQuery query)
        {
            _query = query;
        }

        #endregion

        public CategoryQueryModel Category;

        public void OnGet(string id)
        {
            Category = _query.GetCategoryWithProducts(id);
        }
    }
}

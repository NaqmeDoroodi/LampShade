using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Product;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        #region inj

        private readonly IProductQuery _query;
        public SearchModel(IProductQuery query)
        {
            _query = query;
        }

        #endregion

        public List<ProductQueryModel> SearchedProducts;
        public string SearchedString;

        public void OnGet(string searchStr)
        {
            SearchedString = searchStr;
            SearchedProducts = _query.Search(searchStr);
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        #region inj

        private readonly IArticleCategoryQuery _query;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IArticleCategoryQuery query, IArticleQuery articleQuery)
        {
            _query = query;
            _articleQuery = articleQuery;
        }

        #endregion

        public ArticleCategoryQueryModel Category;
        public List<ArticleCategoryQueryModel> Categories;
        public List<ArticleQueryModel> Articles;

        public void OnGet(string id)
        {
            Category = _query.GetCategoryWithArticles(id);
            Categories = _query.GetCategories();
            Articles = _articleQuery.GetLatestArticles();
        }
    }
}

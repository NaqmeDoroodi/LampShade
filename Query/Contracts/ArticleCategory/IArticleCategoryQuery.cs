using System.Collections.Generic;

namespace Query.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetCategoryWithArticles(string slug);
        List<ArticleCategoryQueryModel> GetCategories();
        List<ArticleCategoryQueryModel> GetCategoryForMenu();
    }
}

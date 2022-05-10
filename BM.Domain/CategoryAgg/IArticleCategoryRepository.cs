using System.Collections.Generic;
using BM.Application.Contract.Category.Models;
using Framework.Domain;

namespace BM.Domain.CategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        string GetSlugBy(long id);
        EditArticleCategory GetCategoryDetails(long id);
        List<ArticleCategoryViewModel> GetCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}

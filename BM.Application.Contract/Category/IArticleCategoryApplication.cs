using System.Collections.Generic;
using BM.Application.Contract.Category.Models;
using Framework.Application;

namespace BM.Application.Contract.Category
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory category);
        OperationResult Edit(EditArticleCategory category);
        EditArticleCategory GetCategoryDetails (long id);
        List<ArticleCategoryViewModel> GetCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}

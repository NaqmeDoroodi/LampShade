using System.Collections.Generic;
using BM.Application.Contract.Article.Models;
using Framework.Application;

namespace BM.Application.Contract.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle article);
        OperationResult Edit(EditArticle article);
        EditArticle GetArticleDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}

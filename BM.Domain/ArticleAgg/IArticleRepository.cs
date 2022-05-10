using System.Collections.Generic;
using BM.Application.Contract.Article.Models;
using Framework.Domain;

namespace BM.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long,Article>
    {
        Article GetWithCategory(long id);
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}

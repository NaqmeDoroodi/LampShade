﻿using System.Collections.Generic;

namespace Query.Contracts.Article
{
    public interface IArticleQuery
    {
        ArticleQueryModel GetArticleDetails(string slug);
        List<ArticleQueryModel> GetLatestArticles();
    }
}

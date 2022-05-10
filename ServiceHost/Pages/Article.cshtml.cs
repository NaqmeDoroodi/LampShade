using System.Collections.Generic;
using CM.Application.Contract.Comment;
using CM.Application.Contract.Comment.Models;
using CM.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Query.Contracts.Article;
using Query.Contracts.ArticleCategory;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _query;
        private readonly IArticleCategoryQuery _categoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleModel(IArticleQuery query, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication)
        {
            _query = query;
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
        }


        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> Categories;

        public void OnGet(string id)
        {
            Article = _query.GetArticleDetails(id);
            LatestArticles = _query.GetLatestArticles();
            Categories = _categoryQuery.GetCategories();
        }


        public IActionResult OnPost(AddComment comment, string articleSlug)
        {
            comment.OwnerType = CommentType.Article;
            var result = _commentApplication.Add(comment);
            return RedirectToPage("./Article", new { Id = articleSlug });
        }
    }
}

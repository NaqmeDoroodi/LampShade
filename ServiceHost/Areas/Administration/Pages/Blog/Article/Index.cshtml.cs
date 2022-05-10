using System.Collections.Generic;
using BM.Application.Contract.Article;
using BM.Application.Contract.Article.Models;
using BM.Application.Contract.Category;
using BM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IArticleApplication _service;
        private readonly IArticleCategoryApplication _articleCategoryService;

        public IndexModel(IArticleApplication service, IArticleCategoryApplication articleCategoryService)
        {
            _service = service;
            _articleCategoryService = articleCategoryService;
        }

        #endregion
        
        public ArticleSearchModel SearchModel { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public SelectList Categories;

        [NeedsPermission(BlogPermission.ArticleList)]
        public void OnGet(ArticleSearchModel model)
        {
            Categories = new SelectList(_articleCategoryService.GetCategories(), "Id", "Name");
            Articles = _service.Search(model);
        }
    }
}

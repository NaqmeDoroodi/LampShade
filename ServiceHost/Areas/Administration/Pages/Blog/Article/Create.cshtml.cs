using BM.Application.Contract.Article;
using BM.Application.Contract.Article.Models;
using BM.Application.Contract.Category;
using BM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        #region inj

        private readonly IArticleCategoryApplication _categoryService;
        private readonly IArticleApplication _service;
        public CreateModel(IArticleCategoryApplication categoryService, IArticleApplication service)
        {
            _categoryService = categoryService;
            _service = service;
        }

        #endregion

        public SelectList Categories;
        public CreateArticle Article;


        [NeedsPermission(BlogPermission.ArticleCreate)]
        public void OnGet()
        {
            Categories = new SelectList(_categoryService.GetCategories(), "Id", "Name");
        }

        [NeedsPermission(BlogPermission.ArticleCreate)]
        public IActionResult OnPost(CreateArticle article)
        {
            var result = _service.Create(article);
            return RedirectToPage("./Index");
        }
    }
}

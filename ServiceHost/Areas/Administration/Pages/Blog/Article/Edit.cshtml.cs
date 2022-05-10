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
    public class EditModel : PageModel
    {
        #region inj

        private readonly IArticleCategoryApplication _categoryService;
        private readonly IArticleApplication _service;
        public EditModel(IArticleCategoryApplication categoryService, IArticleApplication service)
        {
            _categoryService = categoryService;
            _service = service;
        }

        #endregion

        public SelectList Categories;
        public EditArticle Article;


        [NeedsPermission(BlogPermission.ArticleEdit)]
        public void OnGet(long id)
        {
            Article = _service.GetArticleDetails(id);
            Categories = new SelectList(_categoryService.GetCategories(), "Id", "Name");
        }


        [NeedsPermission(BlogPermission.ArticleEdit)]
        public IActionResult OnPost(EditArticle article)
        {
            var title = article.Title;
            var result = _service.Edit(article);
            return RedirectToPage("./Index");
        }
    }
}

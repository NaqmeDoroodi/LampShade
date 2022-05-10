using System.Collections.Generic;
using BM.Application.Contract.Category;
using BM.Application.Contract.Category.Models;
using BM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contract.Category;
using SM.Application.Contract.Category.Models;

namespace ServiceHost.Areas.Administration.Pages.Blog.Category
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IArticleCategoryApplication _service;

        public IndexModel(IArticleCategoryApplication service)
        {
            _service = service;
        }

        #endregion
        
        public ArticleCategorySearchModel SearchModel { get; set; }
        public List<ArticleCategoryViewModel> Categories { get; set; }


        [NeedsPermission(BlogPermission.CategoryList)]
        public void OnGet(ArticleCategorySearchModel model)
        {
            Categories = _service.Search(model);
        }

        [NeedsPermission(BlogPermission.CategoryCreate)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategory());
        }

        [NeedsPermission(BlogPermission.CategoryCreate)]
        public JsonResult OnPostCreate(CreateArticleCategory category)
        {
            var result = _service.Create(category);
            return new JsonResult(result);
        }

        [NeedsPermission(BlogPermission.CategoryEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var category = _service.GetCategoryDetails(id);
            return Partial("./Edit", category);
        }

        [NeedsPermission(BlogPermission.CategoryEdit)]
        public IActionResult OnPostEdit(EditArticleCategory category)
        {
            if (!ModelState.IsValid) return Partial("./Edit", category);

            var result = _service.Edit(category);
            return new JsonResult(result);
        }
    }
}

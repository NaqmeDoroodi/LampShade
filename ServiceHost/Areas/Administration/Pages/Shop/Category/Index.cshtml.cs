using System.Collections.Generic;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contract.Category;
using SM.Application.Contract.Category.Models;
using SM.Infrastructure.Configure;

namespace ServiceHost.Areas.Administration.Pages.Shop.Category
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly ICategoryApplication _service;

        public IndexModel(ICategoryApplication service)
        {
            _service = service;
        }

        #endregion
        
        public CategorySearchModel SearchModel { get; set; }
        public List<CategoryViewModel> Categories { get; set; }


        [NeedsPermission(ShopPermission.CategoryList)]
        public void OnGet(CategorySearchModel model)
        {
            Categories = _service.Search(model);
        }


        [NeedsPermission(ShopPermission.CategoryCreate)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateCategory());
        }


        [NeedsPermission(ShopPermission.CategoryCreate)]
        public JsonResult OnPostCreate(CreateCategory category)
        {
            var result = _service.Create(category);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.CategoryEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var category = _service.GetCategory(id);
            return Partial("./Edit", category);
        }


        [NeedsPermission(ShopPermission.CategoryEdit)]
        public IActionResult OnPostEdit(EditCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = _service.Edit(category);
                return new JsonResult(result);
            }

            return Partial("./Edit", category);
        }
    }
}

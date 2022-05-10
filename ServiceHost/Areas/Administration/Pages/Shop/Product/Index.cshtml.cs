using System.Collections.Generic;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Category;
using SM.Application.Contract.Product;
using SM.Application.Contract.Product.Models;
using SM.Infrastructure.Configure;

namespace ServiceHost.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly ICategoryApplication _categoryService;
        private readonly IProductApplication _productService;


        public IndexModel(ICategoryApplication categoryService, IProductApplication productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        #endregion
        
        public List<ProductViewModel> Products { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public SelectList Categories { get; set; }

        [TempData] public string Message { get; set; }



        [NeedsPermission(ShopPermission.ProductList)]
        public void OnGet(ProductSearchModel model)
        {
            Products = _productService.Search(model);
            Categories = new SelectList(_categoryService.GetCategories(), "Id", "Name");
        }


        [NeedsPermission(ShopPermission.ProductCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Categories = _categoryService.GetCategories()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(ShopPermission.ProductCreate)]
        public JsonResult OnPostCreate(CreateProduct product)
        {
            var result = _productService.Create(product);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.ProductEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var product = _productService.GetProduct(id);
            product.Categories = _categoryService.GetCategories();
            return Partial("./Edit", product);
        }


        [NeedsPermission(ShopPermission.ProductEdit)]
        public JsonResult OnPostEdit(EditProduct product)
        {
            var result = _productService.Edit(product);
            return new JsonResult(result);
        }
    }
}

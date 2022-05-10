using System.Collections.Generic;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Image;
using SM.Application.Contract.Image.Models;
using SM.Application.Contract.Product;
using SM.Infrastructure.Configure;

namespace ServiceHost.Areas.Administration.Pages.Shop.Images
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IProductApplication _productApplication;
        private readonly IImageApplication _imageApplication;
        public IndexModel(IProductApplication productApplication, IImageApplication imageApplication)
        {
            _productApplication = productApplication;
            _imageApplication = imageApplication;
        }

        #endregion


        [TempData]
        public string Message { get; set; }
        public ImageSearchModel SearchModel;
        public List<ImageViewModel> Images;
        public SelectList Products;


        [NeedsPermission(ShopPermission.ImageList)]
        public void OnGet(ImageSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Images = _imageApplication.Search(searchModel);
        }


        [NeedsPermission(ShopPermission.ImageCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateImage
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(ShopPermission.ImageCreate)]
        public JsonResult OnPostCreate(CreateImage command)
        {
            var result = _imageApplication.Create(command);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.ImageEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _imageApplication.GetImage(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", productPicture);
        }


        [NeedsPermission(ShopPermission.ImageEdit)]
        public JsonResult OnPostEdit(EditImage command)
        {
            var result = _imageApplication.Edit(command);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.ImageRemove)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _imageApplication.Remove(id);
            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


        [NeedsPermission(ShopPermission.ImageRestore)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _imageApplication.Restore(id);
            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}

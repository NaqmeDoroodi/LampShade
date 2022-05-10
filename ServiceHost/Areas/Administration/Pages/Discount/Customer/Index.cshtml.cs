using System.Collections.Generic;
using DM.Application.Contracts.Customer;
using DM.Application.Contracts.Customer.Models;
using DM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.Customer
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly ICustomerDiscApplication _discApplication;
        private readonly IProductApplication _productService;

        public IndexModel(ICustomerDiscApplication discApplication, IProductApplication productService)
        {
            _discApplication = discApplication;
            _productService = productService;
        }

        #endregion
        
        public List<CustomerDiscViewModel> Discounts { get; set; }
        public CustomerDiscSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(DiscountPermission.CustomerList)]
        public void OnGet(CustomerDiscSearchModel model)
        {
            Discounts = _discApplication.Search(model);
            Products = new SelectList(_productService.GetProducts(), "Id", "Name");
        }


        [NeedsPermission(DiscountPermission.CustomerCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateCustomerDisc
            {
                Products = _productService.GetProducts()
            };
            return Partial("./Create", command);
        }


        [NeedsPermission(DiscountPermission.CustomerCreate)]
        public JsonResult OnPostCreate(CreateCustomerDisc disc)
        {
            var result = _discApplication.CreateDiscount(disc);
            return new JsonResult(result);
        }


        [NeedsPermission(DiscountPermission.CustomerEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var disc = _discApplication.GetDiscDetails(id);
            disc.Products = _productService.GetProducts();
            return Partial("./Edit", disc);
        }


        [NeedsPermission(DiscountPermission.CustomerEdit)]
        public JsonResult OnPostEdit(EditCustomerDisc disc)
        {
            var result = _discApplication.EditDiscount(disc);
            return new JsonResult(result);
        }
    }
}

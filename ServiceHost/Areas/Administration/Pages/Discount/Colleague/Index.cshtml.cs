using System.Collections.Generic;
using DM.Application.Contracts.Colleague;
using DM.Application.Contracts.Colleague.Models;
using DM.Infrastructure.Configure;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.Colleague
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly IColleagueDiscApplication _colleagueService;
        private readonly IProductApplication _productService;

        public IndexModel(IColleagueDiscApplication colleagueService, IProductApplication productService)
        {
            _colleagueService = colleagueService;
            _productService = productService;
        }

        #endregion
        
        public List<ColleagueDiscViewModel> Discounts { get; set; }
        public ColleagueDiscSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(DiscountPermission.ColleagueList)]
        public void OnGet(ColleagueDiscSearchModel model)
        {
            Discounts = _colleagueService.Search(model);
            Products = new SelectList(_productService.GetProducts(), "Id", "Name");
        }


        [NeedsPermission(DiscountPermission.ColleagueCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateColleagueDisc
            {
                Products = _productService.GetProducts()
            };
            return Partial("./Create", command);
        }

        [NeedsPermission(DiscountPermission.ColleagueCreate)]
        public JsonResult OnPostCreate(CreateColleagueDisc disc)
        {
            var result = _colleagueService.Create(disc);
            return new JsonResult(result);
        }


        [NeedsPermission(DiscountPermission.ColleagueEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var disc = _colleagueService.GetDiscDetails(id);
            disc.Products = _productService.GetProducts();
            return Partial("./Edit", disc);
        }


        [NeedsPermission(DiscountPermission.ColleagueEdit)]
        public JsonResult OnPostEdit(EditColleagueDisc disc)
        {
            var result = _colleagueService.Edit(disc);
            return new JsonResult(result);
        }


        [NeedsPermission(DiscountPermission.ColleagueRemove)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueService.Remove(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


        [NeedsPermission(DiscountPermission.ColleagueRestore)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueService.Restore(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}

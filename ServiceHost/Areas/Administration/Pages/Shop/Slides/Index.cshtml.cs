using System.Collections.Generic;
using Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SM.Application.Contract.Slide;
using SM.Application.Contract.Slide.Models;
using SM.Infrastructure.Configure;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        #region inj

        private readonly ISlideApplication _service;

        public IndexModel(ISlideApplication service)
        {
            _service = service;
        }

        #endregion

        public List<SlideViewModel> Slides { get; set; }
        [TempData] public string Message { get; set; }


        [NeedsPermission(ShopPermission.SlideList)]
        public void OnGet()
        {
            Slides = _service.GetSlides();
        }


        [NeedsPermission(ShopPermission.SlideCreate)]
        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }


        [NeedsPermission(ShopPermission.SlideCreate)]
        public JsonResult OnPostCreate(CreateSlide slide)
        {
            var result = _service.Create(slide);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.SlideEdit)]
        public IActionResult OnGetEdit(long id)
        {
            var slide = _service.GetSlide(id);
            return Partial("./Edit", slide);
        }


        [NeedsPermission(ShopPermission.SlideEdit)]
        public JsonResult OnPostEdit(EditSlide slide)
        {
            var result = _service.Edit(slide);
            return new JsonResult(result);
        }


        [NeedsPermission(ShopPermission.SlideRemove)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _service.Remove(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }


        [NeedsPermission(ShopPermission.SlideRestore)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _service.Restore(id);

            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
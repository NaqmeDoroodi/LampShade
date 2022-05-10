using Microsoft.AspNetCore.Mvc;
using Query.Contracts;
using Query.Contracts.Slide;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        #region inj

        private readonly ISlideQuery _query;

        public SlideViewComponent(ISlideQuery query)
        {
            _query = query;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var slides = _query.GetSlides();
            return View(slides);
        }
    }
}

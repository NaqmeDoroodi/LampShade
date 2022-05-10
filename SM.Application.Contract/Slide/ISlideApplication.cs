using System.Collections.Generic;
using Framework.Application;
using SM.Application.Contract.Slide.Models;

namespace SM.Application.Contract.Slide
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide slide);
        OperationResult Edit(EditSlide slide);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        EditSlide GetSlide(long id);
        List<SlideViewModel> GetSlides();
    }
}

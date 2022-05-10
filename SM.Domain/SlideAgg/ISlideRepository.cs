using System.Collections.Generic;
using Framework.Domain;
using SM.Application.Contract.Slide.Models;

namespace SM.Domain.SlideAgg
{
    public interface ISlideRepository: IRepository<long, Slide>
    {
        EditSlide GetSlide(long id);
        List<SlideViewModel> GetSlides();
    }
}

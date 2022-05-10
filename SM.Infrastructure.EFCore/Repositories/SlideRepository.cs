using System.Collections.Generic;
using System.Linq;
using Framework.Application;
using Framework.Infrastructure;
using SM.Application.Contract.Slide.Models;
using SM.Domain.SlideAgg;

namespace SM.Infrastructure.EFCore.Repositories
{
    public class SlideRepository : BaseRepository<long, Slide>, ISlideRepository
    {
        #region inj

        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public EditSlide GetSlide(long id)
        {

            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                BtnText = x.BtnText,
                Link = x.Link,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetSlides()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Img = x.Img,
                Heading = x.Heading,
                Title = x.Title,
                IsDeleted = x.IsDeleted,
                CreationDate = x.CreationDate.ToFarsi(),
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}

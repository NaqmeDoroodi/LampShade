using System.Collections.Generic;
using System.Linq;
using Query.Contracts.Slide;
using SM.Infrastructure.EFCore;

namespace Query.Query
{
    public class SlideQuery : ISlideQuery
    {
        #region inj

        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        #endregion


        public List<SlideQueryModel> GetSlides()
        {
            return _context.Slides.Where(x => x.IsDeleted == false).Select(x => new SlideQueryModel
            {
                Img = x.Img,
                ImgAlt = x.ImgAlt,
                ImgTitle = x.ImgTitle,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                BtnText = x.BtnText,
                Link = x.Link,
            }).ToList();
        }
    }
}

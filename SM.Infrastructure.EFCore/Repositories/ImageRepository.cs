using System.Collections.Generic;
using System.Linq;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contract.Image.Models;
using SM.Domain.ImageAgg;

namespace SM.Infrastructure.EFCore.Repositories
{
    public class ImageRepository : BaseRepository<long, Image>, IImageRepository
    {
         #region inj

        private readonly ShopContext _context;

        public ImageRepository(ShopContext context) : base (context)
        {
            _context = context;
        }

        #endregion

        public EditImage GetImage(long id)
        {
            return _context.Images.Select(x => new EditImage
            {
                Id = x.Id,
                ImgTitle = x.ImgTitle,
                ImgAlt = x.ImgAlt,
                ProductId = x.ProductId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public Image GetWithProductAndCategory(long id)
        {
            return _context.Images.Include(x => x.Product).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ImageViewModel> Search(ImageSearchModel img)
        {
            var query = _context.Images
                .Include(x => x.Product)
                .Select(x=> new ImageViewModel
                {
                    Id =x.Id,
                    Img = x.Img,
                    IsDeleted = x.IsDeleted,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Product = x.Product.Name,
                    ProductId = x.ProductId,
                });

            if(img.ProductId != 0)
                query = query.Where(x=>x.ProductId == img.ProductId);

            return query.OrderByDescending(c => c.Id).ToList();
        }
    }
}

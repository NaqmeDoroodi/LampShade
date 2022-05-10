using System.Collections.Generic;
using Framework.Domain;
using SM.Application.Contract.Image.Models;

namespace SM.Domain.ImageAgg
{
    public interface IImageRepository : IRepository<long, Image>
    {
        EditImage GetImage(long id);
        Image GetWithProductAndCategory(long id);
        List<ImageViewModel> Search(ImageSearchModel img);
    }
}

using Framework.Domain;
using SM.Domain.ProductAgg;

namespace SM.Domain.ImageAgg
{
    public class Image : BaseEntity
    {
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public bool IsDeleted { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }


        public Image(string img, string imgAlt, string imgTitle, long productId)
        {
            Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            ProductId = productId;
            IsDeleted = false;
        }


        public void Edit(string img, string imgAlt, string imgTitle, long productId)
        {
            if (!string.IsNullOrWhiteSpace(img)) Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            ProductId = productId;
        }

        public void Remove()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
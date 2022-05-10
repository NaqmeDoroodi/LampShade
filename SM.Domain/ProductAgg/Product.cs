using System.Collections.Generic;
using Framework.Domain;
using SM.Domain.CategoryAgg;
using SM.Domain.ImageAgg;

namespace SM.Domain.ProductAgg
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public string ShortDesc { get; private set; }
        public string Desc { get; private set; }
        public string MetaDesc { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public long CategoryId { get; private set; }
        public Category Category { get; private set; }
        public List<Image> Images { get; private set; }


        internal Product()
        {
            Images = new List<Image>();
        }

        public Product(string name, string code, string img, string imgAlt, string imgTitle, string shortDesc,
            string desc, string metaDesc, string slug, string keywords, long categoryId)
        {
            Name = name;
            Code = code;
            Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            ShortDesc = shortDesc;
            Desc = desc;
            MetaDesc = metaDesc;
            Slug = slug;
            Keywords = keywords;
            CategoryId = categoryId;
        }

        public void Edit(string name, string code, string img, string imgAlt, string imgTitle, string shortDesc,
            string desc, string metaDesc, string slug, string keywords, long categoryId)
        {
            Name = name;
            Code = code;
            if (!string.IsNullOrWhiteSpace(img)) Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            ShortDesc = shortDesc;
            Desc = desc;
            MetaDesc = metaDesc;
            Slug = slug;
            Keywords = keywords;
            CategoryId = categoryId;
        }
    }
}
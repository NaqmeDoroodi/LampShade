using System.Collections.Generic;
using Framework.Domain;
using SM.Domain.ProductAgg;

namespace SM.Domain.CategoryAgg
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDesc { get; private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; set; }


        public Category()
        {
            Products = new List<Product>();
        }

        public Category(string name, string desc, string img, string imgAlt, string imgTitle, string keywords, string metaDesc, string slug)
        {
            Name = name;
            Desc = desc;
            Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            Keywords = keywords;
            MetaDesc = metaDesc;
            Slug = slug;
        }


        public void Edit(string name, string desc, string img, string imgAlt, string imgTitle, string keywords, string metaDesc, string slug)
        {
            Name = name;
            Desc = desc;
            if(!string.IsNullOrWhiteSpace(img)) Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            Keywords = keywords;
            MetaDesc = metaDesc;
            Slug = slug;
        }

    }
}

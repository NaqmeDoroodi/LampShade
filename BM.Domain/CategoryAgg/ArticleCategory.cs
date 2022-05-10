using System.Collections.Generic;
using BM.Domain.ArticleAgg;
using Framework.Domain;

namespace BM.Domain.CategoryAgg
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; private set; }
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public string Desc { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDesc { get; private set; }
        public string CanonicalAddress { get; private set; }
        public List<Article> Articles { get; private set; }

        public ArticleCategory(string name, string img, string imgAlt, string imgTitle, string desc, int showOrder, string slug, string keywords,
            string metaDesc, string canonicalAddress)
        {
            Name = name;
            Img = img;
            Desc = desc;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDesc = metaDesc;
            CanonicalAddress = canonicalAddress;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
        }

        public void Edit(string name, string img, string imgAlt, string imgTitle, string desc, int showOrder, string slug, string keywords,
            string metaDesc, string canonicalAddress)
        {
            Name = name;
            if (!string.IsNullOrWhiteSpace(img)) Img = img;
            Desc = desc;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDesc = metaDesc;
            CanonicalAddress = canonicalAddress;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
        }
    }
}
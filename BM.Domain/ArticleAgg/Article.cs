using System;
using BM.Domain.CategoryAgg;
using Framework.Domain;

namespace BM.Domain.ArticleAgg
{
    public class Article : BaseEntity
    {
        public string Title { get; private set; }
        public string ShortDesc { get; private set; }
        public string Desc { get; private set; }
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Slug { get; private set; }
        public string MetaDesc { get; private set; }
        public string Keywords { get; private set; }
        public string CanonicalAddress { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory Category { get; private set; }


        public Article(string title, string shortDesc, string desc, string img, string imgAlt, string imgTitle,
            DateTime publishDate, string slug, string metaDesc, string keywords, string canonicalAddress,
            long categoryId)
        {
            Title = title;
            ShortDesc = shortDesc;
            Desc = desc;
            Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            PublishDate = publishDate;
            Slug = slug;
            MetaDesc = metaDesc;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
        }


        public void Edit(string title, string shortDesc, string desc, string img, string imgAlt, string imgTitle,
            DateTime publishDate, string slug, string metaDesc, string keywords, string canonicalAddress,
            long categoryId)
        {
            Title = title;
            ShortDesc = shortDesc;
            Desc = desc;
            if (!string.IsNullOrWhiteSpace(img)) Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            PublishDate = publishDate;
            Slug = slug;
            MetaDesc = metaDesc;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
        }
    }
}
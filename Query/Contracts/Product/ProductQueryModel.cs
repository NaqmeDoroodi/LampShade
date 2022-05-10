using System.Collections.Generic;
using Query.Contracts.Comment;

namespace Query.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Price { get; set; }
        public double PriceDouble { get; set; }
        public bool HasDisc { get; set; }
        public string PriceWithDisc { get; set; }
        public double PriceWithDiscDouble { get; set; }
        public int DiscRate { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string ShortDesc { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsInStock { get; set; }
        public string MetaDesc { get; set; }

        public List<ImageQueryModel> Images { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
    }
}

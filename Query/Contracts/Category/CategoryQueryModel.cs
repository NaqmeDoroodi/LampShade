using System.Collections.Generic;
using Query.Contracts.Product;

namespace Query.Contracts.Category
{
    public class CategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Desc { get; set; }
        public string Keywords { get; set; }
        public string MetaDesc { get; set; }
        public string Slug { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
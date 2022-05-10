using System.Collections.Generic;
using Query.Contracts.Article;

namespace Query.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public string Name { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string Desc { get; set; }
        public int ShowOrder { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordsList { get; set; }
        public string MetaDesc { get; set; }
        public string CanonicalAddress { get; set; }
        public int ArticleCnt { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
    }
}
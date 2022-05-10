using System.Collections.Generic;
using Query.Contracts.Comment;

namespace Query.Contracts.Article
{
    public class ArticleQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public string PublishDate { get; set; }
        public string Slug { get; set; }
        public string MetaDesc { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public string CanonicalAddress { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
    }
}
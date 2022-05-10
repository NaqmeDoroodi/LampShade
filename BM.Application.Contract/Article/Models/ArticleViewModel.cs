namespace BM.Application.Contract.Article.Models
{
    public class ArticleViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Img { get; set; }
        public string PublishDate { get; set; }
        public long CategoryId { get; set; }
        public string Category { get; set; }
    }
}
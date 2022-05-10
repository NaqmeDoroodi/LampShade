namespace BM.Application.Contract.Category.Models
{
    public class ArticleCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Desc { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }
        public long ArticlesCnt { get; set; }
    }
}
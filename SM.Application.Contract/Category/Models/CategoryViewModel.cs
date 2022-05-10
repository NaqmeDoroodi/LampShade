namespace SM.Application.Contract.Category.Models
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }
        public string CreationDate { get; set; }
        public long ProductCnt { get; set; }
    }
}

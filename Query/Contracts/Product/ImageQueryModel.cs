namespace Query.Contracts.Product
{
    public class ImageQueryModel
    {
        public string Img { get; set; }
        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }
        public bool IsDeleted { get; set; }
        public long ProductId { get; set; }
    }
}
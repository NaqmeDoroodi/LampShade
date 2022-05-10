namespace SM.Application.Contract.Image.Models
{
    public class ImageViewModel
    {
        public long Id { get; set; }
        public string Img { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationDate { get; set; }
        public string Product { get; set; }
        public long ProductId { get; set; }
    }
}
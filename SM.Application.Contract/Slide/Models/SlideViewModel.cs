namespace SM.Application.Contract.Slide.Models
{
    public class SlideViewModel
    {
        public long Id { get; set; }
        public string Img { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public string CreationDate { get; set; }
    }
}
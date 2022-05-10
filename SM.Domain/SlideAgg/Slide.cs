using Framework.Domain;

namespace SM.Domain.SlideAgg
{
    public class Slide : BaseEntity
    {
        public string Img { get; private set; }
        public string ImgAlt { get; private set; }
        public string ImgTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string BtnText { get; private set; }
        public string Link { get; private set; }
        public bool IsDeleted { get; private set; }


        public Slide(string img, string imgAlt, string imgTitle, string heading, string title, string text,
            string btnText, string link)
        {
            Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
            IsDeleted = false;
        }


        public void Edit(string img, string imgAlt, string imgTitle, string heading, string title, string text,
            string btnText, string link)
        {
            if (!string.IsNullOrWhiteSpace(img)) Img = img;
            ImgAlt = imgAlt;
            ImgTitle = imgTitle;
            Heading = heading;
            Title = title;
            Text = text;
            BtnText = btnText;
            Link = link;
        }


        public void Remove()
        {
            IsDeleted = true;
        }


        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
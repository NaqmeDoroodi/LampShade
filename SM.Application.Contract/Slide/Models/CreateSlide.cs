using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Contract.Slide.Models
{
    public class CreateSlide
    {
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[]{".jpeg",".png",".jpj"},ErrorMessage = ValidationMessage.FileExtensionLimit)]
        public IFormFile Img { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgAlt { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Heading { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Text { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string BtnText { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Link { get; set; }
    }
}

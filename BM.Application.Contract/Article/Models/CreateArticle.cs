using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BM.Application.Contract.Article.Models
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Title { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ShortDesc { get; set; }


        public string Desc { get; set; }


        [FileExtensionLimitation(new string[] {".jpeg", ".jpg", "png"}, ErrorMessage = ValidationMessage.FileExtensionLimit)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Img { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgAlt { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string PublishDate { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Slug { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string MetaDesc { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Keywords { get; set; }


        public string CanonicalAddress { get; set; }


        [Range(1,long.MaxValue,ErrorMessage = ValidationMessage.Required)]
        public long CategoryId { get; set; }
    }
}
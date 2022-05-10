using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BM.Application.Contract.Category.Models
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Name { get; set; }


        [FileExtensionLimitation(new string[]{".jpeg",".jpg",".png"},ErrorMessage = ValidationMessage.FileExtensionLimit)]
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Img { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgAlt { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Desc { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public int ShowOrder { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Slug { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string MetaDesc { get; set; }


        public string CanonicalAddress { get; set; }
    }
}
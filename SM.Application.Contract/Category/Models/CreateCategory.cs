using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace SM.Application.Contract.Category.Models
{
    public class CreateCategory
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        [MinLength(3, ErrorMessage = ValidationMessage.MinLength)]
        public string Name { get; set; }


        [MinLength(3, ErrorMessage = ValidationMessage.MinLength)]
        public string Desc { get; set; }


        [FileExtensionLimitation(new string[]{".jpeg",".jpg",".png"},ErrorMessage = ValidationMessage.FileExtensionLimit)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Img { get; set; }


        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        public string ImgAlt { get; set; }


        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string MetaDesc { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        [MaxLength(255, ErrorMessage = ValidationMessage.MaxLength)]
        [MinLength(3, ErrorMessage = ValidationMessage.MinLength)]
        public string Slug { get; set; }
    }
}
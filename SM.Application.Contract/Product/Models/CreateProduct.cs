using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using SM.Application.Contract.Category.Models;

namespace SM.Application.Contract.Product.Models
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Name { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Code { get; set; }


        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtensionLimitation(new string[] {".jpg", ".jpeg", ".png"}, ErrorMessage = ValidationMessage.FileExtensionLimit)]
        public IFormFile Img { get; set; }

        public string ImgAlt { get; set; }
        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ShortDesc { get; set; }


        public string Desc { get; set; }
        public string MetaDesc { get; set; }
        public string Slug { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public long CategoryId { get; set; }


        public List<CategoryViewModel> Categories { get; set; } //for select list
    }
}
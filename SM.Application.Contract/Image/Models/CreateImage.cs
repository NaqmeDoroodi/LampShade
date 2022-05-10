using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;
using SM.Application.Contract.Product.Models;

namespace SM.Application.Contract.Image.Models
{
    public class CreateImage
    {
        [FileExtensionLimitation(new string[]{".jpeg",".jpg",".png"},ErrorMessage = ValidationMessage.FileExtensionLimit)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Img { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgAlt { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ImgTitle { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        [Range(1, 100000, ErrorMessage = ValidationMessage.Required)]
        public long ProductId { get; set; }


        public List<ProductViewModel> Products { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Framework.Application
{
    public class FileExtensionLimitationAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions;

        public FileExtensionLimitationAttribute(string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }


        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtension = Path.GetExtension(file.FileName);
            return _validExtensions.Contains(fileExtension);
        }
    }
}

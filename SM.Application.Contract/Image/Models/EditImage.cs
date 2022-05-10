using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace SM.Application.Contract.Image.Models
{
    public class EditImage : CreateImage
    {
        public long Id { get; set; }
    }
}
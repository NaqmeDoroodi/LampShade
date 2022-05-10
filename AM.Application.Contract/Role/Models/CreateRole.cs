using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace AM.Application.Contract.Role.Models
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Name { get; set; }

        public List<int> Permissions { get; set; }
    }
}

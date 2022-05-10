using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AM.Application.Contract.Role.Models;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace AM.Application.Contract.Account.Models
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessage.Required)]
        public string MobileNum { get; set; }

        public IFormFile ProfileImg { get; set; }

        public int RoleId { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
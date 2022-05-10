using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using SM.Application.Contract.Product.Models;

namespace DM.Application.Contracts.Customer.Models
{
    public class CreateCustomerDisc
    {
        [Range(1,100000,ErrorMessage = ValidationMessage.Required)]
        public long ProductId { get; set; }


        [Range(1, 99, ErrorMessage = ValidationMessage.Required)]
        public int DiscRate { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string StartDate { get; set; }


        [Required(ErrorMessage = ValidationMessage.Required)]
        public string EndDate { get; set; }


        public string Reason { get; set; }


        public List<ProductViewModel> Products { get; set; }
    }
}
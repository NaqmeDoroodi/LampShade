using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using SM.Application.Contract.Product.Models;

namespace DM.Application.Contracts.Colleague.Models
{
    public class CreateColleagueDisc
    {
        [Range(1, 100000, ErrorMessage = ValidationMessage.Required)]
        public long ProductId { get; set; }


        [Range(1, 99, ErrorMessage = ValidationMessage.Required)]
        public int DiscRate { get; set; }


        public List<ProductViewModel> Products { get; set; }
    }
}

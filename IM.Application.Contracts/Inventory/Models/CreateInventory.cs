using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using SM.Application.Contract.Product.Models;

namespace IM.Application.Contracts.Inventory.Models
{
    public class CreateInventory
    {
        [Range(1,100000,ErrorMessage = ValidationMessage.Required)]
        public long ProductId { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = ValidationMessage.Required)]
        public double UnitePrice { get; set; }


        public List<ProductViewModel> Products { get; set; }
    }
}
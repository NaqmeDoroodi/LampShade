using System.Collections.Generic;

namespace SM.Application.Contract.Order.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayPrice { get; set; }
        public int PaymentMethodId { get; set; }



        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            Items.Add(item);

            TotalPrice += item.ItemTotalPrice;
            DiscountPrice += item.ItemDiscountPrice;
            PayPrice += item.ItemPayPrice;
        }

        public void SetPaymentMethod(int methodId)
        {
            PaymentMethodId = methodId;
        }
    }
}
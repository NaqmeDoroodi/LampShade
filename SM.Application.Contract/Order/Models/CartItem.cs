namespace SM.Application.Contract.Order.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public int Count { get; set; }
        public double UnitePrice { get; set; }
        public bool IsInStock { get; set; }
        public int DiscRate { get; set; }
        public double ItemTotalPrice { get; set; }
        public double ItemDiscountPrice { get; set; }
        public double ItemPayPrice { get; set; }


        public void CalcItemTotalPrice()
        {
            ItemTotalPrice = UnitePrice * Count;
        }
    }
}

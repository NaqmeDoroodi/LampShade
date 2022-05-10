namespace SM.Application.Contract.Order.Models
{
    public class OrderItemViewModel
    {
        public long ProductId { get; set; }
        public string Product { get; set; }
        public double UnitPrice { get; set; }
        public int DiscRate { get; set; }
        public int Count { get; set; }
        public long OrderId { get; set; }
    }
}
namespace SM.Application.Contract.Order.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayPrice { get; set; }
        public bool IsPayed { get; set; }
        public bool IsCanceled { get; set; }
        public string TrackingNum { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
        public long RefId { get; set; }
        public string CreationDate { get; set; }
    }
}
namespace SM.Application.Contract.Order.Models
{
    public class OrderSearchModel
    {
        public bool IsCanceled { get; set; }
        public long AccountId { get; set; }
    }
}
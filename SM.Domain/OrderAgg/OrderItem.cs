using Framework.Domain;

namespace SM.Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public int DiscRate { get; private set; }
        public int Count { get; private set; }
        public long OrderId { get; private set; }
        public Order Order { get; private set; }


        public OrderItem(long productId, double unitPrice, int discRate, int count)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            DiscRate = discRate;
            Count = count;
        }
    }
}
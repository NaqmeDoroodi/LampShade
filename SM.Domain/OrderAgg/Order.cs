using System.Collections.Generic;
using Framework.Domain;

namespace SM.Domain.OrderAgg
{
    public class Order : BaseEntity
    {
        public long AccountId { get; private set; }
        public double TotalPrice { get; private set; }
        public double DiscountPrice { get; private set; }
        public double PayPrice { get; private set; }
        public bool IsPayed { get; private set; }
        public bool IsCanceled { get; private set; }
        public string TrackingNum { get; private set; }
        public int PaymentMethodId { get; private set; }
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }


        public Order(long accountId, int paymentMethodId, double totalPrice, double discountPrice, double payPrice)
        {
            AccountId = accountId;
            PaymentMethodId = paymentMethodId;
            TotalPrice = totalPrice;
            DiscountPrice = discountPrice;
            PayPrice = payPrice;
            IsPayed = false;
            IsCanceled = false;
            RefId = 0;
            Items = new List<OrderItem>();
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void Succeeded(long refId)
        {
            if (refId != 0) RefId = refId;
            IsPayed = true;
        }

        public void Add(OrderItem item)
        {
            Items.Add(item);
        }

        public void SetTrackingNum(string number)
        {
            TrackingNum = number;
        }
    }
}

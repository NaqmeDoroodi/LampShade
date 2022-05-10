using System;
using Framework.Domain;

namespace DM.Domain.CustomerAgg
{
    public class CustomerDisc : BaseEntity
    {
        public long ProductId { get; private set; }
        public int DiscRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }


        public CustomerDisc(long productId, int discRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscRate = discRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }


        public void Edit(long productId, int discRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscRate = discRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}

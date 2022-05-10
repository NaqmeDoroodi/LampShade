namespace IM.Application.Contracts.Inventory.Models
{
    public class OperationViewModel
    {
        public long Id { get; set; }
        public bool Operation { get; set; }
        public string OperationDate { get; set; }
        public long Count { get; set; }
        public long CurrentCnt { get; set; }
        public string Desc { get; set; }
        public long OperatorId { get; set; }
        public string Operator { get; set; }
        public long OrderId { get; set; }

    }
}

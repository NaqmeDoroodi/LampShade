using System;

namespace IM.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long Count { get; private set; }
        public long CurrentCnt { get; private set; }
        public string Desc { get; private set; }
        public long OperatorId { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }



        protected InventoryOperation(){}

        public InventoryOperation(bool operation, long count, long currentCnt, string desc, long operatorId,
            long orderId, long inventoryId)
        {
            Operation = operation;
            OperationDate = DateTime.Now;
            Count = count;
            CurrentCnt = currentCnt;
            Desc = desc;
            OperatorId = operatorId;
            OrderId = orderId;
            InventoryId = inventoryId;
        }
    }
}
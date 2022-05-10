namespace IM.Application.Contracts.Inventory.Models
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public long Count { get; set; }
        public string Desc { get; set; }


        public ReduceInventory()
        {
        }

        public ReduceInventory(long productId, long orderId, long count, string desc)
        {
            ProductId = productId;
            OrderId = orderId;
            Count = count;
            Desc = desc;
        }
    }
}
namespace IM.Application.Contracts.Inventory.Models
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Desc { get; set; }
    }
}

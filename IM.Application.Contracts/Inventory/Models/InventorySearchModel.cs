namespace IM.Application.Contracts.Inventory.Models
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool IsInStock { get; set; }
    }
}
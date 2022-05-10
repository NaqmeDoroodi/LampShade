namespace IM.Application.Contracts.Inventory.Models
{
    public class InventoryViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public double UnitePrice { get; set; }
        public long CurrentCnt { get; set; }
        public string CreationDate { get; set; }
        public bool IsInStock { get; set; }
    }
}
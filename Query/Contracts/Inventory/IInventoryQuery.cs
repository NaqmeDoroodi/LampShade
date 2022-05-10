namespace Query.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(IsStock command);
    }
}

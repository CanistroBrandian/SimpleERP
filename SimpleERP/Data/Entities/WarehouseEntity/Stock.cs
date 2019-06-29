namespace SimpleERP.Data.Entities.WarehouseEntity
{
    public class Stock
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Count { get; set; }
    }
}

using SimpleERP.Data.Entities.WarehouseEntity;

namespace SimpleERP.Data.Entities.OrderEntity
{
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}

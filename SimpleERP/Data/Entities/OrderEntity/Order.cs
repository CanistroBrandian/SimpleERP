using SimpleERP.Abstract;
using SimpleERP.Data.Entities.Auth;
using System.Collections.Generic;

namespace SimpleERP.Data.Entities.OrderEntity
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public List<ClientOrder> ClientOrders { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string Information { get; set; }
    }
}

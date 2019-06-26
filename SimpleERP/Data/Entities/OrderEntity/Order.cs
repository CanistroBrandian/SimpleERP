using SimpleERP.Abstract;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

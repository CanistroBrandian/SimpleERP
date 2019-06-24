using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.OrderEntity
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

using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.OrderEntity
{
    public class Order
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int ClientId { get; set; }
        public List<ClientOrder> ClientOrders { get; set; }
        public int EmployeId { get; set; }
        public List<EmployeClient> EmployeClients { get; set; }
        public int ProductId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string Information { get; set; }
    }
}

using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.OrderEntity
{
    public class OrderProduct 
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId {  get; set; }
        public Order Order {  get; set; }
    }
}

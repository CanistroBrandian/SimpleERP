using SimpleERP.Abstract;
using SimpleERP.Data.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Entities.WarehouseEntity
{
    public class Product: IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
    public class OrderRepository : CommonRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ContextEF context) : base(context)
        {
        }
    }
}

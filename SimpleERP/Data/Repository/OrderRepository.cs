using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
    public class OrderRepository : CommonRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ContextEF context) : base(context)
        {
        }
    }
}

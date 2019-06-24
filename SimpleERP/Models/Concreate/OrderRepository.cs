using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.OrderEntity;
using SimpleERP.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Concreate
{
    public class OrderRepository : CommonRepository<Order, int>, IOrderRepository
    {
        private readonly ContextEF _context;
        public OrderRepository(ContextEF context) : base(context)
        {
            _context = context;
        }
    }
}

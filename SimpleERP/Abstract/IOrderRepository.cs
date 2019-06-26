using SimpleERP.Data.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
    public interface IOrderRepository : ICommonRepository<Order, int>
    {
    }
}

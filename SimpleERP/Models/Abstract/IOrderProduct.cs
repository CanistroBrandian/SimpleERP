using SimpleERP.Models.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IOrderProduct : ICommonRepository<OrderProduct, int>
    {
    }
}

using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Abstract
{
   public interface IProductRepository : ICommonRepository<Product, int>
    {
       
    }
}

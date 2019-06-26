using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
   public class ProductRepository : CommonRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ContextEF context) : base(context)
        {
        }
    }
}

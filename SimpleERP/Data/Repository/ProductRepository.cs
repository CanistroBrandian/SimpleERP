using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
   public class ProductRepository : CommonRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ContextEF context) : base(context)
        {
        }
    }
}

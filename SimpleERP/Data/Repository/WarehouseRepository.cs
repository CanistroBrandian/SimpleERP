using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Data.Repository
{
    public class WarehouseRepository : CommonRepository<Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(ContextEF context) : base(context)
        {
            
        }
    }
}
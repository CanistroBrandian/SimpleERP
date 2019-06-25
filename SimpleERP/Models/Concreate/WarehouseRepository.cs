using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;
using System.Collections.Generic;
using System.Linq;

namespace SimpleERP.Models.Concreate
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IEmployeOrders _context;
        public WarehouseRepository(IEmployeOrders context)
        {
            _context = context;
        }

        public List<Warehouse> GetWarehouses()
        {
            return _context.Warehouses.ToList();
        }
    }
}

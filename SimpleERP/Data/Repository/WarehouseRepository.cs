using Microsoft.EntityFrameworkCore;
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

        public async Task AddProductToWarehouse(Stock stock)
        {
            _context.Set<Stock>().Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetAllWarehouseStocks(int warhouseId)
        {
           return await _context.Set<Stock>().AsNoTracking().Where(s => s.WarehouseId == warhouseId).ToListAsync();
        }
    }
}
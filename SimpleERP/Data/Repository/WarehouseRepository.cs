using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.WarehouseEntity;
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

        public async Task<List<Stock>> GetAllStocksAsync()
        {
            return await _context.Set<Stock>().AsNoTracking().ToListAsync();
        }

    }
}
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Models.API.Warehouse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
    public interface IWarehouseRepository : ICommonRepository<Warehouse, int>
    {
        Task AddProductToWarehouse(Stock stock);
        Task<List<Stock>> GetAllStocksAsync();
    }
}

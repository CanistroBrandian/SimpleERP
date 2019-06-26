using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Models.Repository
{
    public class WarehouseRepository : CommonRepository<Warehouse, int>, IWarehouseRepository
    {
        public WarehouseRepository(ContextEF context) : base(context)
        {
        }
    }
}

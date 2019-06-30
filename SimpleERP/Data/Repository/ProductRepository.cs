using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.WarehouseEntity;

namespace SimpleERP.Data.Repository
{
    public class ProductRepository : CommonRepository<Product, int>, IProductRepository
    {
        public ProductRepository(ContextEF context) : base(context)
        {
        }
    }
}

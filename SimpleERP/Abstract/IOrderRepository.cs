using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Models.API.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
    public interface IOrderRepository : ICommonRepository<Order, int>
    {
        Task AddOrderWithProducts(OrderProduct model);
        Task<List<OrderProduct>> GetAllOrderProducts();

    }
}

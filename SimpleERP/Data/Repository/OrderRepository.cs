using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Models.API.Order;

namespace SimpleERP.Data.Repository
{
    public class OrderRepository : CommonRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ContextEF context) : base(context)
        {
        }

        public async Task AddOrderWithProducts(OrderProduct model)
        {
            _context.Set<OrderProduct>().Add(model);
            await _context.SaveChangesAsync();
        }
        public async Task<List<OrderProduct>> GetAllOrderProducts()
        {
            return await _context.Set<OrderProduct>().AsNoTracking().ToListAsync();
        }

    }
}


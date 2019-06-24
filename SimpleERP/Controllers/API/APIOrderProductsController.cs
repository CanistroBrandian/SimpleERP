using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.API.Order;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.OrderEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/orderproduct")]
    [ApiController]
    public class APIOrderProductsController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIOrderProductsController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIOrderProducts
        [HttpGet]
        public async Task<IActionResult> GetOrderProducts()
        {
            return Ok(await _context.OrderProducts.Select(s => new OrderProduct
            {
                OrderId = s.OrderId,
                ProductId = s.ProductId
            }).ToListAsync());
        }

        // GET: api/APIOrderProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderProduct = await _context.OrderProducts.FindAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(new OrderProduct
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId
            });
        }

        // PUT: api/APIOrderProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProduct([FromRoute] int id, [FromBody] OrderProductModel model)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderProduct.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/APIOrderProducts
        [HttpPost]
        public async Task<IActionResult> PostOrderProduct([FromBody] OrderProductModel model)
        {
            var orderProduct = new OrderProduct
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderProducts.Add(orderProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderProductExists(orderProduct.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderProduct", new { id = orderProduct.OrderId }, orderProduct);
        }

        // DELETE: api/APIOrderProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderProduct = await _context.OrderProducts.FindAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();

            return Ok(orderProduct);
        }

        private bool OrderProductExists(int id)
        {
            return _context.OrderProducts.Any(e => e.OrderId == id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.API.Warehouse;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/stock")]
    [ApiController]
    public class APIStocksController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIStocksController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIStocks
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            return Ok(await _context.Stocks.Select(s => new Stock
            {
                WarehouseId = s.WarehouseId,
                ProductId = s.ProductId
            }).ToListAsync());
        }

        // GET: api/APIStocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(new Stock {
                WarehouseId = stock.WarehouseId,
                ProductId = stock.ProductId
            });
        }

        // PUT: api/APIStocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock([FromRoute] int id, [FromBody] StockModel model)
        {
            var stock = new Stock
            {
                WarehouseId = model.WarehouseId,
                ProductId = model.ProductId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.WarehouseId)
            {
                return BadRequest();
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/APIStocks
        [HttpPost]
        public async Task<IActionResult> PostStock([FromBody] StockModel model)
        {
            var stock = new Stock
            {
                ProductId = model.ProductId,
                WarehouseId = model.WarehouseId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stocks.Add(stock);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StockExists(stock.WarehouseId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStock", new { id = stock.WarehouseId }, stock);
        }

        // DELETE: api/APIStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return Ok(stock);
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.WarehouseId == id);
        }
    }
}
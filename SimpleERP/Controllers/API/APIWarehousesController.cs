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
    [Route("api/warehouse")]
    [ApiController]
    public class APIWarehousesController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIWarehousesController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIWarehouses
        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            return Ok(await _context.Warehouses.Select(s => new Warehouse {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync());
        }

        // GET: api/APIWarehouses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouse = await _context.Warehouses.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(new Warehouse {
                Id = warehouse.Id,
                Name = warehouse.Name
            });
        }

        // PUT: api/APIWarehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse([FromRoute] int id, [FromBody] WarehouseModel model)
        {
            var warehouse = new Warehouse
            {
                Id = model.Id,
                Name = model.Name
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // POST: api/APIWarehouses
        [HttpPost]
        public async Task<IActionResult> PostWarehouse([FromBody] WarehouseModel model)
        {
            var warehouse = new Warehouse
            {
                Name = model.Name
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/APIWarehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();

            return Ok(warehouse);
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
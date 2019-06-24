using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.API.Product;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/product")]
    [ApiController]
    public class APIProductsController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIProductsController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIProducts
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.Select(s => new Product
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }).ToListAsync());
        }

        // GET: api/APIProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            });
        }

        // PUT: api/APIProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductModel model)
        {
            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/APIProducts
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/APIProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
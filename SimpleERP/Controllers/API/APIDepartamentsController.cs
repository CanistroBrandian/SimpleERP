using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.API.Department;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities;

namespace SimpleERP.Controllers.API
{
    [Route("api/departament")]
    [ApiController]
    public class APIDepartamentsController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIDepartamentsController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIDepartaments
        [HttpGet]
        public async Task<IActionResult> GetDepartaments()
        {
            return Ok(await _context.Departaments.Select(s => new DepartmentModel
            {
                WarehouseId = s.WarehouseId,
                Name = s.Name,
                Id = s.Id
            }).ToListAsync());
        }

        // GET: api/APIDepartaments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = await _context.Departaments.FindAsync(id);

            if (departament == null)
            {
                return NotFound();
            }

            return Ok(new Departament
            {
                Name = departament.Name,
                WarehouseId = departament.WarehouseId,
                Id = departament.Id
            });
        }

        // PUT: api/APIDepartaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartament([FromRoute] int id, [FromBody] DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = new Departament
            {
                Name = model.Name,
                WarehouseId = model.WarehouseId,
                Id = model.Id
            };

            if (id != departament.Id)
            {
                return BadRequest();
            }

            _context.Entry(departament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentExists(id))
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

        // POST: api/APIDepartaments
        [HttpPost]
        public async Task<IActionResult> PostDepartament([FromBody] DepartmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departament = new Departament
            {
                Name = model.Name,
                WarehouseId = model.WarehouseId,
            };
            _context.Departaments.Add(departament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartament", new { id = departament.Id }, departament);
        }

        // DELETE: api/APIDepartaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = await _context.Departaments.FindAsync(id);
            if (departament == null)
            {
                return NotFound();
            }

            _context.Departaments.Remove(departament);
            await _context.SaveChangesAsync();

            return Ok(departament);
        }

        private bool DepartamentExists(int id)
        {
            return _context.Departaments.Any(e => e.Id == id);
        }
    }
}
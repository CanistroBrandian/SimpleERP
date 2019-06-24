using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.API.Employe;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;

namespace SimpleERP.Controllers.API
{
    [Route("api/employeorder")]
    [ApiController]
    public class APIEmployeOrdersController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIEmployeOrdersController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIEmployeOrders
        [HttpGet]
        public async Task<IActionResult> GetEmployeOrders()
        {
            return Ok(await _context.EmployeOrders.Select(s => new EmployeOrder
            {

                EmployeId = s.EmployeId,
                OrderId = s.OrderId
            }).ToListAsync());
        }

        // GET: api/APIEmployeOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeOrder([FromRoute] int id)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeOrder = await _context.EmployeOrders.FindAsync(id);

            if (employeOrder == null)
            {
                return NotFound();
            }

            return Ok(new EmployeOrder
            {

                EmployeId = employeOrder.EmployeId,
                OrderId = employeOrder.OrderId
            });
        }

        // PUT: api/APIEmployeOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeOrder([FromRoute] int id, [FromBody] EmployeOrderModel model)
        {
            var employeOrder = new EmployeOrder
            {
                EmployeId = model.EmployeId,
                OrderId = model.OrderId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(employeOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeOrderExists(id))
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

        // POST: api/APIEmployeOrders
        [HttpPost]
        public async Task<IActionResult> PostEmployeOrder([FromBody] EmployeOrderModel model)
        {
            var employeOrder = new EmployeOrder
            {
                EmployeId = model.EmployeId,
                OrderId = model.OrderId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeOrders.Add(employeOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeOrderExists(employeOrder.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeOrder", new { id = employeOrder.OrderId }, employeOrder);
        }

        // DELETE: api/APIEmployeOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeOrder = await _context.EmployeOrders.FindAsync(id);
            if (employeOrder == null)
            {
                return NotFound();
            }

            _context.EmployeOrders.Remove(employeOrder);
            await _context.SaveChangesAsync();

            return Ok(employeOrder);
        }

        private bool EmployeOrderExists(int id)
        {
            return _context.EmployeOrders.Any(e => e.OrderId == id);
        }
    }
}
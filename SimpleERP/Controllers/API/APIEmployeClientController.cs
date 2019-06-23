using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;

namespace SimpleERP.Controllers.API
{
    [Route("api/employeclient")]
    [ApiController]
    public class APIEmployeClientController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIEmployeClientController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIEmployeClient
        [HttpGet]
        public IEnumerable<EmployeClient> GetEmployeClients()
        {
            return _context.EmployeClients;
        }

        // GET: api/APIEmployeClient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeClient([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeClient = await _context.EmployeClients.FindAsync(id);

            if (employeClient == null)
            {
                return NotFound();
            }

            return Ok(employeClient);
        }

        // PUT: api/APIEmployeClient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeClient([FromRoute] string id, [FromBody] EmployeClient employeClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeClient.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(employeClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeClientExists(id))
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

        // POST: api/APIEmployeClient
        [HttpPost]
        public async Task<IActionResult> PostEmployeClient([FromBody] EmployeClient employeClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeClients.Add(employeClient);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeClientExists(employeClient.ClientId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeClient", new { id = employeClient.ClientId }, employeClient);
        }

        // DELETE: api/APIEmployeClient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeClient([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeClient = await _context.EmployeClients.FindAsync(id);
            if (employeClient == null)
            {
                return NotFound();
            }

            _context.EmployeClients.Remove(employeClient);
            await _context.SaveChangesAsync();

            return Ok(employeClient);
        }

        private bool EmployeClientExists(string id)
        {
            return _context.EmployeClients.Any(e => e.ClientId == id);
        }
    }
}
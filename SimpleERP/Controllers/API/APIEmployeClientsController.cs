using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Models.API.Employe;

using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Controllers.API
{
    [Route("api/employeclient")]
    [ApiController]
    public class APIEmployeClientsController : ControllerBase
    {
        private readonly IEmployeClientsRepository _repository;

        public APIEmployeClientsController(IEmployeClientsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIEmployeClient
        [HttpGet]
        public async Task<IActionResult> GetEmployeClients()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new EmployeClient
            {
                ClientId = s.ClientId,
                EmployeId = s.EmployeId
            }));
        }

        // GET: api/APIEmployeClient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeClient = await _repository.GetSingleAsync(id);

            if (employeClient == null)
            {
                return NotFound();
            }

            return Ok( new EmployeClientModel
            {
                ClientId = employeClient.ClientId,
                EmployeId = employeClient.EmployeId
            });
        }

        // PUT: api/APIEmployeClient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeClient([FromRoute] string id, [FromBody] EmployeClientModel model)
        { 
              var employeClient = new EmployeClient
              {
                  ClientId = model.ClientId,
                  EmployeId = model.EmployeId
              };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (id != employeClient.ClientId)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(employeClient);



            return NoContent();
        }

        // POST: api/APIEmployeClient
        [HttpPost]
        public async Task<IActionResult> PostEmployeClient([FromBody] EmployeClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeClient = new EmployeClient
            {
                ClientId = model.ClientId,
                EmployeId = model.EmployeId
            };
            
            await _repository.AddAsync(employeClient);


            return CreatedAtAction("GetEmployeClient", new { id = employeClient.EmployeId }, employeClient);
        }

        // DELETE: api/APIEmployeClient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeClient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeClient = await _repository.DeleteAsync(id);
            if (employeClient == null)
            {
                return NotFound();
            }

            var model = new EmployeClient
            {
                ClientId = employeClient.ClientId,
                EmployeId = employeClient.EmployeId
            };

            return Ok(model);
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Models.API.Employe;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;

namespace SimpleERP.Controllers.API
{
    [Route("api/employeorder")]
    [ApiController]
    public class APIEmployeOrdersController : ControllerBase
    {
        private readonly IEmployeOrdersRepository _repository;

        public APIEmployeOrdersController(IEmployeOrdersRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIEmployeOrders
        [HttpGet]
        public async Task<IActionResult> GetEmployeOrders()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new EmployeOrderModel
            {

                EmployeId = s.EmployeId,
                OrderId = s.OrderId
            }));
        }

        // GET: api/APIEmployeOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeOrder([FromRoute] int id)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeOrder = await _repository.GetSingleAsync(id);

            if (employeOrder == null)
            {
                return NotFound();
            }

            return Ok(new EmployeOrderModel
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

            await _repository.UpdateAsync(employeOrder);



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

           await _repository.AddAsync(employeOrder);
           

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

            var employeOrder = await _repository.DeleteAsync(id);
            if (employeOrder == null)
            {
                return NotFound();
            }

            var model = new EmployeOrder
            {
                EmployeId = employeOrder.EmployeId,
                OrderId = employeOrder.OrderId
            };

            return Ok(model);
        }

       
    }
}
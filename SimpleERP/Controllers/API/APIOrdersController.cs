using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.API.Order;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.OrderEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/order")]
    [ApiController]
    public class APIOrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public APIOrdersController(IOrderRepository repository)
        {
            _orderRepository = repository;
        }

        // GET: api/APIOrders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok((await _orderRepository.GetAllAsync()).Select(s => new OrderModel
            {
                Id = s.Id,
                Status = s.Status,
                Information = s.Information
            }));
        }

        // GET: api/APIOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.GetSingleAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(new OrderModel
            {
                Id = order.Id,
                Status = order.Status,
                Information = order.Information
            });
        }

        // PUT: api/APIOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] OrderModel model)
        {
            var order = new Order
            {
                Id = model.Id,
                Information = model.Information,
                Status = model.Status
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        // POST: api/APIOrders
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderModel model)
        {
            var order = new Order
            {
                Status = model.Status,
                Information = model.Information
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            order = await _orderRepository.AddAsync(order);

            model.Id = order.Id;

            return CreatedAtAction("GetOrder", new { id = order.Id }, model);
        }

        // DELETE: api/APIOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.DeleteAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var model = new OrderModel
            {
                Id = order.Id,
                Information = order.Information,
                Status = order.Status
            };

            return Ok(model);
        }
    }
}
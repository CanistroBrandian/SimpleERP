﻿using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Models.API.Order;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/orderproduct")]
    [APIAuthorize]
    [ApiController]
    public class APIOrderProductsController : ControllerBase
    {
        private readonly IOrderProductsRepository _repository;

        public APIOrderProductsController(IOrderProductsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIOrderProducts
        [HttpGet]
        public async Task<IActionResult> GetOrderProducts()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new OrderProduct
            {
                OrderId = s.OrderId,
                ProductId = s.ProductId
            }));
        }

        // GET: api/APIOrderProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderProduct = await _repository.GetSingleAsync(id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(new OrderProductModel
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

            await _repository.UpdateAsync(orderProduct);



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

           await _repository.AddAsync(orderProduct);
           

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

            var orderProduct = await _repository.DeleteAsync(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            var model = new OrderProduct
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId
            };

            return Ok(model);
        }

       
    }
}
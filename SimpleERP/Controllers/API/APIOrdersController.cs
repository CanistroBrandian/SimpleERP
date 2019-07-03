using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Models.API.Order;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route(BASE_ROUTE)]
    [APIAuthorize]
    [ApiController]
    public class APIOrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public const string BASE_ROUTE = "api/order";
        public APIOrdersController(IOrderRepository repository)
        {
            _orderRepository = repository;
        }

        // GET: api/APIOrders
        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <returns> Returns models of orders</returns>
        /// <response code="200">Returns models of orders</response>
        [ProducesResponseType(200)]
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
        /// <summary>
        /// Get orders by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns model of order</returns>
        /// <response code="200">Returns model of order</response>
        /// <response code="400">Invalid Data</response>
        /// <response code="404">Not found order</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /4
        ///     {
        ///        "id":1,
        ///        "information":"information",
        ///        "status" : false
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Returns new model of order</returns>
        /// <response code="200">Returns new model of order</response>
        /// <response code="404">Not found order or invalid data</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
            return Ok(order);
        }

        // POST: api/APIOrders
        /// <summary>
        /// Create new department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "id": 2,
        ///        "information":"information",
        ///        "status" : false   
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Return new model of order</returns>
        /// <response code="201">Return new model of order</response>
        /// <response code="400">Invalid data</response>            
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// <summary>
        /// Delete order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns new model of order</returns>
        /// <response code="200">Returns new model of orde</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found order</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Add new order from products
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /products
        ///     {
        ///        "productId": 1,
        ///        "orderOd": 4  
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Returns model orderProducts</returns>
        /// <response code="201">Returns model OrderProducts</response>
        /// <response code="400">Invalid data</response>   
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("products")]
        public async Task<IActionResult> AddOrderWithProducts([FromBody] OrderProductModel model)
        {

            var orderProducts = new OrderProduct
            {
                ProductId = model.ProductId,
                OrderId = model.OrderId
            };
            await _orderRepository.AddOrderWithProducts(orderProducts);
            return CreatedAtAction("GetStock", orderProducts);
        }
        /// <summary>
        /// Get all order from products
        /// </summary>
        /// <returns>Returns order from product</returns>
        /// <response code="200">Returns order from product</response>
        [ProducesResponseType(200)]
        [HttpGet("products")]
        public async Task<IActionResult> GetAllOrderProducts()
        {
            return Ok((await _orderRepository.GetAllOrderProducts()).Select(s => new OrderProductModel
            {
                ProductId = s.ProductId,
                OrderId = s.OrderId
            }));
        }
    }
}
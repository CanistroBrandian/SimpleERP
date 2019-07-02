using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Models.API.Employe;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/employe")]
    [APIAuthorize]
    [ApiController]
    public class APIEmployeController : ControllerBase
    {
        private readonly IEmployeRepository _repository;

        public APIEmployeController(IEmployeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Add clients to employee
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /clients
        ///     {
        ///        "employeId": 1,
        ///        "clientId": 4  
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Returns model EmployeClient</returns>
        /// <response code="201">Returns model EmployeClient</response>
        /// <response code="400">Invalid data</response>   
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("clients")]
        public async Task<IActionResult> AddClientToEmploye([FromBody] EmployeClientModel model)
        {

            var employeClient = new EmployeClient
            {
                EmployeId = model.EmployeId,
                ClientId = model.ClientId
            };
            if (model.ClientId == null)
            {
                BadRequest("Client is not found");
            }
            if (model.EmployeId == null)
            {
                BadRequest("Employe is not found");
            }
            await _repository.AddClientToEmploye(employeClient);
            return CreatedAtAction("GetEmployeClients", employeClient);
        }

        /// <summary>
        /// Get all clients by employee
        /// </summary>
        /// <returns>Returns clients by employee</returns>
        /// <response code="200">Returns clients by employe</response>   
        [ProducesResponseType(200)]
        [HttpGet("clients")]
        public async Task<IActionResult> GetAllEmployeClients()
        {

            return Ok((await _repository.GetAllEmployeClients()).Select(s => new EmployeClientModel
            {
                EmployeId = s.EmployeId,
                ClientId = s.ClientId
            }));
        }
        /// <summary>
        /// Add order of client to employe
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /orders
        ///     {
        ///        "employeId": 1,
        ///        "orderId": 4  
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>returns model EmployeOrder</returns>
        /// <response code="201">Returns model EmployeOrder</response>
        /// <response code="400">Invalid data</response>   
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("orders")]
        public async Task<IActionResult> AddOrdersToEmploye([FromBody] EmployeOrderModel model)
        {

            var employeOrder = new EmployeOrder
            {
                EmployeId = model.EmployeId,
                OrderId = model.OrderId
            };

            if (model.OrderId == 0)
            {
                BadRequest("Order is not found");
            }
            if (model.EmployeId == null)
            {
                BadRequest("Employe is not found");
            }
            await _repository.AddOrdersToEmploye(employeOrder);
            return CreatedAtAction("GetEmployeOrders", employeOrder);
        }
        /// <summary>
        /// Get all orders of client by employee
        /// </summary>
        /// <returns>Returns clients by employee</returns>
        /// <response code="200">Get all orders of client by employee</response>   
        [ProducesResponseType(200)]
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrdersOfEmploye()
        {

            return Ok((await _repository.GetAllOrdersOfEmploye()).Select(s => new EmployeOrderModel
            {
                EmployeId = s.EmployeId,
                OrderId = s.OrderId
            }));
        }

    }
}

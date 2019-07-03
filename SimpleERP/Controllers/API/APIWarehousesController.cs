using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Models.API.Warehouse;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route(BASE_ROUTE)]
    [APIAuthorize]
    [ApiController]
    public class APIWarehousesController : ControllerBase
    {

        public const string BASE_ROUTE = "api/warehouse";

        private readonly IWarehouseRepository _repository;

        public APIWarehousesController(IWarehouseRepository repo)
        {
            _repository = repo;
        }

        // GET: api/APIWarehouses
        /// <summary>
        /// Get all warehouses
        /// </summary>
        /// <returns> Returns models of warehouses</returns>
        /// <response code="200">Returns models of warehouses</response>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new WarehouseModel
            {
                Id = s.Id,
                Name = s.Name
            }));
        }

        // GET: api/APIWarehouses/5
        /// <summary>
        /// Get warehouse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns model of warehouse</returns>
        /// <response code="200">Returns model of warehouse</response>
        /// <response code="400">Invalid Data</response>
        /// <response code="404">Not found warehouse</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouse = await _repository.GetSingleAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(new WarehouseModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name
            });
        }

        // PUT: api/APIWarehouses/5
        /// <summary>
        /// Update warehouse by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /4
        ///     {
        ///        "id":1,
        ///        "name":"name1"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Returns new model of warehouse</returns>
        /// <response code="200">Returns new model of warehouse</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found warehouse</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse([FromRoute] int id, [FromBody] WarehouseModel model)
        {
            var warehouse = new Warehouse
            {
                Id = model.Id,
                Name = model.Name
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(warehouse);
            return Ok(warehouse);
        }

        // POST: api/APIWarehouses
        /// <summary>
        /// Create new warehouse
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "id":1,
        ///        "name":"name1"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Return new model of warehouse</returns>
        /// <response code="201">Return new model of warehouse</response>
        /// <response code="400">Invalid data</response>            
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> PostWarehouse([FromBody] WarehouseModel model)
        {
            var warehouse = new Warehouse
            {
                Id = model.Id,
                Name = model.Name,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            warehouse = await _repository.AddAsync(warehouse);

            model.Id = warehouse.Id;

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, model);
        }

        // DELETE: api/APIWarehouses/5
        /// <summary>
        /// Delete warehouse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns new model of warehouse</returns>
        /// <response code="200">Returns new model of warehouse</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found warehouse or invalid data</response>        
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouse = await _repository.DeleteAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            var model = new WarehouseModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
            };

            return Ok(model);
        }

        /// <summary>
        /// Add product in warehouse
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /stocks
        ///     {
        ///        "productId": 1,
        ///        "warehouseId": 4  
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>returns model Stocks</returns>
        /// <response code="201">Returns model Stocks</response>
        /// <response code="400">Invalid data</response>   
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("stocks")]
        public async Task<IActionResult> AddProductToWarehouse([FromBody] StockModel model)
        {

            var stock = new Stock
            {
                ProductId = model.ProductId,
                WarehouseId = model.WarehouseId
            };
            await _repository.AddProductToWarehouse(stock);
            return CreatedAtAction("GetStock", model);
        }
        /// <summary>
        /// Get product with warehouse
        /// </summary>
        /// <returns>Returns model stocks</returns>
        /// <response code="200">Returns model stocks</response>   
        [ProducesResponseType(200)]
        [HttpGet("stocks")]
        public async Task<IActionResult> GEtAllWarehouseStocks()
        {
            return Ok((await _repository.GetAllStocksAsync()).Select(s => new StockModel
            {
                ProductId = s.ProductId,
                WarehouseId = s.WarehouseId
            }));
        }


    }
}
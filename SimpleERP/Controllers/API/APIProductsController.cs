using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Models.API.Product;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route(BASE_ROUTE)]
    [ApiController]
    public class APIProductsController : ControllerBase
    {
        public const string BASE_ROUTE = "api/product";
        private readonly IProductRepository _repository;

        public APIProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIProducts
        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns> Returns models of products</returns>
        /// <response code="200">Returns models of products</response>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new ProductModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            }));
        }

        // GET: api/APIProducts/5
        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns model of product</returns>
        /// <response code="200">Returns model of product</response>
        /// <response code="400">Invalid Data</response>
        /// <response code="404">Not found product</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _repository.GetSingleAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description
            });
        }

        // PUT: api/APIProducts/5
        /// <summary>
        /// Update product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /2
        ///     {
        ///        "id":1,
        ///        "name":"name1",
        ///        "description" : "description"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Returns new model of product</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Not found product or invalid data</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ProductModel model)
        {
            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }
            await _repository.UpdateAsync(product);
            return Ok(product);
        }

        // POST: api/APIProducts
        /// <summary>
        /// Create new product
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "id":1,
        ///        "name":"name1",
        ///        "description" : "description"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Return new model of product</returns>
        /// <response code="201">Return new model of product</response>
        /// <response code="400">Invalid data</response>            
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product = await _repository.AddAsync(product);

            model.Id = product.Id;

            return CreatedAtAction("GetProduct", new { id = product.Id }, model);
        }

        // DELETE: api/APIProducts/5

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns new model of warehouse</returns>
        /// <response code="200">Returns new model of warehouse</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found product</response>        
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _repository.DeleteAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductModel
            {
                Name = product.Name,
                Description = product.Description
            };

            return Ok(model);
        }


    }
}
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



            return NoContent();
        }

        // POST: api/APIProducts
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
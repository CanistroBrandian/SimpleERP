using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities;
using SimpleERP.Models.API.Departament;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route(BASE_ROUTE)]
    [APIAuthorize]
    [ApiController]
    public class APIDepartamentsController : ControllerBase
    {
        private readonly IDepartamentRepository _repository;
        public const string BASE_ROUTE = "api/departament";
        public APIDepartamentsController(IDepartamentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIDepartaments
        [HttpGet]
        public async Task<IActionResult> GetDepartaments()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new DepartamentModel
            {
                WarehouseId = s.WarehouseId,
                Name = s.Name,
                Id = s.Id
            }));
        }

        // GET: api/APIDepartaments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = await _repository.GetSingleAsync(id);

            if (departament == null)
            {
                return NotFound();
            }

            return Ok(new DepartamentModel
            {
                Name = departament.Name,
                WarehouseId = departament.WarehouseId,
                Id = departament.Id
            });
        }

        // PUT: api/APIDepartaments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartament([FromRoute] int id, [FromBody] DepartamentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = new Departament
            {
                Name = model.Name,
                WarehouseId = model.WarehouseId,
                Id = model.Id
            };

            if (id != departament.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(departament);
            return NoContent();
        }

        // POST: api/APIDepartaments
        [HttpPost]
        public async Task<IActionResult> PostDepartament([FromBody] DepartamentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departament = new Departament
            {
                Name = model.Name,
                WarehouseId = model.WarehouseId,
            };
            departament = await _repository.AddAsync(departament);

            model.Id = departament.Id;

            return CreatedAtAction("GetDepartament", new { id = departament.Id }, model);
        }

        // DELETE: api/APIDepartaments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departament = await _repository.DeleteAsync(id);
            if (departament == null)
            {
                return NotFound();
            }

            var model = new Departament
            {
                Name = departament.Name,
                WarehouseId = departament.WarehouseId,
            };

            return Ok(model);
        }


    }
}
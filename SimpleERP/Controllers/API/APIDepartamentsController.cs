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
        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns> Returns models of departments</returns>
        /// <response code="200">Returns models of departments</response>
        [ProducesResponseType(200)]
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
        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns model of department</returns>
        /// <response code="200">Returns model of department</response>
        /// <response code="400">Invalid Data</response>
        /// <response code="404">Not found department</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update department by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /4
        ///     {
        ///        "Name": "Sales",
        ///        "WarehouseId": 1,
        ///        "Id" : 445
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Returns new model of department</returns>
        /// <response code="200">Returns new model of department</response>
        /// <response code="404">Not found department or invalid data</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
            return Ok(departament);
        }

        // POST: api/APIDepartaments
        /// <summary>
        /// Create new department
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "Name": "Sales",
        ///        "WarehouseId": 1,
        ///        "Id" : 445     
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Return new model of dapartment</returns>
        /// <response code="201">Return new model of dapartment</response>
        /// <response code="400">Invalid data</response>            
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Delete department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns new model of department</returns>
        /// <response code="200">Returns new model of department</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found department</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
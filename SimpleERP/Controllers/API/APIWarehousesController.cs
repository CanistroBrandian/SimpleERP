﻿using Microsoft.AspNetCore.Mvc;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.API.Warehouse;
using SimpleERP.Models.Entities.WarehouseEntity;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/warehouse")]
    [ApiController]
    public class APIWarehousesController : ControllerBase
    {

        private readonly IWarehouseRepository _repository;

        public APIWarehousesController(IWarehouseRepository repo)
        {
            _repository = repo;
        }

        // GET: api/APIWarehouses
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
            return NoContent();
    }

    // POST: api/APIWarehouses
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

        return CreatedAtAction("GetOrder", new { id = warehouse.Id }, model);
    }

    // DELETE: api/APIWarehouses/5
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
}
}
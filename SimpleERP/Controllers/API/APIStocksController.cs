using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.API.Warehouse;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/stock")]
    [ApiController]
    public class APIStocksController : ControllerBase
    {
        private readonly IStockRepository _repository;

        public APIStocksController(IStockRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIStocks
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new StockModel
            {
                WarehouseId = s.WarehouseId,
                ProductId = s.ProductId
            }));
        }

        // GET: api/APIStocks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _repository.GetSingleAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(new StockModel {
                WarehouseId = stock.WarehouseId,
                ProductId = stock.ProductId
            });
        }

        // PUT: api/APIStocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock([FromRoute] int id, [FromBody] StockModel model)
        {
            var stock = new Stock
            {
                WarehouseId = model.WarehouseId,
                ProductId = model.ProductId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.WarehouseId)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(stock);


            return NoContent();
        }

        // POST: api/APIStocks
        [HttpPost]
        public async Task<IActionResult> PostStock([FromBody] StockModel model)
        {
            var stock = new Stock
            {
                ProductId = model.ProductId,
                WarehouseId = model.WarehouseId
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await _repository.AddAsync(stock);

            
            return CreatedAtAction("GetStock", new { id = stock.ProductId }, stock);
        }

        // DELETE: api/APIStocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _repository.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            var model = new StockModel
            {
                ProductId = stock.ProductId,
                WarehouseId = stock.WarehouseId
            };

            return Ok(model);
        }

    }
}
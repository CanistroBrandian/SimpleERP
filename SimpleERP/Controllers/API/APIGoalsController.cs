using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.GoalEntity;

namespace SimpleERP.Controllers.API
{
    [Route("api/goal")]
    [ApiController]
    public class APIGoalsController : ControllerBase
    {
        private readonly ContextEF _context;

        public APIGoalsController(ContextEF context)
        {
            _context = context;
        }

        // GET: api/APIGoals
        [HttpGet]
        public IEnumerable<Goal> GetGoals()
        {
            return _context.Goals;
        }

        // GET: api/APIGoals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // PUT: api/APIGoals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal([FromRoute] int id, [FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.Id)
            {
                return BadRequest();
            }

            _context.Entry(goal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/APIGoals
        [HttpPost]
        public async Task<IActionResult> PostGoal([FromBody] Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoal", new { id = goal.Id }, goal);
        }

        // DELETE: api/APIGoals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return Ok(goal);
        }

        private bool GoalExists(int id)
        {
            return _context.Goals.Any(e => e.Id == id);
        }
    }
}
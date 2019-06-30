using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Models.API.Goal;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/goal")]
    [APIAuthorize(Roles ="Manager")]
    [ApiController]
    public class APIGoalsController : ControllerBase
    {
        private readonly IGoalRepository _repository;

        public APIGoalsController(IGoalRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIGoals
        [HttpGet]
        public async Task<ActionResult> GetGoals()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new Goal
            {
                Name = s.Name,
                Description = s.Description,
                AssigneId = s.AssigneId,
                ReporterId = s.ReporterId,
                DateCreated = s.DateCreated,
                DateFinished = s.DateFinished
            }));
        }

        // GET: api/APIGoals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _repository.GetSingleAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            return Ok(new GoalModel
            {
                Name = goal.Name,
                Description = goal.Description,
                AssigneId = goal.AssigneId,
                ReporterId = goal.ReporterId,
                DateCreated = goal.DateCreated,
                DateFinished = goal.DateFinished
            });
        }

        // PUT: api/APIGoals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal([FromRoute] int id, [FromBody] GoalModel model)
        {
            var goal = new Goal
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                AssigneId = model.AssigneId,
                ReporterId = model.ReporterId,
                DateCreated = model.DateCreated,
                DateFinished = model.DateFinished
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(goal);



            return NoContent();
        }

        // POST: api/APIGoals
        [HttpPost]
        public async Task<IActionResult> PostGoal([FromBody] GoalModel model)
        {
            var goal = new Goal
            {
                Name = model.Name,
                Description = model.Description,
                AssigneId = model.AssigneId,
                ReporterId = model.ReporterId,
                DateCreated = model.DateCreated,
                DateFinished = model.DateFinished
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            goal = await _repository.AddAsync(goal);

            model.Id = goal.Id;

            return CreatedAtAction("GetGoal", new { id = goal.Id }, model);
        }

        // DELETE: api/APIGoals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goal = await _repository.DeleteAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            var model = new Goal
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                AssigneId = goal.AssigneId,
                ReporterId = goal.ReporterId,
                DateCreated = goal.DateCreated,
                DateFinished = goal.DateFinished
            };

            return Ok(model);
        }


    }
}
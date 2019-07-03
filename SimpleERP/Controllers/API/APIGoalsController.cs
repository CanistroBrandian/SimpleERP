using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Models.API.Goal;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route(BASE_ROUTE)]
    [APIAuthorize(Roles = "Manager")]
    [ApiController]
    public class APIGoalsController : ControllerBase
    {
        private readonly IGoalRepository _repository;
        public const string BASE_ROUTE = "api/goal";
        public APIGoalsController(IGoalRepository repository)
        {
            _repository = repository;
        }

        // GET: api/APIGoals
        /// <summary>
        /// Get all goals of employee or manager if you roles is manager
        /// </summary>
        /// <returns> Returns models of goal</returns>
        /// <response code="200">Returns models of goal</response>
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult> GetGoals()
        {
            return Ok((await _repository.GetAllAsync()).Select(s => new GoalModel
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

        /// <summary>
        /// Get goal by id if you roles is manager
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns model of goal</returns>
        /// <response code="200">Returns model of goal</response>
        /// <response code="400">Invalid Data</response>
        /// <response code="404">Not found goal</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <summary>
        /// Update goal by id if you roles is manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /4
        ///     {
        ///        "id": 1,
        ///        "name": "goal1",
        ///        "description" : "description",
        ///        "AssigneId":"AssigneId",
        ///        "ReporterId": "ReporterId",
        ///        "DateCreated":"2019-05-21T23:10:15",
        ///        "DateFinished":"2020-05-21T23:10:15",
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Returns new model of goal</returns>
        /// <response code="200">Returns new model of goal</response>
        /// <response code="404">Not found goal or invalid data</response>

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

            return Ok(goal);
        }

        // POST: api/APIGoals
        /// <summary>
        /// Create new goal for employe if you roles is manager
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "id": 2,
        ///        "name": "goal2",
        ///        "description" : "description",
        ///        "AssigneId":"AssigneId",
        ///        "ReporterId": "ReporterId",
        ///        "DateCreated":"2019-05-21T23:10:15",
        ///        "DateFinished":"2020-05-21T23:10:15",
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Returns new model of goal</returns>
        /// <response code="201">Returns new model of goal</response>
        /// <response code="400">Invalid data</response>

        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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
        /// <summary>
        /// Delete goal for employe if you roles is manager
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the deleted model of goal</returns>
        /// <response code="200">Returns the deleted model of goal</response>
        /// <response code="400">Invalid data</response>

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
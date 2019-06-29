using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Models.API.Employe;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/employe")]
    [APIAuthorize]
    [ApiController]
    public class APIEmployeController : ControllerBase
    {
        private readonly IEmployeRepository _repository;

        public APIEmployeController(IEmployeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("employeclients")]
        public async Task<IActionResult> AddClientToEmploye([FromBody] EmployeClientModel model)
        {

            var employeClient = new EmployeClient
            {
                EmployeId = model.EmployeId,
                ClientId = model.ClientId
            };
            await _repository.AddClientToEmploye(employeClient);
            return CreatedAtAction("GetEmployeClients", employeClient);
        }

        [HttpGet("employeclients")]
        public async Task<IActionResult> GetAllEmployeClients()
        {

            return Ok((await _repository.GetAllEmployeClients()).Select(s => new EmployeClientModel
            {
                EmployeId = s.EmployeId,
                ClientId = s.ClientId
            }));
        }
    }
}
//}
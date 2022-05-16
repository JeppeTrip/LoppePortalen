using Application.StallTypes.Commands.CreateStallType;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class StallTypeController : ApiBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<CreateStallTypeResponse>> CreateStallType(CreateStallTypeRequest dto)
        {
            return await Mediator.Send(new CreateStallTypeCommand() { Dto = dto });
        }
    }
}

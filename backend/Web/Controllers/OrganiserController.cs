using Application.Organisers.Commands.CreateOrganiser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class OrganiserController : ApiBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateOrganiserResponse>> GetTest(CreateOrganiserRequest dto)
        {
            return await Mediator.Send(new CreateOrganiserCommand() { Dto = dto });
        }
    }
}

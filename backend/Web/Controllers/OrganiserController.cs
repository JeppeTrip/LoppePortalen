using Application.ContactInformation.Commands.AddContactsToOrganiser;
using Application.Organisers.Commands.CreateOrganiser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class OrganiserController : ApiBase
    {
        [HttpPost("/new")]
        public async Task<ActionResult<CreateOrganiserResponse>> CreateOrganiser(CreateOrganiserRequest dto)
        {
            return await Mediator.Send(new CreateOrganiserCommand() { Dto = dto });
        }

        [HttpPost("/Add/ContactInformation")]
        public async Task<ActionResult<AddContactsToOrganiserResponse>> AddContactInformation(AddContactsToOrganiserRequest dto)
        {
            return await Mediator.Send(new AddContactsToOrganiserCommand() { Dto = dto });
        }
    }
}

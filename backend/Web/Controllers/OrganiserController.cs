using Application.ContactInformation.Commands.AddContactsToOrganiser;
using Application.Organisers.Commands.CreateOrganiser;
using Application.Organisers.Queries.GetAllOrganisers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class OrganiserController : ApiBase
    {
        [HttpPost("new")]
        public async Task<ActionResult<CreateOrganiserResponse>> CreateOrganiser(CreateOrganiserRequest dto)
        {
            return await Mediator.Send(new CreateOrganiserCommand() { Dto = dto });
        }

        [HttpPost("add/contactInformation")]
        public async Task<ActionResult<AddContactsToOrganiserResponse>> AddContactInformation(AddContactsToOrganiserRequest dto)
        {
            return await Mediator.Send(new AddContactsToOrganiserCommand() { Dto = dto });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetAllOrganisersResponse>>> GetAllOrganisers()
        {
            return await Mediator.Send(new GetAllOrganisersQuery());
        }
    }
}

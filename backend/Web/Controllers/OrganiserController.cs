using Application.ContactInformation.Commands.AddContactsToOrganiser;
using Application.Organisers.Commands.CreateOrganiser;
using Application.Organisers.Commands.EditOrganiser;
using Application.Organisers.Queries.GetAllOrganisers;
using Application.Organisers.Queries.GetAllOrganisersWithPagination;
using Application.Organisers.Queries.GetOrganiser;
using Application.Organisers.Queries.GetUsersOrganisers;
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
        public async Task<ActionResult<GetAllOrganisersResponse>> GetAllOrganisers()
        {
            return await Mediator.Send(new GetAllOrganisersQuery());
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<GetOrganisersWithPaginationResponse>> GetOrganisers([FromRoute] int pageNumber, [FromRoute] int pageSize)
        {
            var dto = new GetOrganisersWithPaginationRequest() { PageNumber = pageNumber, PageSize = pageSize };
            return await Mediator.Send(new GetOrganisersWithPaginationQuery() { Dto = dto});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrganiserQueryResponse>> GetOrganiser([FromRoute] string id)
        {
            int organiserId = int.Parse(id);
            return await Mediator.Send(
                new GetOrganiserQuery()
                {
                    Dto = new GetOrganiserQueryRequest() { Id = organiserId }
                });
        }

        [HttpPut("edit")]
        public async Task<ActionResult<EditOrganiserResponse>> EditOrganiser(EditOrganiserRequest dto)
        {
            return await Mediator.Send(new EditOrganiserCommand() { Dto = dto });
        }
    }
}

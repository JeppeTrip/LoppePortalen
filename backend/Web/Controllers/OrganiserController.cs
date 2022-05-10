using Application.Common.Exceptions;
using Application.Organisers.Commands.AddContactInformation;
using Application.Organisers.Commands.CreateOrganiser;
using Application.Organisers.Commands.EditOrganiser;
using Application.Organisers.Commands.RemoveContactInformation;
using Application.Organisers.Commands.UploadBanner;
using Application.Organisers.Queries.GetAllOrganisers;
using Application.Organisers.Queries.GetAllOrganisersWithPagination;
using Application.Organisers.Queries.GetOrganiser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
        public async Task<ActionResult<AddContactInformationResponse>> AddContactInformation(AddOrganiserContactInformationRequest dto)
        {
            return await Mediator.Send(new AddContactInformationCommand() { Dto = dto });
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
            int organiserId;
            try
            {
                organiserId = int.Parse(id);
            } catch (Exception ex)
            {
                throw new ValidationException($"No organiser with id {id}.");
            }
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

        [HttpDelete("contactInfo")]
        public async Task<ActionResult<RemoveContactInformationResponse>> RemoveContactInformation(RemoveContactInformationRequest dto)
        {
            return await Mediator.Send(new RemoveContactInformationCommand() { Dto = dto });
        }

        [HttpPut("banner/upload")]
        public async Task<ActionResult<UploadBannerResponse>> UploadOrganiserBanner(int organiserId, IFormFile image)
        {
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);

                    var request = new UploadBannerRequest() { OrganiserId = organiserId, Title = "banner", ImageData = fileBytes };
                    var command = new UploadBannerCommand() { Dto = request };
                    return await Mediator.Send(command);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

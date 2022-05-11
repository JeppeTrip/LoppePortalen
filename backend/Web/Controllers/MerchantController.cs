using Application.Common.Models;
using Application.Merchants.Commands.AddContactInformation;
using Application.Merchants.Commands.CreateMerchant;
using Application.Merchants.Commands.EditMerchant;
using Application.Merchants.Commands.RemoveContactInformation;
using Application.Merchants.Commands.UploadMerchantBanner;
using Application.Merchants.Queries.AllMerchants;
using Application.Merchants.Queries.GetMerchant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MerchantController : ApiBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateMerchantResponse>> CreateMerchant(CreateMerchantRequest dto)
        {
            return await Mediator.Send(new CreateMerchantCommand() { Dto = dto });
        }

        [HttpGet("all")]
        public async Task<ActionResult<AllMerchantsQueryResponse>> GetAllMerchants()
        {
            return await Mediator.Send(new AllMerchantsQuery());
        }

        [HttpGet]
        public async Task<ActionResult<GetMerchantQueryResponse>> GetMerchant([FromQuery] int id)
        {
            return await Mediator.Send(new GetMerchantQuery() { Dto = new GetMerchantQueryRequest() { Id = id } });
        }

        [HttpPut]
        public async Task<ActionResult<EditMerchantResponse>> UpdateMerchant(EditMerchantRequest dto)
        {
            return await Mediator.Send(new EditMerchantCommand() { Dto = dto });
        }

        [HttpGet("booths")]
        public async Task<ActionResult<List<BoothBaseVM>>> GetBooths([FromQuery] int merchantId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add/contactInformation")]
        public async Task<ActionResult<AddContactInformationResponse>> AddContactInformation(AddMerchantContactInformationRequest dto)
        {
            return await Mediator.Send(new AddContactInformationCommand() { Dto = dto });
        }

        [HttpDelete("contactInfo")]
        public async Task<ActionResult<RemoveMerchantContactResponse>> RemoveContactInformation(RemoveMerchantContactRequest dto)
        {
            return await Mediator.Send(new RemoveMerchantContactCommand() { Dto = dto });
        }

        [HttpPut("banner/upload")]
        public async Task<ActionResult<UploadMerchantBannerResponse>> UploadMerchantBanner(int merchantId, IFormFile image)
        {
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);

                    var request = new UploadMerchantBannerRequest() { MerchantId = merchantId, Title = "banner", ImageData = fileBytes };
                    var command = new UploadMerchantBannerCommand() { Dto = request };
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

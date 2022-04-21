using Application.Common.Models;
using Application.Merchants.Commands.CreateMerchant;
using Application.Merchants.Commands.EditMerchant;
using Application.Merchants.Queries.AllMerchants;
using Application.Merchants.Queries.GetBooths;
using Application.Merchants.Queries.GetMerchant;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("/all")]
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
        public async Task<ActionResult<List<Booth>>> GetBooths([FromQuery] int merchantId)
        {
            var boothsResult = await Mediator.Send(new GetBoothsQuery() { Dto = new GetBoothsRequest() { MerchantId = merchantId } });
            var merchantResult = await Mediator.Send(new GetMerchantQuery() { Dto =  new GetMerchantQueryRequest() { Id = merchantId } } );
            var result = boothsResult.Booths.Select(x => new Booth() { BoothName = x.BoothName, BoothDescription = x.BoothDescription, Merchant = merchantResult.Merchant, Stall = null }).ToList();
            return result;
        }
    }
}

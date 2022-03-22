using Application.Markets.Commands.CreateMarket;
using Application.Markets.Commands.EditMarketInstance;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetMarket;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MarketController : ApiBase
    {
        [HttpPost("new")]
        public async Task<ActionResult<CreateMarketResponse>> CreateMarket(CreateMarketRequest dto)
        {
            return await Mediator.Send(new CreateMarketCommand() { Dto = dto });
        }

        [HttpGet("instance/{id}")]
        public async Task<ActionResult<GetMarketInstanceQueryResponse>> GetMarketInstance([FromRoute] string id)
        { 
            int marketId = int.Parse(id);
            return await Mediator.Send(
                new GetMarketInstanceQuery()
                {
                    Dto = new GetMarketInstanceQueryRequest() { MarketId = marketId }
                });
        }

        [HttpGet("instance/all")]
        public async Task<ActionResult<List<GetAllMarketInstancesQueryResponse>>> GetAllMarketInstances()
        {
            return await Mediator.Send(new GetAllMarketInstancesQuery());
        }

        [HttpPost("instance/edit")]
        public async Task<ActionResult<EditMarketInstanceResponse>> EditMarketInstance(EditMarketInstanceRequest dto)
        {
            return await Mediator.Send(new EditMarketInstanceCommand() { Dto = dto});
        }
    }
}

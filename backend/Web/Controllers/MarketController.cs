using Application.Markets.Commands.CancelMarket;
using Application.Markets.Commands.CreateMarket;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetFilteredMarkets;
using Application.Markets.Queries.GetMarket;
using Application.Markets.Queries.GetUsersMarkets;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<GetAllMarketInstancesQueryResponse>> GetAllMarketInstances()
        {
            return await Mediator.Send(new GetAllMarketInstancesQuery());
        }

        [HttpPatch("instance/cancel/{id}")]
        public async Task<ActionResult<CancelMarketInstanceResponse>> CancelMarketInstance([FromRoute] string id)
        {
            try {
                int marketId = int.Parse(id);
                return await Mediator.Send(
                    new CancelMarketInstanceCommand()
                    {
                        Dto = new CancelMarketInstanceRequest() { MarketId = marketId }
                    });
            } catch(Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("instance/filtered")]
        public async Task<ActionResult<List<GetFilteredMarketsQueryResponse>>> GetFilteredMarketInstances(
            bool? isCancelled,
            int? organiserId,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate
        )
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                OrganiserId = organiserId,
                HideCancelled = isCancelled,
                StartDate = startDate,
                EndDate = endDate
            };
            return await Mediator.Send(
                new GetFilteredMarketsQuery()
                {
                    Dto = request
                });
        }

        [HttpGet("user/current")]
        public async Task<ActionResult<GetUsersMarketsResponse>> GetCurrentUsersMarkets()
        {
            var userid = CurrentUserService.UserId;
            return await Mediator.Send(new GetUsersMarketsQuery() { Dto = new GetUsersMarketsRequest() { UserId = userid } });
        }
    }
}

using Application.Common.Models;
using Application.Markets.Commands.CancelMarket;
using Application.Markets.Commands.CreateMarket;
using Application.Markets.Commands.EditMarket;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetFilteredMarkets;
using Application.Markets.Queries.GetMarket;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Stalls.Commands.AddStallsToMarket;
using Application.StallTypes.CreateStallType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Bodies;

namespace Web.Controllers
{
    public class MarketController : ApiBase
    {
        [HttpPost("new")]
        public async Task<ActionResult<CreateMarketResponse>> CreateMarket(NewMarketInfo body)
        {
            try
            {
                Context.Database.BeginTransaction();
                CreateMarketRequest marketRequest = new CreateMarketRequest()
                {
                    MarketName = body.MarketName,
                    Description = body.Description,
                    StartDate = body.StartDate,
                    EndDate = body.EndDate,
                    OrganiserId = body.OrganiserId
                };
                CreateMarketResponse marketResponse = await Mediator.Send(new CreateMarketCommand() { Dto = marketRequest });
                if (!marketResponse.Succeeded)
                {
                    Context.Database.RollbackTransaction();
                    return BadRequest();
                }

                var types = body.StallTypes.Select(x => (x.name.Trim(), x.description.Trim())).ToList();
                CreateStallTypesRequest stallTypesRequest = new CreateStallTypesRequest()
                {
                    MarketId = marketResponse.Market.MarketId,
                    Types = types
                };
                CreateStallTypesResponse stallTypesResponse = await Mediator.Send(new CreateStallTypesCommand() { Dto = stallTypesRequest });
                if (!stallTypesResponse.Succeeded)
                {
                    Context.Database.RollbackTransaction();
                    return BadRequest();
                }
                marketResponse.Market.StallTypes = stallTypesResponse.StallTypes;

                marketResponse.Market.Stalls = new List<Stall>();
                AddStallsToMarketResponse addStallsResponse;
                foreach (var type in stallTypesResponse.StallTypes)
                {
                    addStallsResponse = await Mediator.Send(new AddStallsToMarketCommand()
                    {
                        Dto = new AddStallsToMarketRequest()
                        {
                            MarketId = marketResponse.Market.MarketId,
                            StallTypeId = type.Id,
                            Number = body.StallTypes.First(x => x.name.Equals(type.Name)).count
                        }
                    });
                    if (!addStallsResponse.Succeeded)
                    {
                        Context.Database.RollbackTransaction();
                        return BadRequest();
                    }
                    marketResponse.Market.Stalls.AddRange(addStallsResponse.Stalls);
                }
                Context.Database.CommitTransaction();
                return Ok(marketResponse);
            }
            catch (Exception ex)
            {
                Context.Database.RollbackTransaction();
                throw;
            }
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
        public async Task<ActionResult<GetFilteredMarketsQueryResponse>> GetFilteredMarketInstances(
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

        [HttpPut]
        public async Task<ActionResult<EditMarketResponse>> UpdateMarket(EditMarketRequest dto)
        {
            return await Mediator.Send(new EditMarketCommand() { Dto = dto });
        }
    }
}

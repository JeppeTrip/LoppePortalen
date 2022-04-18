using Application.Common.Models;
using Application.Markets.Commands.BookStalls;
using Application.Markets.Commands.CancelMarket;
using Application.Markets.Commands.CreateMarket;
using Application.Markets.Commands.EditMarket;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetFilteredMarkets;
using Application.Markets.Queries.GetMarket;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Stalls.Commands.AddStallsToMarket;
using Application.Stalls.Commands.RemoveStallsFromMarket;
using Application.Stalls.Queries.GetMarketStalls;
using Application.StallTypes.Commands.CreateStallType;
using Application.StallTypes.Commands.CreateStallTypes;
using Application.StallTypes.Commands.EditStallTypes;
using Application.StallTypes.Queries.GetMarketStallTypes;
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
        public async Task<ActionResult<CreateMarketResponse>> CreateMarket(CreateMarketRequest dto)
        {

                CreateMarketResponse marketResponse = await Mediator.Send(new CreateMarketCommand() { Dto = dto });
                return marketResponse;
        }

        [HttpGet("instance/{id}")]
        public async Task<ActionResult<GetMarketInstanceQueryResponse>> GetMarketInstance([FromRoute] string id)
        { 
            int marketId = int.Parse(id);
            var marketResponse = await Mediator.Send(
                new GetMarketInstanceQuery()
                {
                    Dto = new GetMarketInstanceQueryRequest() { MarketId = marketId }
                });
            var stallResponse = await Mediator.Send(
                new GetMarketStallsQuery()
                {
                    Dto = new GetMarketStallsRequest() { MarketId = marketId }
                });
            
            marketResponse.Market.Stalls = stallResponse.Stalls;
            var stallTypeResponse = await Mediator.Send(
                    new GetMarketStallTypesQuery()
                    {
                        Dto = new GetMarketStallTypesRequest() { MarketId= marketId }
                    }
                );
            marketResponse.Market.StallTypes = stallTypeResponse.StallTypes;
            return marketResponse;
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

            return await Mediator.Send(new EditMarketCommand() { Dto = dto }); ;

        }

        /*** 
         * Add stalls to a market. 
         * Calls a second command to populate the stall response with market data.
         */
        [HttpPost("addStalls")]
        public async Task<ActionResult<AddStallsToMarketResponse>> AddStallsToMarket(AddStallsToMarketRequest dto)
        {
            Context.Database.BeginTransaction();
            try
            {
                var stallResponse = await Mediator.Send(new AddStallsToMarketCommand() { Dto = dto });
                if(!stallResponse.Succeeded)
                {
                    Context.Database.RollbackTransaction();
                    return BadRequest();
                }

                var marketResponse = await Mediator.Send(new GetMarketInstanceQuery() { Dto = new GetMarketInstanceQueryRequest() { MarketId = dto.MarketId } });
                foreach(var stall in stallResponse.Stalls)
                {
                    stall.Market = marketResponse.Market;
                }

                Context.Database.CommitTransaction();
                return stallResponse;
            }
            catch(Exception ex)
            {
                Context.Database.RollbackTransaction();
                throw;
            }
            
        }

        [HttpPost("bookstalls")]
        public async Task<ActionResult<BookStallsResponse>> BookStalls(BookStallsRequest dto)
        {
            return await Mediator.Send(new BookStallsCommand() { Dto = dto });
        }
    }
}

using Application.Common.Exceptions;
using Application.Markets.Commands.BookStalls;
using Application.Markets.Commands.CancelMarket;
using Application.Markets.Commands.CreateMarket;
using Application.Markets.Commands.EditMarket;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetFilteredMarkets;
using Application.Markets.Queries.GetMarketInstance;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Stalls.Commands.AddStallsToMarket;
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

                CreateMarketResponse marketResponse = await Mediator.Send(new CreateMarketCommand() { Dto = dto });
                return marketResponse;
        }

        [HttpGet("instance/{id}")]
        public async Task<ActionResult<GetMarketInstanceQueryResponse>> GetMarketInstance([FromRoute] string id)
        {
            int marketId;
            try
            {
                marketId = int.Parse(id);
            } catch(Exception ex)
            {
                throw new ValidationException($"Invalid market id {id}.");
            }
            
            return await Mediator.Send(new GetMarketInstanceQuery() { Dto = new GetMarketInstanceQueryRequest() { MarketId = marketId } });
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
            [FromQuery] bool? isCancelled,
            [FromQuery] int? organiserId,
            [FromQuery] DateTimeOffset? startDate,
            [FromQuery] DateTimeOffset? endDate,
            [FromQuery] string[] categories
        )
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                OrganiserId = organiserId,
                HideCancelled = isCancelled,
                StartDate = startDate,
                EndDate = endDate,
                Categories = new List<string>(categories)
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
            return await Mediator.Send(new GetUsersMarketsQuery());
        }

        [HttpPut]
        public async Task<ActionResult<EditMarketResponse>> UpdateMarket(EditMarketRequest dto)
        {

            return await Mediator.Send(new EditMarketCommand() { Dto = dto }); ;

        }

        [HttpPost("addStalls")]
        public async Task<ActionResult<AddStallsToMarketResponse>> AddStallsToMarket(AddStallsToMarketRequest dto)
        {

                var stallResponse = await Mediator.Send(new AddStallsToMarketCommand() { Dto = dto });
                return stallResponse;           
        }

        [HttpPost("bookstalls")]
        public async Task<ActionResult<BookStallsResponse>> BookStalls(BookStallsRequest dto)
        {
            return await Mediator.Send(new BookStallsCommand() { Dto = dto });
        }
    }
}

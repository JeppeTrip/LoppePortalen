using Application.Common.Exceptions;
using Application.Markets.Commands.BookStalls;
using Application.Markets.Commands.CancelMarket;
using Application.Markets.Commands.CreateMarket;
using Application.Markets.Commands.EditMarket;
using Application.Markets.Commands.UploadMarketBanner;
using Application.Markets.Queries.GetAllMarkets;
using Application.Markets.Queries.GetFilteredMarkets;
using Application.Markets.Queries.GetMarketInstance;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Stalls.Commands.AddStallsToMarket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            
            return await Mediator.Send(new GetMarketInstanceQuery() { Dto = new GetMarketInstanceRequest() { MarketId = marketId } });
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
            [FromQuery] string[] categories,
            [FromQuery] double? x,
            [FromQuery] double? y,
            [FromQuery] double? distance
        )
        {
            //only fill in distance parameters if all of them are set.
            DistanceParameters parameters = null;
            if(x != null && y != null && distance != null)
                parameters = new DistanceParameters((double) x, (double) y, (double) distance);

            var request = new GetFilteredMarketsQueryRequest()
            {
                OrganiserId = organiserId,
                HideCancelled = isCancelled,
                StartDate = startDate,
                EndDate = endDate,
                Categories = new List<string>(categories),
                DistanceParams = parameters
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

        [HttpPut("banner/upload")]
        public async Task<ActionResult<UploadMarketBannerResponse>> UploadMarketBanner(int marketId, IFormFile image)
        {
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);

                    var request = new UploadMarketBannerRequest() { MarketId = marketId, Title = "banner", ImageData = fileBytes };
                    var command = new UploadMarketBannerCommand() { Dto = request };
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

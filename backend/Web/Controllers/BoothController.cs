using Application.Booths.Commands.UpdateBooth;
using Application.Booths.Queries.GetBooth;
using Application.Common.Models;
using Application.Markets.Queries.GetMarketInstance;
using Application.Stalls.Queries.GetStall;
using Application.StallTypes.Queries.GetStallType;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BoothController : ApiBase
    {
        [HttpGet]
        public async Task<ActionResult<Booth>> GetBooth([FromQuery] string id)
        {
            throw new NotImplementedException();
            /*
            var boothResponse = await Mediator.Send(new GetBoothQuery() { Dto = new GetBoothRequest() { Id = id } });

            //for each booth find the related stalls, their stall type, and their related market.
            Booth booth;
            var stallResponse = await Mediator.Send(new GetStallQuery()
            {
                Dto = new GetStallRequest() { StallId = boothResponse.StallId }
            });

            var stallTypeResponse = await Mediator.Send(new GetStallTypeQuery() { 
                Dto = new GetStallTypeRequest() { StallTypeId = stallResponse.StallTypeId } 
            });
            var marketResponse = await Mediator.Send(new GetMarketInstanceQuery() { 
                Dto = new GetMarketInstanceQueryRequest() { MarketId = stallResponse.MarketInstanceId } 
            });
            booth = new Booth()
            {
                Id = boothResponse.Id,
                BoothName = boothResponse.BoothName,
                BoothDescription = boothResponse.BoothDescription,
                Stall = new Stall()
                {
                    Id = stallResponse.StallId,
                    Market = marketResponse.Market,
                    StallType = new StallType()
                    {
                        Id = stallTypeResponse.StallTypeId,
                        Name = stallTypeResponse.Name,
                        Description = stallTypeResponse.Description,
                        Market = marketResponse.Market,
                        TotalStallCount = 0

                    }
                }
            };

            return booth;
            */
        }

        [HttpPut]
        public async Task<ActionResult<UpdateBoothResponse>> UpdateBooth(UpdateBoothRequest dto)
        {
            return await Mediator.Send(new UpdateBoothCommand() { Dto = dto });
        }
    }
}

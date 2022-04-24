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
        public async Task<ActionResult<BoothBaseVM>> GetBooth([FromQuery] string id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult<UpdateBoothResponse>> UpdateBooth(UpdateBoothRequest dto)
        {
            return await Mediator.Send(new UpdateBoothCommand() { Dto = dto });
        }
    }
}

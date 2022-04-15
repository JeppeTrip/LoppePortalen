using Application.Stalls.Queries.GetMarketStalls;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class StallController : ApiBase
    {
        [HttpGet("{marketId}")]
        public async Task<ActionResult<GetMarketStallsResponse>> GetStallsForMarket([FromRoute] int marketId)
        {

            return await Mediator.Send(new GetMarketStallsQuery() { Dto = new GetMarketStallsRequest() { MarketId = marketId } });
        }
    }
}

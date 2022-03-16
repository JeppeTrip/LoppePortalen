using Application.Markets.Commands.CreateMarket;
using Microsoft.AspNetCore.Mvc;
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

    }
}

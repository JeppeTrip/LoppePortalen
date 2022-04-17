using Application.Merchants.Commands.CreateMerchant;
using Application.Merchants.Queryies.AllMerchants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MerchantController : ApiBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateMerchantResponse>> CreateMerchant(CreateMerchantRequest dto)
        {
            return await Mediator.Send(new CreateMerchantCommand() { Dto = dto });
        }

        [HttpGet]
        public async Task<ActionResult<AllMerchantsQueryResponse>> GetAllMerchants()
        {
            return await Mediator.Send(new AllMerchantsQuery());
        }
    }
}

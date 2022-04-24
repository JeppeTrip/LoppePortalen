using Application.Booths.Queries.GetUsersBooths;
using Application.Common.Models;
using Application.Markets.Queries.GetMarketInstance;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Merchants.Queries.GetUsersMerchants;
using Application.Organisers.Queries.GetUsersOrganisers;
using Application.Stalls.Queries.GetStall;
using Application.StallTypes.Queries.GetStallType;
using Application.User.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class UserController : ApiBase
    {
        [HttpGet]
        public async Task<ActionResult<GetUserResponse>> GetUserInfo()
        {
            var userId = CurrentUserService.UserId;
            return await Mediator.Send(new GetUserQuery() { Dto = new GetUserRequest() { UserId = userId } });
        }

        [HttpPut]
        public async Task<ActionResult<GetUserResponse>> UpdateUserInfo()
        {
            var userId = CurrentUserService.UserId;
            return await Mediator.Send(new GetUserQuery() { Dto = new GetUserRequest() { UserId = userId } });
        }

        [HttpGet("markets")]
        public async Task<ActionResult<GetUsersMarketsResponse>> GetUsersMarkets()
        {
            return await Mediator.Send(new GetUsersMarketsQuery() );
        }

        [HttpGet("organisers")]
        public async Task<ActionResult<GetUsersOrganisersResponse>> GetUsersOrganisers()
        {
            var userid = CurrentUserService.UserId;
            return await Mediator.Send(new GetUsersOrganisersQuery());
        }

        [HttpGet("merchants")]
        public async Task<ActionResult<GetUsersMerchantsResponse>> GetUsersMerchants()
        {
            return await Mediator.Send(new GetUsersMerchantsQuery());
        }

        [HttpGet("booths")]
        public async Task<ActionResult<List<BoothBaseVM>>> GetUsersBooths()
        {
            throw new NotImplementedException();
        }

    }
}

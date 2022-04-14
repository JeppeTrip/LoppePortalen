using Application.Markets.Queries.GetUsersMarkets;
using Application.User.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var userid = CurrentUserService.UserId;
            return await Mediator.Send(new GetUsersMarketsQuery() { Dto = new GetUsersMarketsRequest() { UserId = userid } });
        }

        [HttpGet("oragnisers")]
        public async Task<ActionResult<GetUsersMarketsResponse>> GetUsersOrganisers()
        {
            throw new NotImplementedException();
        }
    }
}

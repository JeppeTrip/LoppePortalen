using Application.User.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;
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
    }
}

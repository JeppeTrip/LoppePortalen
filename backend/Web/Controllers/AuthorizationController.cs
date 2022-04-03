using Application.User.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AuthorizationController : ApiBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(string email, string passWord)
        {
            return await Mediator.Send(new CreateUserCommand()
            {
                Dto = new CreateUserRequest()
                {
                    UserName = email,
                    Password = passWord
                }
            });
        }
    }
}

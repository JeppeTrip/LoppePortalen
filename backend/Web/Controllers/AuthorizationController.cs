using Application.User.Commands.AuthenticateUser;
using Application.User.Commands.CreateUser;
using Application.User.Commands.RefreshJwt;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AuthorizationController : ApiBase
    {
        //TODO: move this over into the user controller probably.
        //TODO: should I return tokens here at all?
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserRequest dto)
        {
            return await Mediator.Send(new RegisterUserCommand()
            {
                Dto = dto
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticateUserResponse>> AuthenticateUser([FromBody] AuthenticateUserRequest dto)
        {
            return await Mediator.Send(new AuthenticateUserCommand()
            {
                Dto = dto
            });
        } 

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<RefreshJwtResponse>> RefreshToken([FromBody] RefreshJwtRequest dto)
        {
            return await Mediator.Send(new RefreshJwtCommand()
            {
                Dto = dto
            });
        }
    }
}

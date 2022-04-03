﻿using Application.User.Commands.CreateUser;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AuthorizationController : ApiBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserRequest dto)
        {
            return await Mediator.Send(new RegisterUserCommand()
            {
                Dto = dto
            });
        }
    }
}

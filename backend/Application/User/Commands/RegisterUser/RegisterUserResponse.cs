using Application.Common.Models;
using System.Collections.Generic;

namespace Application.User.Commands.CreateUser
{
    public class RegisterUserResponse : AuthResult
    {
        public RegisterUserResponse(bool succeeded, IEnumerable<string> errors, string token, string refreshToken) : base(succeeded, errors, token, refreshToken)
        {
        }
    }
}

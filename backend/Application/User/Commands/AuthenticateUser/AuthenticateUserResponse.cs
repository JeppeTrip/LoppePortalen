using Application.Common.Models;
using System.Collections.Generic;

namespace Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserResponse : AuthResult
    {
        public AuthenticateUserResponse(bool succeeded, IEnumerable<string> errors, string token, string refreshToken) : base(succeeded, errors, token, refreshToken)
        {
        }
    }
}

using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class RegisterUserResponse : AuthResult
    {
        public RegisterUserResponse(bool succeeded, IEnumerable<string> errors, string token) : base(succeeded, errors, token)
        {
        }
    }
}

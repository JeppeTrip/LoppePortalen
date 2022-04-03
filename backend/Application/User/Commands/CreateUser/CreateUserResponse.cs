using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserResponse : Result
    {
        public string? UserId { get; set; }
        public CreateUserResponse(bool succeeded, IEnumerable<string> errors, string userId) : base(succeeded, errors)
        {
            UserId = userId;
        }
    }
}

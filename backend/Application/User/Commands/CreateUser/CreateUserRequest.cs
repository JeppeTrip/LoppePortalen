using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

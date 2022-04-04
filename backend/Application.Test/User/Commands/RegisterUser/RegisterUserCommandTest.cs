using Application.User.Commands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.User.Commands.RegisterUser
{
    public class RegisterUserCommandTest : TestBase
    {
        [Fact(Skip="TODO: Implement mock user manager.")]
        public async Task Handle_RegisterUser()
        {
            var request = new RegisterUserRequest()
            {

            };
            var command = new RegisterUserCommand()
            {
                Dto = request
            };
        }
    }
}

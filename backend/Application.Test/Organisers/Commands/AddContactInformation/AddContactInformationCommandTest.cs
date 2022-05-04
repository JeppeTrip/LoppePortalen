using Application.Common.Exceptions;
using Application.Organisers.Commands.AddContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.AddContactInformation
{
    public class AddContactInformationCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_AddContactInfo()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1900,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User1900"));
            var result = await handler.Handle(command, CancellationToken.None);

            //figure out what to return loll
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1900,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("DoesNotExist"));
            var result =

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1900,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserDoesNotExist()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = -1,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User1900"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_OrganiserExistOnOtherUser()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1901,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User1900"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_ContactIfnoAlreadyExists()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1901,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User1901"));

            await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));

        }
    }
}

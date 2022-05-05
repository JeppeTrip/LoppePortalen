using Application.Common.Exceptions;
using Application.Organisers.Commands.RemoveContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.RemoveContactInformation
{
    public class RemoveContactInformationCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_RemoveContactInformation()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2100, Value = "info 1" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("User2100"));
            var result = await handler.Handle(command, CancellationToken.None);

            //todo wtf todo with this result ? 
        }

        [Fact]
        public async void Handle_OrganiserDoesNotExist()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = -1, Value = "info 1" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("User2100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2100, Value = "info 1" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2100, Value = "info 1" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserOwnedByOtherUser()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2101, Value = "info 1" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("User2100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ContactInfoDoesNotExist()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2100, Value = "DoesNotExist" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("User2100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ContactInfoNotOnUsersOrganiser()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 2100, Value = "info 2" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var handler = new RemoveContactInformationCommand.RemoveContactInformationCommandHandler(Context, new CurrentUserService("User2100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

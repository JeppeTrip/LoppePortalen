using Application.Common.Exceptions;
using Application.Merchants.Commands.AddContactInformation;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.AddContactInformation
{
    public class AddContactInformationCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_AddContactInfo()
        {
            var request = new AddContactInformationRequest()
            {
                MerchantId = 2900,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User2900"));
            var result = await handler.Handle(command, CancellationToken.None);

            //figure out what to return loll
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new AddContactInformationRequest()
            {
                MerchantId = 2900,
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
                MerchantId = 2900,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantDoesNotExist()
        {
            var request = new AddContactInformationRequest()
            {
                MerchantId = -1,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User2900"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_MerchantExistOnOtherUser()
        {
            var request = new AddContactInformationRequest()
            {
                MerchantId = 2901,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User2900"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_ContactIfnoAlreadyExists()
        {
            var request = new AddContactInformationRequest()
            {
                MerchantId = 2901,
                Type = 0,
                Value = "value"
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var handler = new AddContactInformationCommand.AddContactInformationCommandHandler(Context, new CurrentUserService("User2901"));

            await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));

        }
    }
}

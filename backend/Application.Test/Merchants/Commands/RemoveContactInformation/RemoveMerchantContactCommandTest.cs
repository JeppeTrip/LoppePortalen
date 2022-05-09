using Application.Common.Exceptions;
using Application.Merchants.Commands.RemoveContactInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.RemoveContactInformation
{
    public class RemoveMerchantContactCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_RemoveContactInformation()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3100, Value = "info 1" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("User3100"));
            var result = await handler.Handle(command, CancellationToken.None);

            //todo wtf todo with this result ? 
        }

        [Fact]
        public async void Handle_MerchantDoesNotExist()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = -1, Value = "info 1" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("User3100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3100, Value = "info 1" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3100, Value = "info 1" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantOwnedByOtherUser()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3101, Value = "info 1" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("User3100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ContactInfoDoesNotExist()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3100, Value = "DoesNotExist" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("User3100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ContactInfoNotOnUsersMerchant()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 3100, Value = "info 2" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var handler = new RemoveMerchantContactCommand.RemoveMerchantContactCommandHandler(Context, new CurrentUserService("User3100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

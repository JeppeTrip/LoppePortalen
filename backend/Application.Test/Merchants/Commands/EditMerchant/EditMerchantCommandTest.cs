using Application.Common.Exceptions;
using Application.Merchants.Commands.EditMerchant;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.EditMerchant
{
    public class EditMerchantCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_EditMerchant()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "New Merchant 2000",
                Description = "New Merchant 2000 description"
            };
            var command = new EditMerchantCommand() { Dto = request };
            var handler = new EditMerchantCommand.EditMerchantCommandHandler(Context, new CurrentUserService("EditMerchantUser"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NonExistantMerchant()
        {
            var request = new EditMerchantRequest()
            {
                Id = -1,
                Name = "New Merchant 2000",
                Description = "New Merchant 2000 description"
            };
            var command = new EditMerchantCommand() { Dto = request };
            var handler = new EditMerchantCommand.EditMerchantCommandHandler(Context, new CurrentUserService("EditMerchantUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistantUser()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "New Merchant 2000",
                Description = "New Merchant 2000 description"
            };
            var command = new EditMerchantCommand() { Dto = request };
            var handler = new EditMerchantCommand.EditMerchantCommandHandler(Context, new CurrentUserService("NotAUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NotUsersMerchant()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "New Merchant 2000",
                Description = "New Merchant 2000 description"
            };
            var command = new EditMerchantCommand() { Dto = request };
            var handler = new EditMerchantCommand.EditMerchantCommandHandler(Context, new CurrentUserService("EditMerchantFakeUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

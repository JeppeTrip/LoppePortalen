using Application.Merchants.Commands.CreateMerchant;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CreateMerchant()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "Merchant",
                Description = "Create Merchant Test",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var handler = new CreateMerchantCommand.CreateMerchantCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Merchant.Id.Should().BePositive();
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "Merchant",
                Description = "Create Merchant Test",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var handler = new CreateMerchantCommand.CreateMerchantCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

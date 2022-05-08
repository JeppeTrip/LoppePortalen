using Application.Common.Exceptions;
using Application.Markets.Commands.CancelMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.CancelMarketInstance
{
    public class CancelMarketInstanceCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CancelMarket()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 2400 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService("User2400"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.IsCancelled.Should().BeTrue();
            result.MarketId.Should().Be(2400);

            var market = Context.MarketInstances.FirstOrDefault(x => x.Id == request.MarketId);
            market.IsCancelled.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 2400 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 2400 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketOwnedByOtherUser()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 2401 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService("User2400"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

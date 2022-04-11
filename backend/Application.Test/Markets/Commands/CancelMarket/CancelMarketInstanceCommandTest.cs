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

namespace Application.Test.Markets.Commands.CancelMarket
{
    public class CancelMarketInstanceCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CancelMarketInstance()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 1 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var Handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            var result = await Handler.Handle(command, CancellationToken.None);
            
            result.Should().NotBeNull();
            result.MarketId.Should().Be(request.MarketId);
            result.IsCancelled.Should().BeTrue();

            var entity = Context.MarketInstances.FirstOrDefault(x => x.Id == request.MarketId);
            entity.Should().NotBeNull();
            entity.IsCancelled.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NoSuchMarketInstance()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = -1 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var Handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await Handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_WrongUserId()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 1 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var Handler = new CancelMarketInstanceCommand.CancelMarketInstanceCommandHandler(Context, new CurrentUserService("-1"));

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await Handler.Handle(command, CancellationToken.None);
            });
        }
    }
}

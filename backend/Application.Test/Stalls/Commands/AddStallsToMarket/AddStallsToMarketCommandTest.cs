using Application.Common.Exceptions;
using Application.Stalls.Commands.AddStallsToMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_AddOneStallToMarket()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Stalls.Should().NotBeEmpty();
            result.Stalls.Count().Should().Be(1);
            result.Stalls.First().Id.Should().BePositive();
            result.Stalls.First().StallType.Id.Should().Be(1100);
            result.Stalls.First().StallType.Name.Should().Be("Stalltype1100");
            result.Stalls.First().StallType.Description.Should().Be("Stalltype1100 Description");
        }

        [Fact]
        public async Task Handle_AddMultipleStallsToMarket()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 3
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Stalls.Should().NotBeEmpty();
            result.Stalls.Count().Should().Be(3);
            result.Stalls.ForEach(st => st.Id.Should().BePositive());
            result.Stalls.ForEach(st => st.StallType.Id.Should().Be(1100));
            result.Stalls.ForEach(st => st.StallType.Name.Should().Be("Stalltype1100"));
            result.Stalls.ForEach(st => st.StallType.Description.Should().Be("Stalltype1100 Description"));
        }

        [Fact]
        public async Task Handle_AddZeroStallsToMarket()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 0
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Stalls.Should().BeEmpty();
    
        }

        [Fact]
        public async Task Handle_MarketInstanceDoesNotExist()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = -1,
                StallTypeId = 1100,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeDoesNotExist()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = -1,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeExistOnOtherMarket()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1101,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1100"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotOwnMarket()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService("User1101"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserIsNull()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1100,
                StallTypeId = 1100,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var handler = new AddStallsToMarketCommand.AddStallsToMarketCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

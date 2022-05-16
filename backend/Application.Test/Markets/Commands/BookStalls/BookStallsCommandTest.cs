using Application.Common.Exceptions;
using Application.Markets.Commands.BookStalls;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.BookStalls
{
    public class BookStallsCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_BookOneStall()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();

            Context.Bookings.Where(x => x.MerchantId == 2500).Count().Should().Be(1);   
        }

        [Fact]
        public async Task Handle_BookMultipleStalls()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 2
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();

            Context.Bookings.Where(x => x.MerchantId == 2500).Count().Should().Be(2);
        }

        [Fact]
        public async Task Handle_BookMultipleTypesOfStalls()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                },
                new StallBooking()
                {
                    StallTypeId = 2501,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();

            Context.Bookings.Where(x => x.MerchantId == 2500).Count().Should().Be(2);
        }

        [Fact]
        public async Task Handle_BookStallTypeThatDoesNotExist()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = -1,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantOwnedByOtherUser()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2501,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantDoesNotExist()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = -1,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketDoesNotExist()
        {
            var request = new BookStallsRequest()
            {
                MarketId = -1,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NotEnoughOfStallType()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2500,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2500,
                    BookingAmount = 3
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_BookStallOnCancelledMarket()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 2503,
                MerchantId = 2500,
                Stalls = new List<StallBooking>() { new StallBooking()
                {
                    StallTypeId = 2502,
                    BookingAmount = 1
                }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var handler = new BookStallsCommand.BookStallsCommandHandler(Context, new CurrentUserService("User2500"));
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

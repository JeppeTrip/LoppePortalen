using Application.Common.Exceptions;
using Application.Markets.Commands.EditMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.EditMarket
{
    public class EditMarketCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_EditMarket()
        {
            var request = new EditMarketRequest() {
                OrganiserId = 2300,
                MarketId = 2300,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("User2300"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_MarketRelatedToUsersOtherOrganiser()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 2300,
                MarketId = 2301,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("User2300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserOwnedByOtherUser()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 2302,
                MarketId = 2302,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("User2300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserDoesNotExist()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = -1,
                MarketId = 2300,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("User2300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

        }

        [Fact]
        public async Task Handle_MarketDoesNotExist()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 2300,
                MarketId = -1,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("User2300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 2300,
                MarketId = 2300,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 2300,
                MarketId = 2300,
                MarketName = "NewName",
                Description = "NewDescription",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(3)
            };
            var command = new EditMarketCommand() { Dto = request };
            var handler = new EditMarketCommand.EditMarketCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

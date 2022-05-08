using Application.Common.Exceptions;
using Application.Markets.Commands.CreateMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.CreateMarket
{
    public class CreateMarketCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CreateMarket()
        {
            var request = new CreateMarketRequest() { 
                OrganiserId = 2200, 
                MarketName="name", 
                Description="description", 
                StartDate=DateTimeOffset.Now, 
                EndDate=DateTimeOffset.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService("User2200"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Market.Should().NotBeNull();
            result.Market.MarketName.Should().Be(request.MarketName);
            result.Market.Description.Should().Be(request.Description);
            result.Market.StartDate.Should().Be(request.StartDate);
            result.Market.EndDate.Should().Be(request.EndDate);
            result.Market.Categories.Should().BeEmpty();
            result.Market.AvailableStallCount.Should().Be(0);
            result.Market.TotalStallCount.Should().Be(0);
            result.Market.OccupiedStallCount.Should().Be(0);
            result.Market.IsCancelled.Should().BeFalse();

            result.Market.Organiser.Should().NotBeNull();
            result.Market.Organiser.Name.Should().Be("Organiser 2200");
            result.Market.Organiser.Description.Should().Be("Organiser 2200 Description");
            result.Market.Organiser.Street.Should().Be("street");
            result.Market.Organiser.StreetNumber.Should().Be("number");
            result.Market.Organiser.Appartment.Should().Be("apt");
            result.Market.Organiser.PostalCode.Should().Be("postal");
            result.Market.Organiser.City.Should().Be("city");
            result.Market.Organiser.UserId.Should().Be("User2200");
        }

        [Fact]
        public async Task Handle_OrganiserOwnedByOtherUser()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 2201,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService("User2200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserDoesNotExist()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = -1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService("User2200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 2200,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 2200,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

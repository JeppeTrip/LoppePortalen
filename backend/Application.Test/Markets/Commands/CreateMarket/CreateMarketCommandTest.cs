using Application.Common.Exceptions;
using Application.Markets.Commands.CreateMarket;
using Domain.EntityExtensions;
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
                EndDate=DateTimeOffset.Now.AddDays(1),
                Address="address",
                City="city",
                PostalCode="postalcode"
            };
            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService("User2200"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Market.Should().NotBeNull();
            result.Market.MarketName.Should().Be(request.MarketName);
            result.Market.Description.Should().Be(request.Description);
            result.Market.StartDate.Should().Be(request.StartDate);
            result.Market.EndDate.Should().Be(request.EndDate);
            result.Market.Address.Should().Be(request.Address);
            result.Market.PostalCode.Should().Be(request.PostalCode);
            result.Market.City.Should().Be(request.City);
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

            var market = Context.MarketInstances.First(x => x.Id == result.Market.MarketId);
            market.Should().NotBeNull();
            market.MarketTemplate.Name.Should().Be(request.MarketName);
            market.MarketTemplate.Description.Should().Be(request.Description);
            market.MarketTemplate.Address.Should().Be(request.Address);
            market.MarketTemplate.City.Should().Be(request.City);
            market.MarketTemplate.PostalCode.Should().Be(request.PostalCode);
            market.StartDate.Should().Be(request.StartDate);
            market.EndDate.Should().Be(request.EndDate);
            market.IsCancelled.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_CreateMarket_NoAddress()
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

            var market = Context.MarketInstances.First(x => x.Id == result.Market.MarketId);
            market.Should().NotBeNull();
            market.MarketTemplate.Name.Should().Be(request.MarketName);
            market.MarketTemplate.Description.Should().Be(request.Description);
            market.MarketTemplate.Address.Should().BeNull();
            market.MarketTemplate.City.Should().BeNull();
            market.MarketTemplate.PostalCode.Should().BeNull();
            market.StartDate.Should().Be(request.StartDate);
            market.EndDate.Should().Be(request.EndDate);
            market.IsCancelled.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_CreateMarket_AddressNull()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 2200,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                Address = null,
                PostalCode = null,
                City = null
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

            var market = Context.MarketInstances.First(x => x.Id == result.Market.MarketId);
            market.Should().NotBeNull();
            market.MarketTemplate.Name.Should().Be(request.MarketName);
            market.MarketTemplate.Description.Should().Be(request.Description);
            market.MarketTemplate.Address.Should().BeNull();
            market.MarketTemplate.City.Should().BeNull();
            market.MarketTemplate.PostalCode.Should().BeNull();
            market.StartDate.Should().Be(request.StartDate);
            market.EndDate.Should().Be(request.EndDate);
            market.IsCancelled.Should().BeFalse();
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

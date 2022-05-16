using Application.Markets.Queries.GetUsersMarkets;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetUsersMarkets()
        {
            var query = new GetUsersMarketsQuery();
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService("User2700"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Markets.Count().Should().Be(2);

            result.Markets.First(x => x.MarketId == 2700).MarketId.Should().Be(2700);
            result.Markets.First(x => x.MarketId == 2700).MarketName.Should().Be("Market 2700 name");
            result.Markets.First(x => x.MarketId == 2700).Description.Should().Be("Market 2700 description");
            result.Markets.First(x => x.MarketId == 2700).StartDate.Should().BeSameDateAs(DateTimeOffset.Now);
            result.Markets.First(x => x.MarketId == 2700).EndDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(1));
            result.Markets.First(x => x.MarketId == 2700).IsCancelled.Should().BeFalse();
            result.Markets.First(x => x.MarketId == 2700).AvailableStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2700).OccupiedStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2700).TotalStallCount.Should().Be(2);
            result.Markets.First(x => x.MarketId == 2700).Categories.Count().Should().Be(2);
            result.Markets.First(x => x.MarketId == 2700).Categories.Should().Contain("category 2700");
            result.Markets.First(x => x.MarketId == 2700).Categories.Should().Contain("category 2701");
            result.Markets.First(x => x.MarketId == 2700).Address.Should().Be("address 2700");
            result.Markets.First(x => x.MarketId == 2700).PostalCode.Should().Be("postal 2700");
            result.Markets.First(x => x.MarketId == 2700).City.Should().Be("city 2700");

            result.Markets.First(x => x.MarketId == 2700).Organiser.Id.Should().Be(2700);
            result.Markets.First(x => x.MarketId == 2700).Organiser.Name.Should().Be("Organiser 2700");
            result.Markets.First(x => x.MarketId == 2700).Organiser.Description.Should().Be("Organiser 2700 Description");
            result.Markets.First(x => x.MarketId == 2700).Organiser.UserId.Should().Be("User2700");
            result.Markets.First(x => x.MarketId == 2700).Organiser.Street.Should().Be("street");
            result.Markets.First(x => x.MarketId == 2700).Organiser.StreetNumber.Should().Be("number");
            result.Markets.First(x => x.MarketId == 2700).Organiser.Appartment.Should().Be("apt");
            result.Markets.First(x => x.MarketId == 2700).Organiser.City.Should().Be("city");
            result.Markets.First(x => x.MarketId == 2700).Organiser.PostalCode.Should().Be("postal");

            result.Markets.First(x => x.MarketId == 2701).MarketId.Should().Be(2701);
            result.Markets.First(x => x.MarketId == 2701).MarketName.Should().Be("Market 2701 name");
            result.Markets.First(x => x.MarketId == 2701).Description.Should().Be("Market 2701 description");
            result.Markets.First(x => x.MarketId == 2701).StartDate.Should().BeSameDateAs(DateTimeOffset.Now);
            result.Markets.First(x => x.MarketId == 2701).EndDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(1));
            result.Markets.First(x => x.MarketId == 2701).IsCancelled.Should().BeFalse();
            result.Markets.First(x => x.MarketId == 2701).AvailableStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2701).OccupiedStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2701).TotalStallCount.Should().Be(2);
            result.Markets.First(x => x.MarketId == 2701).Categories.Count().Should().Be(2);
            result.Markets.First(x => x.MarketId == 2701).Categories.Should().Contain("category 2701");
            result.Markets.First(x => x.MarketId == 2701).Categories.Should().Contain("category 2702");
            result.Markets.First(x => x.MarketId == 2701).Address.Should().BeNull();
            result.Markets.First(x => x.MarketId == 2701).PostalCode.Should().BeNull();
            result.Markets.First(x => x.MarketId == 2701).City.Should().BeNull();

            result.Markets.First(x => x.MarketId == 2701).Organiser.Id.Should().Be(2701);
            result.Markets.First(x => x.MarketId == 2701).Organiser.Name.Should().Be("Organiser 2701");
            result.Markets.First(x => x.MarketId == 2701).Organiser.Description.Should().Be("Organiser 2701 Description");
            result.Markets.First(x => x.MarketId == 2701).Organiser.UserId.Should().Be("User2700");
            result.Markets.First(x => x.MarketId == 2701).Organiser.Street.Should().Be("street");
            result.Markets.First(x => x.MarketId == 2701).Organiser.StreetNumber.Should().Be("number");
            result.Markets.First(x => x.MarketId == 2701).Organiser.Appartment.Should().Be("apt");
            result.Markets.First(x => x.MarketId == 2701).Organiser.City.Should().Be("city");
            result.Markets.First(x => x.MarketId == 2701).Organiser.PostalCode.Should().Be("postal");
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var query = new GetUsersMarketsQuery();
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService("DoesNotExist"));
            var result = await handler.Handle(query, CancellationToken.None);
            
            result.Should().NotBeNull();
            result.Markets.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var query = new GetUsersMarketsQuery();
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService(null));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Markets.Count().Should().Be(0);
        }

    }
}

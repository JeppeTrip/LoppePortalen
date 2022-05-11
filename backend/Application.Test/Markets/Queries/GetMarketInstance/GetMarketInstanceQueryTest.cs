using Application.Common.Exceptions;
using Application.Markets.Queries.GetMarketInstance;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetMarketInstance
{
    public class GetMarketInstanceQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetMarketInstance()
        {
            var request = new GetMarketInstanceRequest() { MarketId = 2600 };
            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Market.Should().NotBeNull();

            result.Market.MarketId.Should().Be(2600);
            result.Market.MarketName.Should().Be("Market 2600 name");
            result.Market.Description.Should().Be("Market 2600 description");
            result.Market.StartDate.Should().BeSameDateAs(DateTimeOffset.Now);
            result.Market.EndDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(1));
            result.Market.IsCancelled.Should().BeFalse();
            result.Market.AvailableStallCount.Should().Be(1);
            result.Market.OccupiedStallCount.Should().Be(2);
            result.Market.TotalStallCount.Should().Be(3);
            result.Market.Categories.Count().Should().Be(3);
            result.Market.Categories.Should().Contain("category 2600");
            result.Market.Categories.Should().Contain("category 2601");
            result.Market.Categories.Should().Contain("category 2602");
            result.Market.Address.Should().BeNull();
            result.Market.PostalCode.Should().BeNull();
            result.Market.City.Should().BeNull();
            result.Market.ImageData.Should().NotBeEmpty();

            result.Market.Organiser.Id.Should().Be(2600);
            result.Market.Organiser.Name.Should().Be("Organiser 2600");
            result.Market.Organiser.Description.Should().Be("Organiser 2600 Description");
            result.Market.Organiser.UserId.Should().Be("User2600");
            result.Market.Organiser.Street.Should().Be("street");
            result.Market.Organiser.StreetNumber.Should().Be("number");
            result.Market.Organiser.Appartment.Should().Be("apt");
            result.Market.Organiser.City.Should().Be("city");
            result.Market.Organiser.PostalCode.Should().Be("postal");

            result.Market.StallTypes.Count().Should().Be(2);
            result.Market.StallTypes.First(x => x.Id == 2600).Should().NotBeNull();
            result.Market.StallTypes.First(x => x.Id == 2600).Name.Should().Be("Stalltype 2600");
            result.Market.StallTypes.First(x => x.Id == 2600).Description.Should().Be("Stalltype 2600 description");

            result.Market.Stalls.Count().Should().Be(3);

            result.Market.Booths.Count().Should().Be(2);
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Should().NotBeNull();
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Name.Should().Be("booking 2600");
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Description.Should().Be("booking 2600 description");
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Stall.Id.Should().Be(2602);
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Stall.StallType.Id.Should().Be(2601);
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Stall.StallType.Name.Should().Be("Stalltype 2601");
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Stall.StallType.Description.Should().Be("Stalltype 2601 description");
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Categories.Count().Should().Be(2);
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Categories.Should().Contain("category 2600");
            result.Market.Booths.First(x => x.Id.Equals("Booking2600")).Categories.Should().Contain("category 2601");
        }

        [Fact]
        public async Task Handle_GetMarketInstance_WithAddress()
        {
            var request = new GetMarketInstanceRequest() { MarketId = 2601 };
            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Market.Should().NotBeNull();

            result.Market.MarketId.Should().Be(2601);
            result.Market.MarketName.Should().Be("Market 2601 name");
            result.Market.Description.Should().Be("Market 2601 description");
            result.Market.StartDate.Should().BeSameDateAs(DateTimeOffset.Now);
            result.Market.EndDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(1));
            result.Market.IsCancelled.Should().BeFalse();
            result.Market.AvailableStallCount.Should().Be(0);
            result.Market.OccupiedStallCount.Should().Be(0);
            result.Market.TotalStallCount.Should().Be(0);
            result.Market.Categories.Should().BeEmpty();
            result.Market.Address.Should().Be("address");
            result.Market.PostalCode.Should().Be("postal");
            result.Market.City.Should().Be("city");
            result.Market.ImageData.Should().BeNull();

            result.Market.Organiser.Id.Should().Be(2600);
            result.Market.Organiser.Name.Should().Be("Organiser 2600");
            result.Market.Organiser.Description.Should().Be("Organiser 2600 Description");
            result.Market.Organiser.UserId.Should().Be("User2600");
            result.Market.Organiser.Street.Should().Be("street");
            result.Market.Organiser.StreetNumber.Should().Be("number");
            result.Market.Organiser.Appartment.Should().Be("apt");
            result.Market.Organiser.City.Should().Be("city");
            result.Market.Organiser.PostalCode.Should().Be("postal");

            result.Market.StallTypes.Should().BeEmpty();

            result.Market.Stalls.Should().BeEmpty();

            result.Market.Booths.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_MarketDoesNotExist()
        {
            var request = new GetMarketInstanceRequest() { MarketId = -1};
            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}

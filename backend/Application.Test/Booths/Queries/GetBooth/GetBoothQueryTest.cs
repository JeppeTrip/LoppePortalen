using Application.Booths.Queries.GetBooth;
using Application.Common.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Queries.GetBooth
{
    public class GetBoothQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetBooth()
        {
            var request = new GetBoothRequest() { Id = "Booth1700" };
            var query = new GetBoothQuery() { Dto = request };
            var handler = new GetBoothQuery.GetBoothQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booth.Id.Should().Be("Booth1700");
            result.Booth.Name.Should().Be("Booth 1700");
            result.Booth.Description.Should().Be("Booth 1700 Description");
            result.Booth.Categories.Count().Should().Be(3);
            result.Booth.Categories.Should().Contain("Category 1700");
            result.Booth.Categories.Should().Contain("Category 1701");
            result.Booth.Categories.Should().Contain("Category 1702");

            result.Booth.Stall.Id.Should().Be(1700);
            result.Booth.Stall.StallType.Name.Should().Be("Stalltype 1700");
            result.Booth.Stall.StallType.Description.Should().Be("Stalltype 1700 description");

            result.Booth.Stall.Market.MarketId.Should().Be(1700);

            result.Booth.Stall.Market.MarketId.Should().Be(1700);
            result.Booth.Stall.Market.MarketName.Should().Be("Market 1700");
            result.Booth.Stall.Market.Description.Should().Be("Market 1700 Description");
            result.Booth.Stall.Market.StartDate.Should().Be(new DateTime(1990, 1, 1));
            result.Booth.Stall.Market.EndDate.Should().Be(new DateTime(1990, 1, 2));
            result.Booth.Stall.Market.IsCancelled.Should().BeFalse();
            result.Booth.Stall.Market.AvailableStallCount.Should().Be(0);
            result.Booth.Stall.Market.OccupiedStallCount.Should().Be(1);
            result.Booth.Stall.Market.TotalStallCount.Should().Be(1);
            result.Booth.Stall.Market.Categories.Count().Should().Be(3);
            result.Booth.Stall.Market.Categories.Should().Contain("Category 1700");
            result.Booth.Stall.Market.Categories.Should().Contain("Category 1701");
            result.Booth.Stall.Market.Categories.Should().Contain("Category 1702");
            result.Booth.Stall.Market.Address.Should().BeNull();
            result.Booth.Stall.Market.PostalCode.Should().BeNull();
            result.Booth.Stall.Market.City.Should().BeNull();
        }

        [Fact]
        public async Task Handle_GetBooth_MarketAddress()
        {
            var request = new GetBoothRequest() { Id = "Booth1701" };
            var query = new GetBoothQuery() { Dto = request };
            var handler = new GetBoothQuery.GetBoothQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booth.Id.Should().Be("Booth1701");
            result.Booth.Name.Should().Be("Booth 1701");
            result.Booth.Description.Should().Be("Booth 1701 Description");
            result.Booth.Categories.Should().BeEmpty();
            result.Booth.Stall.Id.Should().Be(1700);
            result.Booth.Stall.StallType.Name.Should().Be("Stalltype 1701");
            result.Booth.Stall.StallType.Description.Should().Be("Stalltype 1701 description");

            result.Booth.Stall.Market.MarketId.Should().Be(1700);

            result.Booth.Stall.Market.MarketId.Should().Be(1701);
            result.Booth.Stall.Market.MarketName.Should().Be("Market 1701");
            result.Booth.Stall.Market.Description.Should().Be("Market 1701 Description");
            result.Booth.Stall.Market.StartDate.Should().Be(new DateTime(1990, 1, 1));
            result.Booth.Stall.Market.EndDate.Should().Be(new DateTime(1990, 1, 2));
            result.Booth.Stall.Market.IsCancelled.Should().BeFalse();
            result.Booth.Stall.Market.AvailableStallCount.Should().Be(0);
            result.Booth.Stall.Market.OccupiedStallCount.Should().Be(1);
            result.Booth.Stall.Market.TotalStallCount.Should().Be(1);
            result.Booth.Stall.Market.Categories.Should().BeEmpty();
            result.Booth.Stall.Market.Address.Should().Be("address 1701");
            result.Booth.Stall.Market.PostalCode.Should().Be("1701");
            result.Booth.Stall.Market.Description.Should().Be("city 1701");
        }

        [Fact]
        public async Task Handle_NoSuchBooth()
        {
            var request = new GetBoothRequest() { Id = "DoesNotExist" };
            var query = new GetBoothQuery() { Dto = request };
            var handler = new GetBoothQuery.GetBoothQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

    }
}

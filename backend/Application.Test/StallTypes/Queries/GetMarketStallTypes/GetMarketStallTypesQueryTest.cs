using Application.Common.Exceptions;
using Application.StallTypes.Queries.GetMarketStallTypes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_NoSuchMarket()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = -1 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var handler = new GetMarketStallTypesQuery.GetMarketStallTypesQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketWithNoStallTypes()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = 8000 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var handler = new GetMarketStallTypesQuery.GetMarketStallTypesQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.StallTypes.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_MarketWithOneStallType()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = 8001 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var handler = new GetMarketStallTypesQuery.GetMarketStallTypesQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.StallTypes.Count().Should().Be(1);
            result.StallTypes.First().Id.Should().Be(8000);
            result.StallTypes.First().Name.Should().Be("Get markets Stalltype 1");
            result.StallTypes.First().Description.Should().Be("Get markets Stalltype description");
        }

        [Fact]
        public async Task Handle_MarketWithMultipleStallTypes()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = 8002 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var handler = new GetMarketStallTypesQuery.GetMarketStallTypesQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.StallTypes.Count().Should().Be(3);

            var type1 = result.StallTypes.Find(x => x.Id.Equals(8001));
            type1.Should().NotBeNull();
            type1.Name.Should().Be("Get markets Stalltype 2");
            type1.Description.Should().Be("Get markets Stalltype description");
            type1.NumberOfStalls.Should().Be(0);

            var type2 = result.StallTypes.Find(x => x.Id.Equals(8002));
            type2.Should().NotBeNull();
            type2.Name.Should().Be("Get markets Stalltype 3");
            type2.Description.Should().Be("Get markets Stalltype description");
            type2.NumberOfStalls.Should().Be(1);

            var type3 = result.StallTypes.Find(x => x.Id.Equals(8003));
            type3.Should().NotBeNull();
            type3.Name.Should().Be("Get markets Stalltype 4");
            type3.Description.Should().Be("Get markets Stalltype description");
            type3.NumberOfStalls.Should().Be(3);
        }
    }
}

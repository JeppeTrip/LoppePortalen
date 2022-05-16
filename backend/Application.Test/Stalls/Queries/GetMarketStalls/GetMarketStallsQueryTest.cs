using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Stalls.Queries.GetMarketStalls;
using System.Threading;
using FluentAssertions;

namespace Application.Test.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_MarketWithNoStalls()
        {
            var request = new GetMarketStallsRequest(){ MarketId = 1300};
            var query = new GetMarketStallsQuery() { Dto = request };
            var handler = new GetMarketStallsQuery.GetMarketStallsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Stalls.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_MarketWithOneStall()
        {
            var request = new GetMarketStallsRequest() { MarketId = 1301 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var handler = new GetMarketStallsQuery.GetMarketStallsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Stalls.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_MarketWithMultipleStalls()
        {
            var request = new GetMarketStallsRequest() { MarketId = 1302 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var handler = new GetMarketStallsQuery.GetMarketStallsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Stalls.Count().Should().Be(3);
        }

        [Fact]
        public async Task Handle_MarketInstanceDoesntExist()
        {
            var request = new GetMarketStallsRequest() { MarketId = 1302 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var handler = new GetMarketStallsQuery.GetMarketStallsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Stalls.Count().Should().Be(3);
        }
    }
}

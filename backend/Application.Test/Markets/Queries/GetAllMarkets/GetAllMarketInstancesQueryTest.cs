using Application.Markets.Queries.GetAllMarkets;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetAllMarkets
{
    public class GetAllMarketInstancesQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetAllMarkets() {
            var command = new GetAllMarketInstancesQuery();
            var handler = new GetAllMarketInstancesQuery.GetAllMarketInstancesQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(Context.MarketInstances.ToList().Count);
        }
    }
}

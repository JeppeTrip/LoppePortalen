using Application.Common.Exceptions;
using Application.Markets.Queries.GetMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries
{
    public class GetMarketInstanceQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetMarketInstace()
        {
            var request = new GetMarketInstanceQueryRequest()
            {
                MarketId = 1
            };

            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketId.Should().Be(request.MarketId);
            result.MarketName.Should().Be("Test Market 1");
            result.OrganiserId.Should().BePositive();
            result.Description.Should().Be("Test Description 1");
            result.StartDate.Should().BeOnOrBefore(result.EndDate);
        }

        [Fact]
        public async Task Handle_NonExistentId()
        {
            var request = new GetMarketInstanceQueryRequest()
            {
                MarketId = int.MaxValue
            };

            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_NegativeId()
        {
            var request = new GetMarketInstanceQueryRequest()
            {
                MarketId = -1
            };

            var query = new GetMarketInstanceQuery() { Dto = request };
            var handler = new GetMarketInstanceQuery.GetMarketInstanceQueryHandler(Context);

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new GetMarketInstanceQueryValidator().ValidateAsync(query, CancellationToken.None);
            });
        }
    }
}

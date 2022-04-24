﻿using Application.Common.Exceptions;
using Application.Markets.Queries.GetMarketInstance;
using FluentAssertions;
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
            result.Market.MarketId.Should().Be(request.MarketId);
            result.Market.MarketName.Should().Be("Test Market 1");
            result.Market.Organiser.Id.Should().BePositive();
            result.Market.Description.Should().Be("Test Description 1");
            result.Market.StartDate.Should().BeOnOrBefore(result.Market.EndDate);
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

            var valRes = await new GetMarketInstanceQueryValidator().ValidateAsync(query, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);

        }
    }
}

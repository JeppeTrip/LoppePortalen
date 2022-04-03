using Application.Markets.Queries.GetFilteredMarkets;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetFilteredMarkets
{
    public class GetFilteredMarketsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_NoFiltersSet()
        {
            var request = new GetFilteredMarketsQueryRequest();
            var command = new GetFilteredMarketsQuery() { Dto = request};
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(Context.MarketInstances.ToList().Count);
        }


        [Fact]
        public async Task Handle_FetchOnlyActiveMarkets()
        {
            var request = new GetFilteredMarketsQueryRequest() { HideCancelled = true };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(Context.MarketInstances.Where(x => !x.IsCancelled).Count());
        }

        [Fact]
        public async Task Handle_FetchStartDate()
        {
            var request = new GetFilteredMarketsQueryRequest() {
                StartDate = new DateTimeOffset(new DateTime(1990, 1, 1))
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(Context.MarketInstances.Where(x => x.StartDate >= request.StartDate).Count());
        }

        [Fact]
        public async Task Handle_FetchStartDateNoMarketsLaterThan()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(9999, 1, 1))
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_AllEndDatesEarlier()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                EndDate = new DateTimeOffset(new DateTime(9999, 1, 1))
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(Context.MarketInstances.Count());
        }

        [Fact]
        public async Task Handle_DateRange()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(1980, 1, 1)),
                EndDate = new DateTimeOffset(new DateTime(1980, 6, 1))
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Count.Should().Be(5);
        }
    }
}

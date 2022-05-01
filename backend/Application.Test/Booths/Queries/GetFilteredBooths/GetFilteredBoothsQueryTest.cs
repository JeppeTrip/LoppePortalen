using Application.Booths.Queries.GetFilteredBooths;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Queries.GetFilteredBooths
{
    public class GetFilteredBoothsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_NoParameters()
        {
            var request = new GetFilteredBoothRequest();
            var query = new GetFilteredBoothsQuery() { Dto = request};
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.Count().Should().Be(Context.Bookings.Count());
        }

        [Fact]
        public async Task Handle_FilterOnStartDate()
        {
            var request = new GetFilteredBoothRequest() { StartDate = new DateTimeOffset(new DateTime(1990, 1, 1))};
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.ForEach(x =>
            {
                Assert.True( DateTimeOffset.Compare(x.Stall.Market.StartDate, request.StartDate.Value) >= 0 || DateTimeOffset.Compare(x.Stall.Market.EndDate, request.StartDate.Value) >= 0);
            });
        }

        [Fact]
        public async Task Handle_FilterOnEndDate()
        {
            var request = new GetFilteredBoothRequest() { EndDate = new DateTimeOffset(new DateTime(1990, 1, 1)) };
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.ForEach(x =>
            {
                Assert.True(x.Stall.Market.StartDate <= request.EndDate.Value || x.Stall.Market.EndDate <= request.StartDate);
            });
        }

        [Fact]
        public async Task Handle_PeriodWithNoBooths()
        {
            var request = new GetFilteredBoothRequest() { 
                StartDate = new DateTimeOffset(new DateTime(1500, 1, 1)), 
                EndDate = new DateTimeOffset(new DateTime(1500, 2, 1)) };
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_FilterOnNoCategory()
        {
            var request = new GetFilteredBoothRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(1500, 3, 1)),
                EndDate = new DateTimeOffset(new DateTime(1500, 4, 1))
            };
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.Count().Should().Be(3);
        }

        [Fact]
        public async Task Handle_FilterOnSingleCategory()
        {
            var request = new GetFilteredBoothRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(1500, 3, 1)),
                EndDate = new DateTimeOffset(new DateTime(1500, 4, 1)),
                Categories = new List<string>() { "Category 1500"}
            };
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_FilterOnMultipleCategories()
        {
            var request = new GetFilteredBoothRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(1500, 3, 1)),
                EndDate = new DateTimeOffset(new DateTime(1500, 4, 1)),
                Categories = new List<string>() { "Category 1501", "Category 1500" }
            };
            var query = new GetFilteredBoothsQuery() { Dto = request };
            var handler = new GetFilteredBoothsQuery.GetFilteredBoothsQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Should().NotBeEmpty();
            result.Booths.Count().Should().Be(2);
        }
    }
}

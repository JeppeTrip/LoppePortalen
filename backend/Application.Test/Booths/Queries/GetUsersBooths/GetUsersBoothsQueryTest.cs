using Application.Booths.Queries.GetUsersBooths;
using Application.Common.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Queries.GetUsersBooths
{
    public class GetUsersBoothsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_UserIsNull()
        {
            var query = new GetUsersBoothsQuery();
            var handler = new GetUsersBoothsQuery.GetUsersBoothsQueryHandler(Context, new CurrentUserService(null));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var query = new GetUsersBoothsQuery();
            var handler = new GetUsersBoothsQuery.GetUsersBoothsQueryHandler(Context, new CurrentUserService("DoesNotExist"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserWithNoBooths()
        {
            var query = new GetUsersBoothsQuery();
            var handler = new GetUsersBoothsQuery.GetUsersBoothsQueryHandler(Context, new CurrentUserService("User1800"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserWithOneBooth()
        {
            var query = new GetUsersBoothsQuery();
            var handler = new GetUsersBoothsQuery.GetUsersBoothsQueryHandler(Context, new CurrentUserService("User1801"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Count().Should().Be(1);
            result.Booths.First().Id.Should().Be("Booth1800");
            result.Booths.First().Name.Should().Be("Booth 1800");
            result.Booths.First().Description.Should().Be("Booth 1800 Description");
            result.Booths.First().Categories.Count().Should().Be(3);
            result.Booths.First().Categories.Should().Contain("Category 1800");
            result.Booths.First().Categories.Should().Contain("Category 1801");
            result.Booths.First().Categories.Should().Contain("Category 1802");

            result.Booths.First().Stall.Id.Should().Be(1800);
            result.Booths.First().Stall.StallType.Name.Should().Be("Stalltype 1800");
            result.Booths.First().Stall.StallType.Description.Should().Be("Stalltype 1800 description");

            result.Booths.First().Stall.Market.MarketId.Should().Be(1800);

            result.Booths.First().Stall.Market.MarketId.Should().Be(1800);
            result.Booths.First().Stall.Market.MarketName.Should().Be("Market 1800");
            result.Booths.First().Stall.Market.Description.Should().Be("Market 1800 Description");
            result.Booths.First().Stall.Market.StartDate.Should().Be(new DateTime(1990, 1, 1));
            result.Booths.First().Stall.Market.EndDate.Should().Be(new DateTime(1990, 1, 2));
            result.Booths.First().Stall.Market.IsCancelled.Should().BeFalse();
            result.Booths.First().Stall.Market.AvailableStallCount.Should().BePositive();
            result.Booths.First().Stall.Market.OccupiedStallCount.Should().BePositive();
            result.Booths.First().Stall.Market.TotalStallCount.Should().BePositive();
            result.Booths.First().Stall.Market.Categories.Count().Should().Be(3);
            result.Booths.First().Stall.Market.Categories.Should().Contain("Category 1800");
            result.Booths.First().Stall.Market.Categories.Should().Contain("Category 1801");
            result.Booths.First().Stall.Market.Categories.Should().Contain("Category 1802");

        }

        [Fact]
        public async Task Handle_UserWithMultipleBooths()
        {
            var query = new GetUsersBoothsQuery();
            var handler = new GetUsersBoothsQuery.GetUsersBoothsQueryHandler(Context, new CurrentUserService("User1802"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Booths.Count().Should().Be(3);
        }
    }
}

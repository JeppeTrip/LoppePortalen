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

            result.Markets.Count.Should().Be(Context.MarketInstances.ToList().Count);
        }


        [Fact]
        public async Task Handle_FetchOnlyActiveMarkets()
        {
            var request = new GetFilteredMarketsQueryRequest() { HideCancelled = true };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Markets.Count.Should().Be(Context.MarketInstances.Where(x => !x.IsCancelled).Count());
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

            result.Markets.Count.Should().Be(Context.MarketInstances.Where(x => x.StartDate >= request.StartDate).Count());
        }

        [Fact]
        public async Task Handle_FetchStartDateNoMarketsLaterThan()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                StartDate = DateTimeOffset.MaxValue
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Markets.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_AllEndDatesEarlier()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                EndDate = DateTimeOffset.MaxValue
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Markets.Count.Should().Be(Context.MarketInstances.Count());
        }

        [Fact]
        public async Task Handle_DateRange()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                StartDate = new DateTimeOffset(new DateTime(2800, 1, 1)),
                EndDate = new DateTimeOffset(new DateTime(2800, 1, 10))
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Markets.Count.Should().Be(1);

            result.Markets.First(x => x.MarketId == 2800).MarketId.Should().Be(2800);
            result.Markets.First(x => x.MarketId == 2800).MarketName.Should().Be("Market 2800 name");
            result.Markets.First(x => x.MarketId == 2800).Description.Should().Be("Market 2800 description");
            result.Markets.First(x => x.MarketId == 2800).StartDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 1, 1)));
            result.Markets.First(x => x.MarketId == 2800).EndDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 1, 16)));
            result.Markets.First(x => x.MarketId == 2800).IsCancelled.Should().BeFalse();
            result.Markets.First(x => x.MarketId == 2800).AvailableStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2800).OccupiedStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2800).TotalStallCount.Should().Be(2);
            result.Markets.First(x => x.MarketId == 2800).Categories.Count().Should().Be(2);
            result.Markets.First(x => x.MarketId == 2800).Categories.Should().Contain("category 2800");
            result.Markets.First(x => x.MarketId == 2800).Categories.Should().Contain("category 2801");
            result.Markets.First(x => x.MarketId == 2800).Address.Should().Be("address 2800");
            result.Markets.First(x => x.MarketId == 2800).PostalCode.Should().Be("postal 2800");
            result.Markets.First(x => x.MarketId == 2800).City.Should().Be("city 2800");

            result.Markets.First(x => x.MarketId == 2800).Organiser.Id.Should().Be(2800);
            result.Markets.First(x => x.MarketId == 2800).Organiser.Name.Should().Be("Organiser 2800");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Description.Should().Be("Organiser 2800 Description");
            result.Markets.First(x => x.MarketId == 2800).Organiser.UserId.Should().Be("User2800");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Street.Should().Be("street");
            result.Markets.First(x => x.MarketId == 2800).Organiser.StreetNumber.Should().Be("number");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Appartment.Should().Be("apt");
            result.Markets.First(x => x.MarketId == 2800).Organiser.City.Should().Be("city");
            result.Markets.First(x => x.MarketId == 2800).Organiser.PostalCode.Should().Be("postal");
        }

        [Fact]
        public async Task Handle_ItemCategories()
        {
            var request = new GetFilteredMarketsQueryRequest()
            {
                Categories = new List<string>() { "category 2800", "category 2802" }
            };
            var command = new GetFilteredMarketsQuery() { Dto = request };
            var handler = new GetFilteredMarketsQuery.GetFilteredMarketsQueryHandler(Context);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Markets.Count.Should().Be(2);

            result.Markets.First(x => x.MarketId == 2800).MarketId.Should().Be(2800);
            result.Markets.First(x => x.MarketId == 2800).MarketName.Should().Be("Market 2800 name");
            result.Markets.First(x => x.MarketId == 2800).Description.Should().Be("Market 2800 description");
            result.Markets.First(x => x.MarketId == 2800).StartDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 1, 1)));
            result.Markets.First(x => x.MarketId == 2800).EndDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 1, 16)));
            result.Markets.First(x => x.MarketId == 2800).IsCancelled.Should().BeFalse();
            result.Markets.First(x => x.MarketId == 2800).AvailableStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2800).OccupiedStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2800).TotalStallCount.Should().Be(2);
            result.Markets.First(x => x.MarketId == 2800).Categories.Count().Should().Be(2);
            result.Markets.First(x => x.MarketId == 2800).Categories.Should().Contain("category 2800");
            result.Markets.First(x => x.MarketId == 2800).Categories.Should().Contain("category 2801");


            result.Markets.First(x => x.MarketId == 2800).Organiser.Id.Should().Be(2800);
            result.Markets.First(x => x.MarketId == 2800).Organiser.Name.Should().Be("Organiser 2800");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Description.Should().Be("Organiser 2800 Description");
            result.Markets.First(x => x.MarketId == 2800).Organiser.UserId.Should().Be("User2800");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Street.Should().Be("street");
            result.Markets.First(x => x.MarketId == 2800).Organiser.StreetNumber.Should().Be("number");
            result.Markets.First(x => x.MarketId == 2800).Organiser.Appartment.Should().Be("apt");
            result.Markets.First(x => x.MarketId == 2800).Organiser.City.Should().Be("city");
            result.Markets.First(x => x.MarketId == 2800).Organiser.PostalCode.Should().Be("postal");

            result.Markets.First(x => x.MarketId == 2801).MarketId.Should().Be(2801);
            result.Markets.First(x => x.MarketId == 2801).MarketName.Should().Be("Market 2801 name");
            result.Markets.First(x => x.MarketId == 2801).Description.Should().Be("Market 2801 description");
            result.Markets.First(x => x.MarketId == 2801).StartDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 2, 1)));
            result.Markets.First(x => x.MarketId == 2801).EndDate.Should().BeSameDateAs(new DateTimeOffset(new DateTime(2800, 2, 16)));
            result.Markets.First(x => x.MarketId == 2801).IsCancelled.Should().BeFalse();
            result.Markets.First(x => x.MarketId == 2801).AvailableStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2801).OccupiedStallCount.Should().Be(1);
            result.Markets.First(x => x.MarketId == 2801).TotalStallCount.Should().Be(2);
            result.Markets.First(x => x.MarketId == 2801).Categories.Count().Should().Be(2);
            result.Markets.First(x => x.MarketId == 2801).Categories.Should().Contain("category 2801");
            result.Markets.First(x => x.MarketId == 2801).Categories.Should().Contain("category 2802");
            result.Markets.First(x => x.MarketId == 2801).Address.Should().BeNull();
            result.Markets.First(x => x.MarketId == 2801).PostalCode.Should().BeNull();
            result.Markets.First(x => x.MarketId == 2801).City.Should().BeNull();


            result.Markets.First(x => x.MarketId == 2801).Organiser.Id.Should().Be(2801);
            result.Markets.First(x => x.MarketId == 2801).Organiser.Name.Should().Be("Organiser 2801");
            result.Markets.First(x => x.MarketId == 2801).Organiser.Description.Should().Be("Organiser 2801 Description");
            result.Markets.First(x => x.MarketId == 2801).Organiser.UserId.Should().Be("User2800");
            result.Markets.First(x => x.MarketId == 2801).Organiser.Street.Should().Be("street");
            result.Markets.First(x => x.MarketId == 2801).Organiser.StreetNumber.Should().Be("number");
            result.Markets.First(x => x.MarketId == 2801).Organiser.Appartment.Should().Be("apt");
            result.Markets.First(x => x.MarketId == 2801).Organiser.City.Should().Be("city");
            result.Markets.First(x => x.MarketId == 2801).Organiser.PostalCode.Should().Be("postal");
        }
    }
}

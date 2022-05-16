using Application.Common.Exceptions;
using Application.Stalls.Queries.GetStall;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Queries.GetStall
{
    public class GetStallQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetStallWithBooth()
        {
            var request = new GetStallRequest() { StallId = 1400 };
            var command = new GetStallQuery() { Dto = request };
            var handler = new GetStallQuery.GetStallQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Stall.Should().NotBeNull();
            result.Stall.Id.Should().Be(1400);
            
            result.Stall.StallType.Id.Should().Be(1400);
            result.Stall.StallType.Name.Should().Be("Stalltype 1400");
            result.Stall.StallType.Description.Should().Be("Stalltype 1400 description");

            result.Stall.Market.MarketId.Should().Be(1400);
            result.Stall.Market.MarketName.Should().Be("Market 1400");
            result.Stall.Market.Description.Should().Be("Market 1400 Description");
            result.Stall.Market.StartDate.Should().Be(new DateTime(1990, 1, 1));
            result.Stall.Market.EndDate.Should().Be(new DateTime(1990, 1, 2));
            result.Stall.Market.IsCancelled.Should().BeFalse();
            result.Stall.Market.AvailableStallCount.Should().Be(2);
            result.Stall.Market.OccupiedStallCount.Should().Be(1);
            result.Stall.Market.TotalStallCount.Should().Be(3);
            result.Stall.Market.Categories.Count().Should().Be(3);
            result.Stall.Market.Categories.Should().Contain("Category 1400");
            result.Stall.Market.Categories.Should().Contain("Category 1401");
            result.Stall.Market.Categories.Should().Contain("Category 1402");

            result.Stall.Market.Organiser.Id.Should().Be(1400);
            result.Stall.Market.Organiser.Name.Should().Be("Organiser 1400");
            result.Stall.Market.Organiser.Description.Should().Be("Organiser 1400 Description");
            result.Stall.Market.Organiser.UserId.Should().Be("User1400");
            result.Stall.Market.Organiser.Street.Should().Be("street");
            result.Stall.Market.Organiser.StreetNumber.Should().Be("number");
            result.Stall.Market.Organiser.Appartment.Should().Be("apt");
            result.Stall.Market.Organiser.City.Should().Be("city");
            result.Stall.Market.Organiser.PostalCode.Should().Be("postal");

            result.Stall.Booths.First().Id.Should().Be("Booth1400");
            result.Stall.Booths.First().Name.Should().Be("Booth 1400");
            result.Stall.Booths.First().Description.Should().Be("Booth 1400 Description");
            result.Stall.Booths.First().Categories.Should().Contain("Category 1400");
            result.Stall.Booths.First().Categories.Should().Contain("Category 1401");
            result.Stall.Booths.First().Categories.Should().Contain("Category 1402");

            result.Stall.Booths.First().MerchantBaseVM.Id.Should().Be(1400);
            result.Stall.Booths.First().MerchantBaseVM.UserId.Should().Be("User1400");
            result.Stall.Booths.First().MerchantBaseVM.Name.Should().Be("Merchant 1400");
            result.Stall.Booths.First().MerchantBaseVM.Description.Should().Be("Merchant 1400 Description");
        }

        [Fact]
        public async Task Handle_GetStallWithoutBooth()
        {
            var request = new GetStallRequest() { StallId = 1401 };
            var command = new GetStallQuery() { Dto = request };
            var handler = new GetStallQuery.GetStallQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Stall.Should().NotBeNull();
            result.Stall.Id.Should().Be(1401);

            result.Stall.StallType.Id.Should().Be(1400);
            result.Stall.StallType.Name.Should().Be("Stalltype 1400");
            result.Stall.StallType.Description.Should().Be("Stalltype 1400 description");

            result.Stall.Market.MarketId.Should().Be(1400);
            result.Stall.Market.MarketName.Should().Be("Market 1400");
            result.Stall.Market.Description.Should().Be("Market 1400 Description");
            result.Stall.Market.StartDate.Should().Be(new DateTime(1990, 1, 1));
            result.Stall.Market.EndDate.Should().Be(new DateTime(1990, 1, 2));
            result.Stall.Market.IsCancelled.Should().BeFalse();
            result.Stall.Market.AvailableStallCount.Should().Be(2);
            result.Stall.Market.OccupiedStallCount.Should().Be(1);
            result.Stall.Market.TotalStallCount.Should().Be(3);
            result.Stall.Market.Categories.Count().Should().Be(3);
            result.Stall.Market.Categories.Should().Contain("Category 1400");
            result.Stall.Market.Categories.Should().Contain("Category 1401");
            result.Stall.Market.Categories.Should().Contain("Category 1402");

            result.Stall.Market.Organiser.Id.Should().Be(1400);
            result.Stall.Market.Organiser.Name.Should().Be("Organiser 1400");
            result.Stall.Market.Organiser.Description.Should().Be("Organiser 1400 Description");
            result.Stall.Market.Organiser.UserId.Should().Be("User1400");
            result.Stall.Market.Organiser.Street.Should().Be("street");
            result.Stall.Market.Organiser.StreetNumber.Should().Be("number");
            result.Stall.Market.Organiser.Appartment.Should().Be("apt");
            result.Stall.Market.Organiser.City.Should().Be("city");
            result.Stall.Market.Organiser.PostalCode.Should().Be("postal");

            result.Stall.Booths.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_StallDoesNotExist()
        {
            var request = new GetStallRequest() { StallId = -1 };
            var command = new GetStallQuery() { Dto = request };
            var handler = new GetStallQuery.GetStallQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

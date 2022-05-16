using Application.Common.Exceptions;
using Application.Merchants.Queries.GetMerchant;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Queries.GetMerchant
{
    public class GetMerchantQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetMerchantNoBooths()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3000
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().Be("User3000");
            result.Merchant.Name.Should().Be("Merchant 3000");
            result.Merchant.Description.Should().Be("Merchant 3000 description");
            result.Merchant.Booths.Should().BeEmpty();
            result.Merchant.ContactInfo.Count().Should().Be(1);
            result.Merchant.ContactInfo.First().Value.Should().Be("value");
            result.Merchant.ContactInfo.First().Type.Should().Be(0);
            result.Merchant.ImageData.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Handle_GetMerchantOneBooth()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3001
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().Be("User3000");
            result.Merchant.Name.Should().Be("Merchant 3001");
            result.Merchant.Description.Should().Be("Merchant 3001 description");
            result.Merchant.ImageData.Should().BeNull();
            result.Merchant.Booths.Count().Should().Be(1);

            result.Merchant.Booths.First().Id.Should().Be("Booth3000");
            result.Merchant.Booths.First().Name.Should().Be("Booth 3000");
            result.Merchant.Booths.First().Description.Should().Be("Booth 3000 Description");
            result.Merchant.Booths.First().Stall.Market.Categories.Count().Should().Be(3);
            result.Merchant.Booths.First().Categories.Should().Contain("Category 3000");
            result.Merchant.Booths.First().Categories.Should().Contain("Category 3001");
            result.Merchant.Booths.First().Categories.Should().Contain("Category 3002");

            result.Merchant.Booths.First().Stall.Id.Should().Be(3000);

            result.Merchant.Booths.First().Stall.StallType.Id.Should().Be(3000);
            result.Merchant.Booths.First().Stall.StallType.Name.Should().Be("Stalltype 3000");
            result.Merchant.Booths.First().Stall.StallType.Description.Should().Be("Stalltype 3000 description");

            result.Merchant.Booths.First().Stall.Market.MarketId.Should().Be(3000);
            result.Merchant.Booths.First().Stall.Market.MarketName.Should().Be("Market 3000");
            result.Merchant.Booths.First().Stall.Market.Description.Should().Be("Market 3000 Description");
            result.Merchant.Booths.First().Stall.Market.StartDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(-1));
            result.Merchant.Booths.First().Stall.Market.EndDate.Should().BeSameDateAs(DateTimeOffset.Now.AddDays(1));
            result.Merchant.Booths.First().Stall.Market.IsCancelled.Should().BeFalse();
            result.Merchant.Booths.First().Stall.Market.AvailableStallCount.Should().Be(0);
            result.Merchant.Booths.First().Stall.Market.OccupiedStallCount.Should().Be(1);
            result.Merchant.Booths.First().Stall.Market.TotalStallCount.Should().Be(1);
            result.Merchant.Booths.First().Stall.Market.Categories.Count().Should().Be(3);
            result.Merchant.Booths.First().Stall.Market.Categories.Should().Contain("Category 3000");
            result.Merchant.Booths.First().Stall.Market.Categories.Should().Contain("Category 3001");
            result.Merchant.Booths.First().Stall.Market.Categories.Should().Contain("Category 3002");

            result.Merchant.ContactInfo.Count().Should().Be(0);

        }

        [Fact]
        public async Task Handle_GetMerchantMultipleBooths()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3002
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().Be("User3000");
            result.Merchant.Name.Should().Be("Merchant 3002");
            result.Merchant.Description.Should().Be("Merchant 3002 description");
            result.Merchant.Booths.Count().Should().Be(3);

            result.Merchant.ContactInfo.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_NonExistantMerchant()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = -1
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketIsCancelled()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3003
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().Be("User3000");
            result.Merchant.Name.Should().Be("Merchant 3003");
            result.Merchant.Description.Should().Be("Merchant 3003 description");
            result.Merchant.Booths.Count().Should().Be(0);

            result.Merchant.ContactInfo.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_MarketIsOver()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3004
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().Be("User3000");
            result.Merchant.Name.Should().Be("Merchant 3004");
            result.Merchant.Description.Should().Be("Merchant 3004 description");
            result.Merchant.Booths.Count().Should().Be(0);

            result.Merchant.ContactInfo.Count().Should().Be(0);
        }
    }
}

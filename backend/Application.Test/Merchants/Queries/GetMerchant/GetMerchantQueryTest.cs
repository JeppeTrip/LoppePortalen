using Application.Common.Exceptions;
using Application.Merchants.Queries.GetMerchant;
using FluentAssertions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Queries.GetMerchant
{
    public class GetMerchantQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetMerchant()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 3000
            };
            var command = new GetMerchantQuery() { Dto = request };
            var handler = new GetMerchantQuery.GetMerchantQueryHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Merchant.UserId.Should().NotBeEmpty();
            result.Merchant.UserId.Should().Be("GetMerchantUser");
            result.Merchant.Name.Should().NotBeEmpty();
            result.Merchant.Name.Should().Be("Merchant 3000");
            result.Merchant.Description.Should().NotBeNull();
            result.Merchant.Description.Should().Be("Merchant 3000 description");
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
    }
}

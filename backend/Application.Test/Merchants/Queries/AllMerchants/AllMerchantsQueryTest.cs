using Application.Merchants.Queries.AllMerchants;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Queries.AllMerchants
{
    public class AllMerchantsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetAllMerchants()
        {
            var request = new AllMerchantsQuery();
            var handler = new AllMerchantsQuery.AllMerchantsQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);
            result.MerchantList.Should().NotBeEmpty();
            result.MerchantList.Count.Should().Be(Context.Merchants.Count());
        }
    }
}

using Application.Merchants.Queries.GetUsersMerchants;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Queries.GetUsersMerchants
{
    public class GetUsersMerchantsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_NoMerchants()
        {
            var query = new GetUsersMerchantsQuery();
            var handler = new GetUsersMerchantsQuery.GetUsersMerchantsQueryHandler(Context, new CurrentUserService("UsersMerchantsNoMerchant"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Merchants.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_OneMerchant()
        {
            var query = new GetUsersMerchantsQuery();
            var handler = new GetUsersMerchantsQuery.GetUsersMerchantsQueryHandler(Context, new CurrentUserService("UsersMerchantsOneMerchant"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Merchants.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_MultipleMerchants()
        {
            var query = new GetUsersMerchantsQuery();
            var handler = new GetUsersMerchantsQuery.GetUsersMerchantsQueryHandler(Context, new CurrentUserService("UsersMerchantsMultipleMerchant"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Merchants.Count.Should().Be(3);
        }

        [Fact]
        public async Task Handle_NoSuchUser()
        {
            var query = new GetUsersMerchantsQuery();
            var handler = new GetUsersMerchantsQuery.GetUsersMerchantsQueryHandler(Context, new CurrentUserService("UserDoesNotExists"));
            var result = await handler.Handle(query, CancellationToken.None);

            result.Merchants.Count.Should().Be(0);
        }
    }
}

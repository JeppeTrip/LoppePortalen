using Application.Markets.Queries.GetUsersMarkets;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetMarkets()
        {
            var request = new GetUsersMarketsQuery();
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService(Guid.Empty.ToString()));
            var result = await handler.Handle(request, CancellationToken.None);

            result.Markets.Count.Should().Be(Context.MarketInstances.Where(x => x.MarketTemplate.Organiser.UserId.Equals(Guid.Empty.ToString())).Count());
        }

        [Fact]
        public async Task Handle_UserNotCurrentUser()
        {
            var request = new GetUsersMarketsQuery();
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}

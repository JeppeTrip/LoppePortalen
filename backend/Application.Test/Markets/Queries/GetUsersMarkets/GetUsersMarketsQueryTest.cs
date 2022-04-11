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
            var dto = new GetUsersMarketsRequest() { UserId = Guid.Empty.ToString() };
            var request = new GetUsersMarketsQuery() { Dto = dto };
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService(dto.UserId));
            var result = await handler.Handle(request, CancellationToken.None);

            result.Succeeded.Should().BeTrue();

            result.Markets.Count.Should().Be(Context.MarketInstances.Where(x => x.MarketTemplate.Organiser.UserId.Equals(dto.UserId)).Count());
        }

        [Fact]
        public async Task Handle_UserNotCurrentUser()
        {
            var dto = new GetUsersMarketsRequest() { UserId = "-1" };
            var request = new GetUsersMarketsQuery() { Dto = dto };
            var handler = new GetUsersMarketsQuery.GetUsersMarketsQueryHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}

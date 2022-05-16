using Application.Common.Exceptions;
using Application.Organisers.Queries.GetUsersOrganisers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries.GetUsersOrganisers
{
    public class GetUsersOrganisersQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetOrganisers()
        {
            var request = new GetUsersOrganisersQuery();
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context, new CurrentUserService(Guid.Empty.ToString()));
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Organisers.Count.Should().Be(Context.Organisers.Where(x => x.UserId.Equals(Guid.Empty.ToString())).Count());
        }

        [Fact]
        public async Task Handle_NonExistentUser()
        {
            var request = new GetUsersOrganisersQuery();
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context, new CurrentUserService("-1"));
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Organisers.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserIdNull()
        {
            var request = new GetUsersOrganisersQuery();
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context, new CurrentUserService(null));
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Organisers.Count.Should().Be(0);
        }
    }
}

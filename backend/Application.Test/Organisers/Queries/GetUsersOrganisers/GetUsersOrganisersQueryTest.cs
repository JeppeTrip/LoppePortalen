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
            var dto = new GetUsersOrganisersRequest() { UserId = Guid.Empty.ToString() };
            var request = new GetUsersOrganisersQuery() { Dto = dto };
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(Context.Organisers.Where(x => x.UserId.Equals(request.Dto.UserId)).Count());
        }

        [Fact]
        public async Task Handle_NonExistentUser()
        {
            var dto = new GetUsersOrganisersRequest() { UserId = "-1" };
            var request = new GetUsersOrganisersQuery() { Dto = dto };
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task Handle_UserIdNull()
        {
            var dto = new GetUsersOrganisersRequest() { UserId = null};
            var request = new GetUsersOrganisersQuery() { Dto = dto };
            var handler = new GetUsersOrganisersQuery.GetUsersOrganisersQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Count.Should().Be(0);
        }
    }
}

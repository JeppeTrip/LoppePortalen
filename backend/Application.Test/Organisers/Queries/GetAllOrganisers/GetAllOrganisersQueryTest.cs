using Application.Organisers.Queries.GetAllOrganisers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries.GetAllOrganisers
{
    public class GetAllOrganisersQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetAllOrganisers()
        {
            var request = new GetAllOrganisersQuery();
            var handler = new GetAllOrganisersQuery.GetAllOrganisersQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);
            result.Should().NotBeEmpty();
            result.Count.Should().Be(Context.Organisers.Count());
        }
    }
}

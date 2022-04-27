using Application.Common.Exceptions;
using Application.StallTypes.Queries.GetStallType;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Queries.GetStallType
{
    public class GetStallTypeQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetStallType()
        {
            var request = new GetStallTypeRequest() { StallTypeId = 7000 };
            var query = new GetStallTypeQuery() { Dto = request };
            var handler = new GetStallTypeQuery.GetStallTypeQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.StallType.Should().NotBeNull();
            result.StallType.Name.Should().Be("Get Stalltype");
            result.StallType.Description.Should().Be("Get Stalltype description");
            result.StallType.Id.Should().Be(7000);
        }

        [Fact]
        public async Task Handle_NoSuchStallType()
        {
            var request = new GetStallTypeRequest() { StallTypeId = -1 };
            var query = new GetStallTypeQuery() { Dto = request };
            var handler = new GetStallTypeQuery.GetStallTypeQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}

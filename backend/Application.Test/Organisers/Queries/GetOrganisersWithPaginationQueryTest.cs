using Application.Organisers.Queries.GetAllOrganisersWithPagination;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries
{
    public class GetOrganisersWithPaginationQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetAllOrganisers()
        {
            var request = new GetOrganisersWithPaginationRequest() { PageNumber = 1, PageSize = int.MaxValue };

            var query = new GetOrganisersWithPaginationQuery() { Dto = request };
            var handler = new GetOrganisersWithPaginationQuery.GetOrganisersWithPaginationQueryHandler(Context, null);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Organisers.TotalCount.Should().Be(Context.Organisers.ToList().Count);
            result.Organisers.Items.Count.Should().BeLessOrEqualTo(request.PageSize);
        }

        [Fact]
        public async Task Handle_NoPageNumber()
        {
            var request = new GetOrganisersWithPaginationRequest() {PageSize = 10 };

            var query = new GetOrganisersWithPaginationQuery() { Dto = request };
            var handler = new GetOrganisersWithPaginationQuery.GetOrganisersWithPaginationQueryHandler(Context, null);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Organisers.TotalCount.Should().BeLessOrEqualTo(request.PageSize);
            result.Organisers.PageNumber.Should().Be(request.PageNumber);
            result.Organisers.Items.Count.Should().BeLessOrEqualTo(request.PageSize);

            foreach(var item in result.Organisers.Items)
            {
                Assert.True(Context.Organisers.Find(item.Id) != null);
            }
        }

        [Fact]
        public async Task Handle_NoPageSize()
        {
            var request = new GetOrganisersWithPaginationRequest() { };

            var query = new GetOrganisersWithPaginationQuery() { Dto = request };
            var handler = new GetOrganisersWithPaginationQuery.GetOrganisersWithPaginationQueryHandler(Context, null);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Organisers.TotalCount.Should().BeLessOrEqualTo(request.PageSize);
            result.Organisers.PageNumber.Should().Be(request.PageNumber);
            result.Organisers.Items.Count.Should().BeLessOrEqualTo(request.PageSize);

            foreach (var item in result.Organisers.Items)
            {
                Assert.True(Context.Organisers.Find(item.Id) != null);
            }
        }
    }
}
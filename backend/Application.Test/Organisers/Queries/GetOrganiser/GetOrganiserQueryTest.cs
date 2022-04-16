using Application.Common.Exceptions;
using Application.Organisers.Queries.GetOrganiser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetOrganiser()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1 };
            var request = new GetOrganiserQuery() { Dto = dto};
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
        }

        [Fact]
        public async Task Handle_NonExistentOrganiser()
        {
            var dto = new GetOrganiserQueryRequest() { Id = -1 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}

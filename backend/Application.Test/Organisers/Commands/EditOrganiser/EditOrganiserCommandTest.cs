using Application.Common.Exceptions;
using Application.Organisers.Commands.EditOrganiser;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.EditOrganiser
{
    public class EditOrganiserCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_EditOrganiser()
        {
            var oldOrg = Context.Organisers.FirstOrDefault(x => x.Id == 1);
            var request = new EditOrganiserRequest()
            {
                OrganiserId = 1,
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var handler = new EditOrganiserCommand.EditOrganiserCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
            var newOrg = Context.Organisers.FirstOrDefault(x => x.Id == 1);
            newOrg.Id.Should().Be(request.OrganiserId);
            newOrg.UserId.Should().Be(Guid.Empty.ToString());
            newOrg.Name.Should().Be(request.Name);
            newOrg.Description.Should().Be(request.Description);
            newOrg.Address.Street.Should().Be(request.Street);
            newOrg.Address.Number.Should().Be(request.Number);
            newOrg.Address.Appartment.Should().Be(request.Appartment);
            newOrg.Address.PostalCode.Should().Be(request.PostalCode);
            newOrg.Address.City.Should().Be(request.City);
        }

        [Fact]
        public async Task Handle_NonExistentOrganiser()
        {
            var oldOrg = Context.Organisers.FirstOrDefault(x => x.Id == 1);
            var request = new EditOrganiserRequest()
            {
                OrganiserId = -1,
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var handler = new EditOrganiserCommand.EditOrganiserCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }
    }
}

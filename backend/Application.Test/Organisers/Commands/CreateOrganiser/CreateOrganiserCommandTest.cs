using Application.Common.Exceptions;
using Application.Organisers.Commands.CreateOrganiser;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CreateOrganiser()
        {
            var request = new CreateOrganiserRequest()
            {
                UserId = Guid.Empty.ToString(),
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var handler = new CreateOrganiserCommand.CreateOrganiserCommandHandler(Context, new CurrentUserService(request.UserId));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Id.Should().BePositive();
            result.Name.Should().Be(request.Name);
            result.Appartment.Should().Be(request.Appartment);
            result.City.Should().Be(request.City);
            result.Description.Should().Be(request.Description);
            result.Number.Should().Be(request.Number);
            result.PostalCode.Should().Be(request.PostalCode);
            result.Street.Should().Be(request.Street);

            Context.Organisers.FirstOrDefault(o => o.Id == result.Id).Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_NonExistentUser()
        {
            var request = new CreateOrganiserRequest()
            {
                UserId = Guid.NewGuid().ToString(),
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var handler = new CreateOrganiserCommand.CreateOrganiserCommandHandler(Context, new CurrentUserService(request.UserId));
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

        }
    }
}

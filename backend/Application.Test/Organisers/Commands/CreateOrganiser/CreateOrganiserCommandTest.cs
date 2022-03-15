using Application.Common.Exceptions;
using Application.Organisers.Commands.CreateOrganiser;
using FluentAssertions;
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

            var handler = new CreateOrganiserCommand.CreateOrganiserCommandHandler(Context);
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
        public async Task Handle_EmptyName()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "",
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

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyAppartment()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "",
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

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyCity()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = null,
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyNumber()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyPostalCode()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "",
                Street = "Test Organiser Street"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EmptyStreet()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = ""
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            });
        }
    }
}

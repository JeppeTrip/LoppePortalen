using Application.Organisers.Commands.CreateOrganiser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.CreateOrganiser
{
    public class CreateOrganiserCommandValidatorTest
    {
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
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };
            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);

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
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
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
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
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
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
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
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
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
                Street = "",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyUserId()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Street",
                UserId = ""
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullName()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = null,
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };
            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);

        }

        [Fact]
        public async Task Handle_NullAppartment()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = null,
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NullCity()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = null,
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullDescription()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = null,
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullNumber()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = null,
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullPostalCode()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = null,
                Street = "Test Organiser Street",
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullStreet()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = null,
                UserId = "UserId"
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullUserId()
        {
            var request = new CreateOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Street",
                UserId = null
            };
            var command = new CreateOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new CreateOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }
    }
}

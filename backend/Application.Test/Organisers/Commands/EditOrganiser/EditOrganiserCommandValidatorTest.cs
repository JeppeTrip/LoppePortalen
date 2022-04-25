using Application.Organisers.Commands.EditOrganiser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.EditOrganiser
{
    public class EditOrganiserCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_EmptyName()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };
            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);

        }

        [Fact]
        public async Task Handle_EmptyAppartment()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_EmptyCity()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_EmptyNumber()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyPostalCode()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyStreet()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullName()
        {
            var request = new EditOrganiserRequest()
            {
                Name = null,
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };
            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);

        }

        [Fact]
        public async Task Handle_NullAppartment()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = null,
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NullCity()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = null,
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullDescription()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = null,
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullNumber()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = null,
                PostalCode = "Test Organiser Postal",
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullPostalCode()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = null,
                Street = "Test Organiser Street",
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullStreet()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = null,
                OrganiserId = 1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_HandleOrganiserIdZero()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Street",
                OrganiserId = 0
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(2); //empty and less than 1
        }


        [Fact]
        public async Task Handle_HandleOrganiserIdNegative()
        {
            var request = new EditOrganiserRequest()
            {
                Name = "Test Organiser Name",
                Appartment = "Test Organiser Appartment",
                City = "Test Organiser City",
                Description = "Test Organiser Description",
                Number = "Test Organiser Number",
                PostalCode = "Test Organiser Postal",
                Street = "Street",
                OrganiserId = -1
            };
            var command = new EditOrganiserCommand()
            {
                Dto = request
            };

            var valRes = await new EditOrganiserCommandValidator().ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }
    }
}

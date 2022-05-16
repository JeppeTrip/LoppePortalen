using Application.Organisers.Commands.RemoveContactInformation;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Application.Test.Organisers.Commands.RemoveContactInformation
{
    public class RemoveContactInformationCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 1, Value = "value" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var validator = new RemoveContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Handle_OrganiserIdZero()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 0, Value = "value" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var validator = new RemoveContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }


        [Fact]
        public void Handle_OrganiserIdNegative()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = -1, Value = "value" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var validator = new RemoveContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueNull()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 1, Value = null };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var validator = new RemoveContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueEmpty()
        {
            var request = new RemoveContactInformationRequest() { OrganiserId = 1, Value = "" };
            var command = new RemoveContactInformationCommand() { Dto = request };
            var validator = new RemoveContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

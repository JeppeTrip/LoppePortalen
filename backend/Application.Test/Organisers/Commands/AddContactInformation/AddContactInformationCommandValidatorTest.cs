using Application.Organisers.Commands.AddContactInformation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.AddContactInformation
{
    public class AddContactInformationCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1,
                Value = "value",
                Type = 0
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_OrganiserIdZero()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 0,
                Value = "value",
                Type = 0
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_OrganiserIdNegative()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = -1,
                Value = "value",
                Type = 0
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueEmpty()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1,
                Value = "",
                Type = 0
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueNull()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1,
                Value = null,
                Type = 0
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_TypeNotInEnum()
        {
            var request = new AddContactInformationRequest()
            {
                OrganiserId = 1,
                Value = null,
                Type = (Domain.Enums.ContactInfoType)(-1)
            };
            var command = new AddContactInformationCommand() { Dto = request };
            var validator = new AddContactInformationCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().BePositive();
        }
    }
}

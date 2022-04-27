using Application.StallTypes.Commands.CreateStallType;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype",
                Description = "Stalltype description",
                MarketId = 1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();

        }

        [Fact]
        public async Task Handle_EmptyName()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "",
                Description = "Stalltype description",
                MarketId = 1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullName()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = null,
                Description = "Stalltype description",
                MarketId = 1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype",
                Description = "",
                MarketId = 1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NullDescription()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype",
                Description = null,
                MarketId = 1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NegativeId()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype",
                Description = "Stalltype description",
                MarketId = -1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_ZeroId()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype",
                Description = "Stalltype description",
                MarketId = 0
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var validator = new CreateStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

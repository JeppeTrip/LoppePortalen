using Application.StallTypes.Commands.EditStallTypes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Commands.EditStallType
{
    public class EditStallTypeCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                StallTypeName = "Name",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        public async Task Handle_MarketIdZero()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 0,
                StallTypeId = 1,
                StallTypeName = "Name",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        public async Task Handle_MarketIdNegative()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = -1,
                StallTypeId = 1,
                StallTypeName = "Name",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        public async Task Handle_StallTypeIdZero()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 0,
                StallTypeName = "Name",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
        public async Task Handle_StallTypeIdNegative()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = -1,
                StallTypeName = "Name",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        public async Task Handle_NameEmpty()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                StallTypeName = "",
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        public async Task Handle_NameNull()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                StallTypeName = null,
                StallTypeDescription = "Description"
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        public async Task Handle_DescriptionEmpty()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                StallTypeName = "Name",
                StallTypeDescription = ""
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        public async Task Handle_DescriptionNull()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                StallTypeName = "Name",
                StallTypeDescription = null
            };
            var command = new EditStallTypeCommand()
            {
                Dto = request
            };
            var validator = new EditStallTypeCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

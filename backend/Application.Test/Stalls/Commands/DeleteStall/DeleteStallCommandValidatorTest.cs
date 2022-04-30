using Application.Stalls.Commands.DeleteStall;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Commands.DeleteStall
{
    public class DeleteStallCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new DeleteStallRequest() { StallId = 1 };
            var command = new DeleteStallCommand() { Dto = request };
            var validator = new DeleteStallCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_IdZero()
        {
            var request = new DeleteStallRequest() { StallId = 0 };
            var command = new DeleteStallCommand() { Dto = request };
            var validator = new DeleteStallCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_IdNegative()
        {
            var request = new DeleteStallRequest() { StallId = -1 };
            var command = new DeleteStallCommand() { Dto = request };
            var validator = new DeleteStallCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

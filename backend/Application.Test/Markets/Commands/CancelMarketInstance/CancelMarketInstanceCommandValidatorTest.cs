using Application.Markets.Commands.CancelMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.CancelMarketInstance
{
    public class CancelMarketInstanceCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 1 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var validator = new CancelMarketInstanceCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = 0 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var validator = new CancelMarketInstanceCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new CancelMarketInstanceRequest() { MarketId = -1 };
            var command = new CancelMarketInstanceCommand() { Dto = request };
            var validator = new CancelMarketInstanceCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

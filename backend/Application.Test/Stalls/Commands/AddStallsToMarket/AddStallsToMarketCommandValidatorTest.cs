using Application.Stalls.Commands.AddStallsToMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Commands.AddStallsToMarket
{
    public class AddStallsToMarketCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new AddStallsToMarketRequest() { 
              MarketId = 1,
              StallTypeId = 1,
              Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Handle_ZeroMarketId()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 0,
                StallTypeId = 1,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_NegativeMarketId()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = -1,
                StallTypeId = 1,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ZeroStallTypeId()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1,
                StallTypeId = 0,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_NegativeStallTypeId()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1,
                StallTypeId = -1,
                Number = 1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ZeroStallCount()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                Number = 0
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_NegativeStallCount()
        {
            var request = new AddStallsToMarketRequest()
            {
                MarketId = 1,
                StallTypeId = 1,
                Number = -1
            };
            var command = new AddStallsToMarketCommand() { Dto = request };
            var validator = new AddStallsToMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

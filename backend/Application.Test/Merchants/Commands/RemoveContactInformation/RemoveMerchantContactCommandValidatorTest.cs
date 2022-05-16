using Application.Merchants.Commands.RemoveContactInformation;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.RemoveContactInformation
{
    public class RemoveMerchantContactCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 1, Value = "value" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var validator = new RemoveMerchantContactCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Handle_MerchantIdZero()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 0, Value = "value" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var validator = new RemoveMerchantContactCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }


        [Fact]
        public void Handle_MerchantIdNegative()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = -1, Value = "value" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var validator = new RemoveMerchantContactCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueNull()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 1, Value = null };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var validator = new RemoveMerchantContactCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ValueEmpty()
        {
            var request = new RemoveMerchantContactRequest() { MerchantId = 1, Value = "" };
            var command = new RemoveMerchantContactCommand() { Dto = request };
            var validator = new RemoveMerchantContactCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

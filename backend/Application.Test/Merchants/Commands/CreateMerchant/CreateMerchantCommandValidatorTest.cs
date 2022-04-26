using Application.Merchants.Commands.CreateMerchant;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.CreateMerchant
{
    public class CreateMerchantCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "Merchant",
                Description = "Create Merchant Test",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var validator = new CreateMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeTrue();
;
        }

        [Fact]
        public async Task Handle_EmptyName()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "",
                Description = "Create Merchant Test",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var validator = new CreateMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "Merchant",
                Description = "",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var validator = new CreateMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NullName()
        {
            var request = new CreateMerchantRequest()
            {
                Name = null,
                Description = "Create Merchant Test",
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var validator = new CreateMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullDescription()
        {
            var request = new CreateMerchantRequest()
            {
                Name = "Merchant",
                Description = null,
            };

            var command = new CreateMerchantCommand() { Dto = request };
            var validator = new CreateMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }
    }
}

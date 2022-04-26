using Application.Merchants.Commands.CreateMerchant;
using Application.Merchants.Commands.EditMerchant;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.EditMerchant
{
    public class EditMerchantCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "Merchant",
                Description = "Create Merchant Test",
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_EmptyName()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "",
                Description = "Create Merchant Test",
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_EmptyDescription()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "Merchant",
                Description = "",
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NullName()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = null,
                Description = "Create Merchant Test",
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullDescription()
        {
            var request = new EditMerchantRequest()
            {
                Id = 2000,
                Name = "Merchant",
                Description = null,
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NegativeId()
        {
            var request = new EditMerchantRequest()
            {
                Id = -1,
                Name = "Merchant",
                Description = "Merchant Description"
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_ZeroId()
        {
            var request = new EditMerchantRequest()
            {
                Id = 0,
                Name = "Merchant",
                Description = "Merchant Description"
            };

            var command = new EditMerchantCommand() { Dto = request };
            var validator = new EditMerchantCommandValidator();
            var res = validator.Validate(command);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }
    }
}

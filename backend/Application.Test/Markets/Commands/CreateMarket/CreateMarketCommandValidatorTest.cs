using Application.Markets.Commands.CreateMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.CreateMarket
{
    public class CreateMarketCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_OrganiserIdZero()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 0,
                MarketName = "name",
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_OrganiserIdNegative()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = -1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketNameEmpty()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "",
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketNameNull()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = null,
                Description = "description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_DescriptionNull()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "name",
                Description = null,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_DescriptionEmpty()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "name",
                Description = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1)
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_StartDateBeforeEndDate()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "name",
                Description = "",
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now
            };
            var command = new CreateMarketCommand() { Dto = request };
            var validator = new CreateMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}

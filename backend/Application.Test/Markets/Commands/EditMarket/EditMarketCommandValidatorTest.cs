using Application.Markets.Commands.EditMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.EditMarket
{
    public class EditMarketCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();    
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 0,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = -1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_OrganiserIdZero() 
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 0,
                MarketId = 1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketNameEmpty()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = "",
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketNameNull()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = null,
                Description = "description",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketDescriptionEmpty()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = "name",
                Description = "",
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        public void Handle_MarketDescriptionNull()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = "name",
                Description = null,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        public void Handle_StartdateAfterEndDate()
        {
            var request = new EditMarketRequest()
            {
                OrganiserId = 1,
                MarketId = 1,
                MarketName = "name",
                Description = "description",
                StartDate = DateTimeOffset.Now.AddDays(1),
                EndDate = DateTimeOffset.Now
            };
            var command = new EditMarketCommand() { Dto = request };
            var validator = new EditMarketCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

    }
}

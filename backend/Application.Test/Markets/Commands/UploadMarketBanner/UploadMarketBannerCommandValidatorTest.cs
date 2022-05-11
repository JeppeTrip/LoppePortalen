using Application.Markets.Commands.UploadMarketBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.UploadMarketBanner
{
    public class UploadMarketBannerCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 0,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleNull()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = null
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleEmpty()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = ""
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataEmpty()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 1,
                ImageData = new byte[0],
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataNull()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 1,
                ImageData = null,
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var validator = new UploadMarketBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}

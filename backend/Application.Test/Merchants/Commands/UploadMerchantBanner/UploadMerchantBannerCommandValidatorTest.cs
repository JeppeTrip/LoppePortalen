using Application.Merchants.Commands.UploadMerchantBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.UploadMerchantBanner
{
    public class UploadMerchantBannerCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 0,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleNull()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = null
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleEmpty()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = ""
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataEmpty()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 1,
                ImageData = new byte[0],
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataNull()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 1,
                ImageData = null,
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var validator = new UploadMerchantBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}

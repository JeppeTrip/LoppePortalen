using Application.Booths.Commands.UploadBoothBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Commands.UploadBoothBanner
{
    public class UploadBoothBannerCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "boothId",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_BoothIdEmpty()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_BoothIdNull()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = null,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleNull()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "boothid",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = null
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleEmpty()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "boothid",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = ""
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataEmpty()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "boothid",
                ImageData = new byte[0],
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataNull()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "boothid",
                ImageData = null,
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var validator = new UploadBoothBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}

using Application.Organisers.Commands.UploadBanner;
using FluentAssertions;
using System.Text;
using Xunit;

namespace Application.Test.Organisers.Commands.UploadBanner
{
    public class UploadBannerCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_OrganiserIdZero()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 0,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_OrganiserIdNegative()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleNull()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = null
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_TitleEmpty()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = ""
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataEmpty()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 1,
                ImageData = new byte[0],
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_FileDataNull()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 1,
                ImageData = null,
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var validator = new UploadBannerCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

    }
}

using Application.Booths.Commands.UploadBoothBanner;
using Application.Common.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Commands.UploadBoothBanner
{
    public class UploadBoothBannerCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_UploadBanner()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "booking3500",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService("User3500"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.BoothId.Should().Be(request.BoothId);
            result.Title.Should().Be(request.Title);

            var image = Context.BookingImages.FirstOrDefault(x => x.BookingId.Equals(request.BoothId) && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_BoothDoesNotExist()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "DoesNotExist",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService("User3500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BoothOwnedByOtherUser()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "booking3501",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService("User3500"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "booking3500",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "booking3500",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BoothBannerAlreadyExist()
        {
            var request = new UploadBoothBannerRequest()
            {
                BoothId = "booking3501",
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "new_title"
            };
            var command = new UploadBoothBannerCommand() { Dto = request };
            var handler = new UploadBoothBannerCommand.UploadBoothBannerCommandHandler(Context, new CurrentUserService("User3501"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.BoothId.Should().Be(request.BoothId);
            result.Title.Should().Be(request.Title);

            var image = Context.BookingImages.FirstOrDefault(x => x.BookingId.Equals(request.BoothId) && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }
    }
}

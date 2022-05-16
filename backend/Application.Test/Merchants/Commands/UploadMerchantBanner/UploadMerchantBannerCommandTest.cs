using Application.Common.Exceptions;
using Application.Merchants.Commands.UploadMerchantBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Commands.UploadMerchantBanner
{
    public class UploadMerchantBannerCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_UploadBanner()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 3400,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService("User3400"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MerchantId.Should().Be(request.MerchantId);
            result.Title.Should().Be(request.Title);

            var image = Context.MerchantImages.FirstOrDefault(x => x.MerchantId == request.MerchantId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_MerchantDoesNotExist()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService("User3400"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantOwnedByOtherUser()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 3401,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService("User3400"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 3400,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 3400,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MerchantBannerAlreadyExist()
        {
            var request = new UploadMerchantBannerRequest()
            {
                MerchantId = 3401,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "new_title"
            };
            var command = new UploadMerchantBannerCommand() { Dto = request };
            var handler = new UploadMerchantBannerCommand.UploadMerchantBannerCommandHandler(Context, new CurrentUserService("User3401"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MerchantId.Should().Be(request.MerchantId);
            result.Title.Should().Be(request.Title);

            var image = Context.MerchantImages.FirstOrDefault(x => x.MerchantId == request.MerchantId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }
    }
}

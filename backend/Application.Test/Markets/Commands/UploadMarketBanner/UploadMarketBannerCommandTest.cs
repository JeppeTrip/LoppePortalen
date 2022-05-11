using Application.Common.Exceptions;
using Application.Markets.Commands.UploadMarketBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.UploadMarketBanner
{
    public class UploadMarketBannerCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_UploadBanner()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 3300,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService("User3300"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketId.Should().Be(request.MarketId);
            result.Title.Should().Be(request.Title);

            var image = Context.MarketImages.FirstOrDefault(x => x.MarketTemplateId == request.MarketId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_MarketDoesNotExist()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService("User3300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketOwnedByOtherUser()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 3301,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService("User3300"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 3300,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 3300,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketBannerAlreadyExist()
        {
            var request = new UploadMarketBannerRequest()
            {
                MarketId = 3301,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "new_title"
            };
            var command = new UploadMarketBannerCommand() { Dto = request };
            var handler = new UploadMarketBannerCommand.UploadMarketBannerCommandHandler(Context, new CurrentUserService("User3301"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketId.Should().Be(request.MarketId);
            result.Title.Should().Be(request.Title);

            var image = Context.MarketImages.FirstOrDefault(x => x.MarketTemplateId == request.MarketId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }
    }
}

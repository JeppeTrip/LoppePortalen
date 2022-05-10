using Application.Common.Exceptions;
using Application.Organisers.Commands.UploadBanner;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Commands.UploadBanner
{
    public class UploadBannerCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_UploadBanner()
        {
            var request = new UploadBannerRequest() {
            OrganiserId = 3200,
            ImageData = Encoding.ASCII.GetBytes("some_data"),
            Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService("User3200"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.OrganiserId.Should().Be(request.OrganiserId);
            result.Title.Should().Be(request.Title);

            var image = Context.OrganiserImages.FirstOrDefault(x => x.OrganiserId == request.OrganiserId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_OrganiserDoesNotExist()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = -1,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService("User3200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserOwnedByOtherUser()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 3201,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService("User3200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 3200,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNull()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 3200,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OrganiserBannerAlreadyExist()
        {
            var request = new UploadBannerRequest()
            {
                OrganiserId = 3201,
                ImageData = Encoding.ASCII.GetBytes("some_data"),
                Title = "new_title"
            };
            var command = new UploadBannerCommand() { Dto = request };
            var handler = new UploadBannerCommand.UploadBannerCommandHandler(Context, new CurrentUserService("User3201"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.OrganiserId.Should().Be(request.OrganiserId);
            result.Title.Should().Be(request.Title);

            var image = Context.OrganiserImages.FirstOrDefault(x => x.OrganiserId == request.OrganiserId && x.ImageTitle.Equals(request.Title));
            image.Should().NotBeNull();
        }
    }
}

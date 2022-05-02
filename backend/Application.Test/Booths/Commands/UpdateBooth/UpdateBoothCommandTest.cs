using Application.Booths.Commands.UpdateBooth;
using Application.Common.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Commands.UpdateBooth
{
    public class UpdateBoothCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_UpdateBooth()
        {
            var request = new UpdateBoothRequest() { 
                Id = "Booth1600",
                BoothName = "NewName",
                BoothDescription = "NewDescription",
                ItemCategories = new List<string>() { "Category 1601"}
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var handler = new UpdateBoothCommand.UpdateBoothCommandHandler(Context, new CurrentUserService("User1600"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();

            var booking = Context.Bookings.FirstOrDefault(x => x.Id.Equals("Booth1600"));
            booking.BoothName.Should().Be("NewName");
            booking.BoothDescription.Should().Be("NewDescription");
            booking.ItemCategories.Count().Should().Be(1);
            booking.ItemCategories.First().Name.Should().Be("Category 1601");
        }

        [Fact]
        public async Task Handle_BoothDoesNotExist()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "DoesNotExist",
                BoothName = "NewName",
                BoothDescription = "NewDescription",
                ItemCategories = new List<string>() { "Category 1601" }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var handler = new UpdateBoothCommand.UpdateBoothCommandHandler(Context, new CurrentUserService("User1600"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_BoothOwnedByOtherUser()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "Booth1601",
                BoothName = "NewName",
                BoothDescription = "NewDescription",
                ItemCategories = new List<string>() { "Category 1601" }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var handler = new UpdateBoothCommand.UpdateBoothCommandHandler(Context, new CurrentUserService("User1600"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

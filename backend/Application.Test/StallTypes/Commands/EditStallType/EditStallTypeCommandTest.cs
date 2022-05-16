using Application.Common.Exceptions;
using Application.StallTypes.Commands.EditStallTypes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Commands.EditStallType
{
    public class EditStallTypeCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_EditStallType()
        {
            var request = new EditStallTypeRequest() { 
                MarketId = 6000,
                StallTypeId = 6000,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));
            var result = await handler.Handle(command, CancellationToken.None);

            var entity = Context.StallTypes.FirstOrDefault(x => x.Id.Equals(request.StallTypeId));
            entity.Should().NotBeNull();
            entity.Name.Should().Be(request.StallTypeName);
            entity.Description.Should().Be(request.StallTypeDescription);
        }

        [Fact]
        public async Task Handle_NoSuchMarket()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = -1,
                StallTypeId = 6000,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketOwnedByOtherUser()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 6002,
                StallTypeId = 6000,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NoSuchStallType()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 6000,
                StallTypeId = -1,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeExistsOnAnotherOwnedMarket()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 6000,
                StallTypeId = 6002,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeExistsOnMarketOwnedByOtherUser()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 6000,
                StallTypeId = 6003,
                StallTypeName = "New Name",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NewNameAlreadyExists()
        {
            var request = new EditStallTypeRequest()
            {
                MarketId = 6000,
                StallTypeId = 6000,
                StallTypeName = "Edit Stalltype 2",
                StallTypeDescription = "New Description"
            };
            var command = new EditStallTypeCommand() { Dto = request };
            var handler = new EditStallTypeCommand.EditStallTypeCommandHandler(Context, new CurrentUserService("EditStallTypeUser1"));

            await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

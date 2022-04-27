using Application.Common.Exceptions;
using Application.StallTypes.Commands.CreateStallType;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Commands.CreateStallType
{
    public class CreateStallTypeCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CreateStallType()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Creat stall type",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("CreateStallTypeUser"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Id.Should().BePositive();
            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);    

            var entity = Context.StallTypes.FirstOrDefault(x => x.Id.Equals(result.Id));
            entity.Should().NotBeNull();
            entity.Name.Should().Be(request.Name);
            entity.Description.Should().Be(request.Description);
            entity.MarketTemplateId.Should().Be(request.MarketId);
        }

        [Fact]
        public async Task Handle_NoSuchMarket()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Creat stall type",
                Description = "Create stall type description",
                MarketId = -1
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("CreateStallTypeUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistantUser()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Creat stall type",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("ThisIsNotAUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NullUser()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Creat stall type",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserIsNotOwner()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Creat stall type",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("FakeCreateStallTypeUser"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeNameAlreadyOnMarket()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = "Stalltype Exists",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("CreateStallTypeUser"));

            await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallTypeNameAlreadyOnMarket_TrailingWhiteSpaces()
        {
            var request = new CreateStallTypeRequest()
            {
                Name = " Stalltype Exists ",
                Description = "Create stall type description",
                MarketId = 5000
            };
            var command = new CreateStallTypeCommand() { Dto = request };
            var handler = new CreateStallTypeCommand.CreateStallTypeCommandHandler(Context, new CurrentUserService("CreateStallTypeUser"));

            await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

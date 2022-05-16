using Application.Common.Exceptions;
using Application.Stalls.Commands.DeleteStall;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Commands.DeleteStall
{
    public class DeleteStallCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_DeleteStall()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1200
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));
            var result = await handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_StallDoesNotExist()
        {
            var request = new DeleteStallRequest()
            {
                StallId = -1
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotOwnStall()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1202
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserDoesNotExist()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1200
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("DoesNotExist"));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserIsNull()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1200
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService(null));

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketInProgress()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1204
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<ForbiddenAccessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketIsOver()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1203
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<ForbiddenAccessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_StallIsBooked()
        {
            var request = new DeleteStallRequest()
            {
                StallId =  1205
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<ForbiddenAccessException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_MarketIsCancelled()
        {
            var request = new DeleteStallRequest()
            {
                StallId = 1205
            };
            var command = new DeleteStallCommand() { Dto = request };
            var handler = new DeleteStallCommand.DeleteStallCommandHandler(Context, new CurrentUserService("User1200"));

            await Assert.ThrowsAsync<ForbiddenAccessException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}

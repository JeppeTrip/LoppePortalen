using Application.Common.Exceptions;
using Application.User.Queries.GetUser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.User.Queries.GetUser
{
    public class GetUserQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_Get_Existing_User()
        {
            var request = new GetUserRequest()
            {
                UserId = Guid.Empty.ToString()
            };

            var query = new GetUserQuery() { Dto = request};
            var handler = new GetUserQuery.GetUserQueryHandler(Context);
            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Errors.Should().BeEmpty();
            result.User.Id.Should().Be(Guid.Empty.ToString());
        }

        [Fact]
        public async Task Handle_No_User_With_Id()
        {
            var request = new GetUserRequest()
            {
                UserId = Guid.NewGuid().ToString()
            };

            var query = new GetUserQuery() { Dto = request };
            var handler = new GetUserQuery.GetUserQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}

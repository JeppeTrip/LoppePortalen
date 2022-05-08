using Application.Markets.Commands.CreateMarket;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.CreateMarket
{
    public class CreateMarketCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_CreateMarket()
        {
            var request = new CreateMarketRequest()
            {
                OrganiserId = 1,
                MarketName = "Test",
                Description = "Test market",
                StartDate = new DateTimeOffset(new DateTime(2022, 1, 1)),
                EndDate = new DateTimeOffset(new DateTime(2022, 1, 10)),
                };

            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Market.MarketId.Should().BePositive();


        }
    }
}

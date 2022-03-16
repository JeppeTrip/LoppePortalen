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
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context);

            var result = handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().BePositive();
            var templates = Context.MarketTemplates
                .Where(x => x.OrganiserId == request.OrganiserId && x.Name.Equals(request.MarketName) && x.Description.Equals(request.Description))
                .ToList();

            templates.Count().Should().Be(1);
        }

        [Fact (Skip = "Find out how to test validators.")]
        public async Task Handle_WrongOrganiserId()
        {

        }

        [Fact(Skip = "Find out how to test validators.")]
        public async Task Handle_NoMarketName()
        {

        }

        [Fact(Skip = "Find out how to test validators.")]
        public async Task Handle_NoMarketDescription()
        {

        }

        [Fact(Skip = "Find out how to test validators.")]
        public async Task Handle_NoMarketStartDate()
        {

        }

        [Fact(Skip = "Find out how to test validators.")]
        public async Task Handle_NoMarketEndDate()
        {

        }

        [Fact(Skip = "Find out how to test validators.")]
        public async Task Handle_NoEndDateBeforeStartDate()
        {

        }
    }
}

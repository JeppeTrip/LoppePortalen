﻿using Application.Markets.Commands.CreateMarket;
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
                Stalls = new List<StallDto> { new StallDto { 
                    Name = "Basic Stall", 
                    Description = "1x2m stall.",
                    Count = 100
                }}};

            var command = new CreateMarketCommand() { Dto = request };
            var handler = new CreateMarketCommand.CreateMarketCommandHandler(Context, new CurrentUserService(Guid.Empty.ToString()));

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketId.Should().BePositive();
            var templates = Context.MarketTemplates
                .Where(x => x.OrganiserId == request.OrganiserId && x.Name.Equals(request.MarketName) && x.Description.Equals(request.Description))
                .ToList();
            templates.Count().Should().Be(1);

            var instance = Context.MarketInstances.First(x => x.Id == result.MarketId);
            instance.Should().NotBeNull();
            instance.MarketTemplate.StallTypes.Should().HaveCount(1);
            Context.Stalls.Where(x => x.StallType.Name.Equals(request.Stalls[0].Name)).Should().HaveCount(100);

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

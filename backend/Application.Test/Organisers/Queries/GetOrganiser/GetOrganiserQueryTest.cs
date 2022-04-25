using Application.Common.Exceptions;
using Application.Organisers.Queries.GetOrganiser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQueryTest : TestBase
    {
        [Fact]
        public async Task Handle_GetOrganiser_NoMarkets()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1000 };
            var request = new GetOrganiserQuery() { Dto = dto};
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_GetOrganiser_OneMarket_NoStalls()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1001 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(1);
            result.Organiser.Markets.First().MarketId.Should().BePositive();
            result.Organiser.Markets.First().MarketName.Should().NotBeEmpty();
            result.Organiser.Markets.First().IsCancelled.Should().BeFalse();
            result.Organiser.Markets.First().StartDate.Should().BeSameDateAs(DateTime.Now);
            result.Organiser.Markets.First().EndDate.Should().BeSameDateAs(DateTime.Now.AddDays(1));
            result.Organiser.Markets.First().TotalStallCount.Should().Be(0);
            result.Organiser.Markets.First().AvailableStallCount.Should().Be(0);
            result.Organiser.Markets.First().OccupiedStallCount.Should().Be(0);
            result.Organiser.Markets.First().Categories.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_GetOrganiser_OneMarket_WithStallTypes_NoStalls()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1002 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(1);
            result.Organiser.Markets.First().MarketId.Should().BePositive();
            result.Organiser.Markets.First().MarketName.Should().NotBeEmpty();
            result.Organiser.Markets.First().IsCancelled.Should().BeFalse();
            result.Organiser.Markets.First().StartDate.Should().BeSameDateAs(DateTime.Now);
            result.Organiser.Markets.First().EndDate.Should().BeSameDateAs(DateTime.Now.AddDays(1));
            result.Organiser.Markets.First().TotalStallCount.Should().Be(0);
            result.Organiser.Markets.First().AvailableStallCount.Should().Be(0);
            result.Organiser.Markets.First().OccupiedStallCount.Should().Be(0);
            result.Organiser.Markets.First().Categories.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_GetOrganiser_OneMarket_WithStalls_NoBooths()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1003 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(1);
            result.Organiser.Markets.First().MarketId.Should().BePositive();
            result.Organiser.Markets.First().MarketName.Should().NotBeEmpty();
            result.Organiser.Markets.First().IsCancelled.Should().BeFalse();
            result.Organiser.Markets.First().StartDate.Should().BeSameDateAs(DateTime.Now);
            result.Organiser.Markets.First().EndDate.Should().BeSameDateAs(DateTime.Now.AddDays(1));
            result.Organiser.Markets.First().TotalStallCount.Should().Be(4);
            result.Organiser.Markets.First().AvailableStallCount.Should().Be(4);
            result.Organiser.Markets.First().OccupiedStallCount.Should().Be(0);
            result.Organiser.Markets.First().Categories.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_GetOrganiser_OneMarket_StallsAndBooths()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1005 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(1);
            result.Organiser.Markets.First().MarketId.Should().BePositive();
            result.Organiser.Markets.First().MarketName.Should().NotBeEmpty();
            result.Organiser.Markets.First().IsCancelled.Should().BeFalse();
            result.Organiser.Markets.First().StartDate.Should().BeSameDateAs(DateTime.Now);
            result.Organiser.Markets.First().EndDate.Should().BeSameDateAs(DateTime.Now.AddDays(1));
            result.Organiser.Markets.First().TotalStallCount.Should().Be(4);
            result.Organiser.Markets.First().AvailableStallCount.Should().Be(2);
            result.Organiser.Markets.First().OccupiedStallCount.Should().Be(2);
            result.Organiser.Markets.First().Categories.Count().Should().Be(4);
        }

        [Fact]
        public async Task Handle_GetOrganiser_MultipleMarkets()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1006 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();

            //Get the organiser that should be returned.
            var entity = Context.Organisers.FirstOrDefault(x => x.Id == dto.Id);
            //Test result data corresponds to that of the entity.
            result.Organiser.Id.Should().Be(entity.Id);
            result.Organiser.Name.Should().Be(entity.Name);
            result.Organiser.Description.Should().Be(entity.Description);
            result.Organiser.Street.Should().Be(entity.Address.Street);
            result.Organiser.StreetNumber.Should().Be(entity.Address.Number);
            result.Organiser.Appartment.Should().Be(entity.Address.Appartment);
            result.Organiser.PostalCode.Should().Be(entity.Address.PostalCode);
            result.Organiser.City.Should().Be(entity.Address.City);
            result.Organiser.Markets.Count().Should().Be(2);
        }

        [Fact]
        public async Task Handle_NonExistentOrganiser()
        {
            var dto = new GetOrganiserQueryRequest() { Id = -1 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var handler = new GetOrganiserQuery.GetOrganiserQueryHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}

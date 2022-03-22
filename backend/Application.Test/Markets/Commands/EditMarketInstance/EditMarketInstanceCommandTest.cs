using Application.Common.Exceptions;
using Application.Markets.Commands.EditMarketInstance;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Commands.EditMarketInstance
{
    public class EditMarketInstanceCommandTest : TestBase
    {
        [Fact]
        public async Task Handle_EditInstanceWithNoSiblingsAndNoChangeToTemplate()
        {
            var instance = Context.MarketInstances
                .Include(x => x.MarketTemplate)
                .FirstOrDefault(x => x.Id == 1);
            var request = new EditMarketInstanceRequest() {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = instance.MarketTemplate.Name,
                Description = instance.MarketTemplate.Description,
                EndDate = instance.StartDate.AddDays(10), 
                StartDate = instance.EndDate.AddDays(10)
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };
            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketInstanceId.Should().Be(instance.Id);
            result.StartDate.Should().Be(request.StartDate);
            result.EndDate.Should().Be(request.EndDate);
            result.MarketName.Should().Be(instance.MarketTemplate.Name);
            result.Description.Should().Be(instance.MarketTemplate.Description);
            result.OrganiserId.Should().Be(instance.MarketTemplate.OrganiserId);

        } 

        [Fact]
        public async Task Handle_EditInstanceWithSiblingsAndNoChangeToTemplate()
        {
            var instance = Context.MarketInstances
                .Include(x => x.MarketTemplate)
                .FirstOrDefault(x => x.Id == 2);

            var templateId = instance.MarketTemplateId;
            var numSiblings = instance.MarketTemplate.MarketInstances.Count();

            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 2,
                MarketName = instance.MarketTemplate.Name,
                Description = instance.MarketTemplate.Description,
                EndDate = instance.StartDate.AddDays(10),
                StartDate = instance.EndDate.AddDays(10)
            };

            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketInstanceId.Should().Be(instance.Id);
            result.StartDate.Should().Be(request.StartDate);
            result.EndDate.Should().Be(request.EndDate);
            result.MarketName.Should().Be(instance.MarketTemplate.Name);
            result.Description.Should().Be(instance.MarketTemplate.Description);
            result.OrganiserId.Should().Be(instance.MarketTemplate.OrganiserId);

            var newInstance = Context.MarketInstances
               .Include(x => x.MarketTemplate)
               .FirstOrDefault(x => x.Id == 2);

            newInstance.MarketTemplateId.Should().Be(templateId);
            newInstance.MarketTemplate.Name.Should().Be(request.MarketName);
            newInstance.MarketTemplate.Description.Should().Be(request.Description);
            newInstance.MarketTemplate.MarketInstances.Should().HaveCount(numSiblings);
        }

        [Fact]
        public async Task Handle_EditInstanceWithNoSiblingsAndChangeToTemplate()
        {
            var instance = Context.MarketInstances
                .Include(x => x.MarketTemplate)
                .FirstOrDefault(x => x.Id == 1);
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "Creative New Name",
                Description = "Creative New Description",
                EndDate = instance.StartDate.AddDays(10),
                StartDate = instance.EndDate.AddDays(10)
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };
            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            var newInstance = Context.MarketInstances
                .Include(x => x.MarketTemplate)
                .FirstOrDefault(x => x.Id == 1);

            result.Should().NotBeNull();
            result.MarketInstanceId.Should().Be(instance.Id);
            result.StartDate.Should().Be(request.StartDate);
            result.EndDate.Should().Be(request.EndDate);
            result.MarketName.Should().Be(request.MarketName);
            result.Description.Should().Be(request.Description);
            result.OrganiserId.Should().Be(instance.MarketTemplate.OrganiserId);
        
            newInstance.MarketTemplateId.Should().Be(instance.MarketTemplate.Id);
            newInstance.MarketTemplate.Name.Should().Be(request.MarketName);
            newInstance.MarketTemplate.Description.Should().Be(request.Description);
        }

        [Fact]
        public async Task Handle_EditInstanceWithSiblingsAndChangeToTemplate()
        {
            var instance = Context.MarketInstances
                .Include(x => x.MarketTemplate)
                .FirstOrDefault(x => x.Id == 2);

            var templateId = instance.MarketTemplateId;
            var numSiblings = instance.MarketTemplate.MarketInstances.Count();

            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 2,
                MarketName = "Creative New Name",
                Description = "Creative New Description",
                EndDate = instance.StartDate.AddDays(10),
                StartDate = instance.EndDate.AddDays(10)
            };

            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.MarketInstanceId.Should().Be(instance.Id);
            result.StartDate.Should().Be(request.StartDate);
            result.EndDate.Should().Be(request.EndDate);
            result.MarketName.Should().Be(instance.MarketTemplate.Name);
            result.Description.Should().Be(instance.MarketTemplate.Description);
            result.OrganiserId.Should().Be(instance.MarketTemplate.OrganiserId);

            var newInstance = Context.MarketInstances
               .Include(x => x.MarketTemplate)
               .FirstOrDefault(x => x.Id == 2);

            newInstance.MarketTemplateId.Should().NotBe(templateId);
            newInstance.MarketTemplate.Name.Should().Be(request.MarketName);
            newInstance.MarketTemplate.Description.Should().Be(request.Description);
            newInstance.MarketTemplate.MarketInstances.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handle_EditInstanceNonExistentInstanceId()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = int.MaxValue,
                MarketName = "This Should fail so doesn't matter",
                Description = "This Should fail so doesn't matter",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(1)
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };
            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_EditInstanceNonExistentOrganiserId()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = int.MaxValue,
                MarketInstanceId = 1,
                MarketName = "This Should fail so doesn't matter",
                Description = "This Should fail so doesn't matter",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(1)
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };
            var handler = new EditMarketInstanceCommand.EditMarketInstanceCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
        }

        [Fact]
        public async Task Handle_NegativeOrganiserId()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = -1,
                MarketInstanceId = 1,
                MarketName = "This Should fail so doesn't matter",
                Description = "This Should fail so doesn't matter",
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NoMarketName()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "",
                Description = "Description",
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
            valRes.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_NoMarketDescription()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "Market Name",
                Description = "",
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NoMarketStartDate()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "Name",
                Description = "Description",
                EndDate = DateTime.Now.AddDays(1),
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_NoMarketEndDate()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "Name",
                Description = "Description",
                StartDate = DateTime.Now,
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_NoEndDateBeforeStartDate()
        {
            var request = new EditMarketInstanceRequest()
            {
                OrganiserId = 1,
                MarketInstanceId = 1,
                MarketName = "Name",
                Description = "Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(-1),
            };
            var command = new EditMarketInstanceCommand()
            {
                Dto = request
            };

            var valRes = await new EditMarketInstanceCommandValidator()
                .ValidateAsync(command, CancellationToken.None);
            valRes.IsValid.Should().BeFalse();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Exceptions;
using Domain.EntityExtensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.EntityExtensions.MarketInstance
{
    public class ItemCategoriesTest : TestBase
    {
        [Fact]
        public async Task Handle_NoStalls()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9000));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9000");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(0);
        }

        [Fact]
        public async Task Handle_NoBooths()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9001));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9001");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(0);
        }

        [Fact]
        public async Task Handle_OneBoothNoCategory()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9002));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9002");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(0);
        }

        [Fact]
        public async Task Handle_OneBoothOneCategory()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9006));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9006");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(1);
            categoryList.First().Should().Be("Market Entity Extension Category 1");
        }

        [Fact]
        public async Task Handle_OneBoothMultipleCategory()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9007));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9007");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(2);
            categoryList.Should().Contain("Market Entity Extension Category 1");
            categoryList.Should().Contain("Market Entity Extension Category 2");
        }

        [Fact]
        public async Task Handle_MultipleBoothsNoCategories()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9004));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9004");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(0);
        }

        [Fact]
        public async Task Handle_MultipleBoothsUniqueCategories()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9008));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9008");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(3);
            categoryList.Should().Contain("Market Entity Extension Category 1");
            categoryList.Should().Contain("Market Entity Extension Category 2");
            categoryList.Should().Contain("Market Entity Extension Category 3");
        }

        [Fact]
        public async Task Handle_MultipleBoothsOverlappingCategories()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .ThenInclude(x => x.Bookings)
                .ThenInclude(x => x.ItemCategories)
                .FirstOrDefault(x => x.Id.Equals(9009));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9009");
            }

            var categoryList = market.ItemCategories();
            categoryList.Should().NotBeNull();
            categoryList.Should().HaveCount(3);
            categoryList.Should().Contain("Market Entity Extension Category 1");
            categoryList.Should().Contain("Market Entity Extension Category 2");
            categoryList.Should().Contain("Market Entity Extension Category 3");
        }
    }
}

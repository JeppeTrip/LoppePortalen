using Application.Common.Exceptions;
using Domain.EntityExtensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.EntityExtensions.MarketInstance
{
    public class TotalStallCountTest : TestBase
    {
        [Fact]
        public void Handle_NoStalls()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .FirstOrDefault(x => x.Id.Equals(9000));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9000");
            }

            var count = market.TotalStallCount();
            count.Should().Be(0);
        }

        [Fact]
        public void Handle_OneStall()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .FirstOrDefault(x => x.Id.Equals(9001));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9001");
            }

            var count = market.TotalStallCount();
            count.Should().Be(1);
        }

        [Fact]
        public void Handle_OneStallbooked()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .FirstOrDefault(x => x.Id.Equals(9002));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9002");
            }

            var count = market.TotalStallCount();
            count.Should().Be(1);
        }

        [Fact]
        public void Handle_MultipleStalls()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .FirstOrDefault(x => x.Id.Equals(9003));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9003");
            }

            var count = market.TotalStallCount();
            count.Should().Be(3);
        }

        [Fact]
        public void Handle_MultipleStallsBooked()
        {
            var market = Context.MarketInstances
                .Include(x => x.Stalls)
                .FirstOrDefault(x => x.Id.Equals(9004));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9004");
            }

            var count = market.TotalStallCount();
            count.Should().Be(3);
        }

        [Fact]
        public void Handle_MultipleStallsMixed()
        {
            var market = Context.MarketInstances
                 .Include(x => x.Stalls)
                 .FirstOrDefault(x => x.Id.Equals(9005));

            if (market == null)
            {
                throw new NotFoundException("MarketInstance", "9005");
            }

            var count = market.TotalStallCount();
            count.Should().Be(3);
        }
    }
}

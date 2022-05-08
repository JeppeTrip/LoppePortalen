using Application.Stalls.Queries.GetMarketStalls;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Queries.GetMarketStalls
{
    public class GetMarketStallsQueryValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new GetMarketStallsRequest() {MarketId = 1 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var validator = new GetMarketStallsQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new GetMarketStallsRequest() { MarketId = 0 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var validator = new GetMarketStallsQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new GetMarketStallsRequest() { MarketId = -1 };
            var query = new GetMarketStallsQuery() { Dto = request };
            var validator = new GetMarketStallsQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

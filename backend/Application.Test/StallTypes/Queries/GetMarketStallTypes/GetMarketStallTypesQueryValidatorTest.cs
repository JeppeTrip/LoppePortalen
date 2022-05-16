using Application.StallTypes.Queries.GetMarketStallTypes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Queries.GetMarketStallTypes
{
    public class GetMarketStallTypesQueryValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = 1 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var validator = new GetMarketStallTypesQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_NegativeId()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = -1 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var validator = new GetMarketStallTypesQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_ZeroId()
        {
            var request = new GetMarketStallTypesRequest() { MarketId = 0 };
            var query = new GetMarketStallTypesQuery() { Dto = request };
            var validator = new GetMarketStallTypesQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

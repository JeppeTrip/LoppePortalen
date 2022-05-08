using Application.Markets.Queries.GetMarketInstance;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetMarketInstance
{
    public class GetMarketInstanceQueryValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new GetMarketInstanceRequest()
            {
                MarketId = 1
            };
            var query = new GetMarketInstanceQuery() { Dto = request };
            var validator = new GetMarketInstanceQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new GetMarketInstanceRequest()
            {
                MarketId = 0
            };
            var query = new GetMarketInstanceQuery() { Dto = request };
            var validator = new GetMarketInstanceQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new GetMarketInstanceRequest()
            {
                MarketId = -1
            };
            var query = new GetMarketInstanceQuery() { Dto = request };
            var validator = new GetMarketInstanceQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
        }
    }
}

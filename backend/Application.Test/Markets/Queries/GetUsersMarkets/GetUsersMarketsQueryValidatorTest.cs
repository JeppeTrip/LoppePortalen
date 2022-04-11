using Application.Markets.Queries.GetUsersMarkets;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Markets.Queries.GetUsersMarkets
{
    public class GetUsersMarketsQueryValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidUserId()
        {
            var dto = new GetUsersMarketsRequest() { UserId = Guid.Empty.ToString() };
            var request = new GetUsersMarketsQuery() { Dto = dto };
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_UserIdEmpty()
        {
            var dto = new GetUsersMarketsRequest() { UserId = "" };
            var request = new GetUsersMarketsQuery() { Dto = dto };
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public async Task Handle_UserIdNull()
        {
            var dto = new GetUsersMarketsRequest() { UserId = null };
            var request = new GetUsersMarketsQuery() { Dto = dto };
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }
    }
}

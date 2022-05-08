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
        public void Handle_ValidUserId()
        {
            var request = new GetUsersMarketsQuery() ;
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_UserIdEmpty()
        {
            var request = new GetUsersMarketsQuery();
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Handle_UserIdNull()
        {
            var request = new GetUsersMarketsQuery();
            var validator = new GetUsersMarketsQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }
    }
}

using Application.Stalls.Queries.GetStall;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Stalls.Queries.GetStall
{
    public class GetStallQueryValidatorTest : TestBase
    {
        [Fact]
        public async void Handle_ValidRequest()
        {
            var request = new GetStallRequest() { StallId = 1 };
            var query = new GetStallQuery() { Dto = request };
            var validator = new GetStallQueryValidator();
            var result = validator.Validate(query); 

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();    
        }

        [Fact]
        public async void Handle_ZeroId()
        {
            var request = new GetStallRequest() { StallId = 0 };
            var query = new GetStallQuery() { Dto = request };
            var validator = new GetStallQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async void Handle_NegativeId()
        {
            var request = new GetStallRequest() { StallId = -1 };
            var query = new GetStallQuery() { Dto = request };
            var validator = new GetStallQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

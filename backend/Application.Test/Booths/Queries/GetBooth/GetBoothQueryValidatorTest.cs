using Application.Booths.Queries.GetBooth;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Queries.GetBooth
{
    public class GetBoothQueryValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new GetBoothRequest() { Id = "id" };
            var query = new GetBoothQuery() { Dto = request };
            var validator = new GetBoothQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_EmptyId()
        {
            var request = new GetBoothRequest() { Id = "" };
            var query = new GetBoothQuery() { Dto = request };
            var validator = new GetBoothQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NullId()
        {
            var request = new GetBoothRequest() { Id = null };
            var query = new GetBoothQuery() { Dto = request };
            var validator = new GetBoothQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }
    }
}

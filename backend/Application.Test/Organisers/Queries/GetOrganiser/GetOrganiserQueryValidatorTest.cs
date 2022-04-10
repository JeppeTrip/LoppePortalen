using Application.Organisers.Queries.GetOrganiser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Organisers.Queries.GetOrganiser
{
    public class GetOrganiserQueryValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 1 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var validator = new GetOrganiserQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_EmptyId()
        {
            var dto = new GetOrganiserQueryRequest() { Id = 0 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var validator = new GetOrganiserQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
        }

        [Fact]
        public async Task Handle_NegativeId()
        {
            var dto = new GetOrganiserQueryRequest() { Id = -1 };
            var request = new GetOrganiserQuery() { Dto = dto };
            var validator = new GetOrganiserQueryValidator();
            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }
    }
}

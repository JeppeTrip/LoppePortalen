using Application.StallTypes.Queries.GetStallType;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.StallTypes.Queries.GetStallType
{
    public class GetStallTypeQueryValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new GetStallTypeRequest() { StallTypeId = 1 };
            var query = new GetStallTypeQuery() { Dto = request };
            var validator = new GetStallTypeQueryValidator();
            var res = validator.Validate(query);

            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_IdNegative()
        {
            var request = new GetStallTypeRequest() { StallTypeId = -1 };
            var query = new GetStallTypeQuery() { Dto = request };
            var validator = new GetStallTypeQueryValidator();
            var res = validator.Validate(query);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }

        [Fact]
        public void Handle_IdZero()
        {
            var request = new GetStallTypeRequest() { StallTypeId = 0 };
            var query = new GetStallTypeQuery() { Dto = request };
            var validator = new GetStallTypeQueryValidator();
            var res = validator.Validate(query);

            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }
    }
}

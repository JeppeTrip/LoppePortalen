using Application.Merchants.Queries.GetMerchant;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Merchants.Queries.GetMerchant
{
    public class GetMerchantQueryValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_ValidRequest()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 1
            };
            var command = new GetMerchantQuery() { Dto = request };
            var validator = new GetMerchantQueryValidator();
            var res = validator.Validate(command);
            res.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NegativeId()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = -1
            };
            var command = new GetMerchantQuery() { Dto = request };
            var validator = new GetMerchantQueryValidator();
            var res = validator.Validate(command);
            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);  
        }

        [Fact]
        public async Task Handle_ZeroId()
        {
            var request = new GetMerchantQueryRequest()
            {
                Id = 0
            };
            var command = new GetMerchantQuery() { Dto = request };
            var validator = new GetMerchantQueryValidator();
            var res = validator.Validate(command);
            res.IsValid.Should().BeFalse();
            res.Errors.Count().Should().Be(1);
        }
    }
}

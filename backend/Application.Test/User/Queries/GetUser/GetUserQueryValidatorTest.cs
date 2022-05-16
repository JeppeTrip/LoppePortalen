using Application.User.Queries.GetUser;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.User.Queries.GetUser
{
    public class GetUserQueryValidatorTest
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new GetUserRequest()
            {
                UserId = Guid.Empty.ToString()
            };

            var query = new GetUserQuery() { Dto = request };
            var validator = new GetUserQueryValidator();
            var result = validator.Validate(query);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_UserId_Null()
        {
            var request = new GetUserRequest()
            {
                UserId = null
            };

            var query = new GetUserQuery() { Dto = request };
            var validator = new GetUserQueryValidator();

            var result = validator.Validate(query);
            result.IsValid!.Should().BeFalse();
        }

        [Fact]
        public void Handle_UserId_Empty()
        {
            var request = new GetUserRequest()
            {
                UserId = ""
            };

            var query = new GetUserQuery() { Dto = request };
            var validator = new GetUserQueryValidator();

            var result = validator.Validate(query);
            result.IsValid!.Should().BeFalse();
        }
    }
}

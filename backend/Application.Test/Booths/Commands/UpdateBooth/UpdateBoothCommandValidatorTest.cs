using Application.Booths.Commands.UpdateBooth;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Booths.Commands.UpdateBooth
{
    public class UpdateBoothCommandValidatorTest : TestBase
    {
        [Fact]
        public async Task Handle_EverythingFilledOut()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "name",
                BoothDescription = "description",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_IdEmpty()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "",
                BoothName = "name",
                BoothDescription = "description",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_IdNull()
        {
            var request = new UpdateBoothRequest()
            {
                Id = null,
                BoothName = "name",
                BoothDescription = "description",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_NameEmpty()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "",
                BoothDescription = "description",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_NameNull()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = null,
                BoothDescription = "description",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_DescriptionEmpty()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "name",
                BoothDescription = "",
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_DescriptionNull()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "name",
                BoothDescription = null,
                ItemCategories = new List<string>() { "category " }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().Be(1);
        }

        [Fact]
        public async Task Handle_CategoriesEmpty()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "name",
                BoothDescription = "",
                ItemCategories = new List<string>() { }
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_CategoriesNull()
        {
            var request = new UpdateBoothRequest()
            {
                Id = "id",
                BoothName = "name",
                BoothDescription = null,
                ItemCategories = null
            };
            var command = new UpdateBoothCommand() { Dto = request };
            var validator = new UpdateBoothCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Count().Should().BePositive();
        }
    }
}

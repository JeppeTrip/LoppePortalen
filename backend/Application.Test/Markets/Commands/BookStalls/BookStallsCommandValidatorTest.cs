using Application.Markets.Commands.BookStalls;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Application.Test.Markets.Commands.BookStalls
{
    public class BookStallsCommandValidatorTest : TestBase
    {
        [Fact]
        public void Handle_ValidRequest()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() { 
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Handle_MarketIdZero()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 0,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MarketIdNegative()
        {
            var request = new BookStallsRequest()
            {
                MarketId = -1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MerchantIdZero()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 0,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_MerchantIdNegative()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = -1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_EmptyStallList()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>()
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_StallTypeIdZero()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 0, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_StallTypeNegative()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = -2, BookingAmount = 1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_BookingAmountNegative()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = -1 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Handle_BookingAmountZero()
        {
            var request = new BookStallsRequest()
            {
                MarketId = 1,
                MerchantId = 1,
                Stalls = new List<StallBooking>() {
                    new StallBooking() { StallTypeId = 1, BookingAmount = 1 },
                    new StallBooking() { StallTypeId = 2, BookingAmount = 0 }}
            };
            var command = new BookStallsCommand() { Dto = request };
            var validator = new BookStallsCommandValidator();
            var result = validator.Validate(command);

            result.IsValid.Should().BeFalse();
        }
    }
}

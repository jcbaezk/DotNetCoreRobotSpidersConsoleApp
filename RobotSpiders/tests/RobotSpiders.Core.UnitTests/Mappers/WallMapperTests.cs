using System;
using FluentAssertions;
using RobotSpiders.Core.Exceptions;
using RobotSpiders.Core.Mappers;
using Xunit;

namespace RobotSpiders.Core.UnitTests.Mappers
{
    public class WallMapperTests
    {
        [Theory]
        [InlineData("A B")]
        [InlineData("12")]
        [InlineData("12 A")]
        [InlineData("A 23")]
        [InlineData("-12 23")]
        [InlineData("12 -23")]
        [InlineData("0 0")]
        [InlineData("3 7 10")]
        public void ToDomain_ShouldThrowExceptionGivenInputIsNotValid(string input)
        {
            var mapper = new WallMapper();

            Action act = () => mapper.ToDomain(input);

            act.Should().Throw<WallInputException>();
        }

        [Fact]
        public void ToDomain_ShouldMapValidWallInputToDomain()
        {
            const int expectedHorizontalSize = 7;
            const int expectedVerticalSize = 17;
            var validWallInput = $"{expectedHorizontalSize} {expectedVerticalSize}";
            var mapper = new WallMapper();

            var result = mapper.ToDomain(validWallInput);

            result.Should().NotBeNull();
            result.HorizontalSize.Should().Be(expectedHorizontalSize);
            result.VerticalSize.Should().Be(expectedVerticalSize);
        }
    }
}
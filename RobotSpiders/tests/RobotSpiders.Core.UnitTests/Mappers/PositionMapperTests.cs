using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Mappers;
using Xunit;

namespace RobotSpiders.Core.UnitTests.Mappers
{
    public class PositionMapperTests
    {
        private readonly PositionMapper _mapper;

        public PositionMapperTests()
        {
            _mapper = new PositionMapper();
        }

        [Fact]
        public void ToDomain_ShouldMapPositionObjectGivenValidInput()
        {
            var expectedX = new Fixture().Create<int>();
            var expectedY = new Fixture().Create<int>();
            var expectedOrientation = new Fixture().Create<Orientation>();
            var input = new List<string> {expectedX.ToString(), expectedY.ToString(), expectedOrientation.ToString()};

            var result = _mapper.ToDomain(input);

            result.Should().NotBeNull();
            result.X.Should().Be(expectedX);
            result.Y.Should().Be(expectedY);
            result.Orientation.Should().Be(expectedOrientation);
        }

        [Theory]
        [InlineData("ABC", "DEF")]
        [InlineData("-1", "-21")]
        public void ToDomain_ShouldMapDefaultPositionObjectValuesGivenValidInput(string x, string y)
        {
            var expectedOrientation = new Fixture().Create<Orientation>();
            var input = new List<string> { x, y, expectedOrientation.ToString() };

            var result = _mapper.ToDomain(input);

            result.Should().NotBeNull();
            result.X.Should().Be(0);
            result.Y.Should().Be(0);
        }

        [Fact]
        public void ToTextFileString_ShouldReturnStringWithTextFileFormat()
        {
            var position = new Fixture().Create<Position>();
            var expectedValue = $"{position.X} {position.Y} {position.Orientation.ToString()}";

            var result = _mapper.ToTextFileString(position);

            result.Should().Be(expectedValue);
        }
    }
}
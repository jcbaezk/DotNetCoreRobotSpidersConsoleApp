using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using RobotSpiders.Core.Exceptions;
using RobotSpiders.Core.Mappers;
using Xunit;

namespace RobotSpiders.Core.UnitTests.Mappers
{
    public class SpiderMapperTests
    {
        private readonly SpiderMapper _mapper;
        private readonly Mock<IPositionMapper> _positionMapper;
        private readonly Mock<IInstructionMapper> _instructionMapper;

        public SpiderMapperTests()
        {
            _positionMapper = new Mock<IPositionMapper>();
            _instructionMapper = new Mock<IInstructionMapper>();
            _mapper = new SpiderMapper(_positionMapper.Object, _instructionMapper.Object);
        }

        [Theory]
        [InlineData("12")]
        [InlineData("12 20 ")]
        [InlineData("12 20 NotValid")]
        [InlineData("12 23 Left sjs")]
        public void ToDomain_ShouldThrowExceptionGivenPositionInputIsNotValid(string input)
        {
            var inputs = new List<string> {input, string.Empty};

            Action act = () => _mapper.ToDomains(inputs);

            act.Should().Throw<SpiderInputException>();
        }

        [Theory]
        [InlineData("FFFJ")]
        [InlineData("XFFF")]
        [InlineData("FFAF")]
        public void ToDomain_ShouldThrowExceptionGivenInstructionInputIsNotValid(string input)
        {
            var inputs = new List<string> { "1 2 Left", input };

            Action act = () => _mapper.ToDomains(inputs);

            act.Should().Throw<SpiderInputException>();
        }

        [Fact]
        public void ToDomain_ShouldMapValidSpiderInputToDomain()
        {
            var inputs = new List<string> { "1 2 Left", "LRLRF" };

            var result = _mapper.ToDomains(inputs);

            result.Should().NotBeNull();
            _positionMapper.Verify(x => x.ToDomain(It.IsAny<IReadOnlyList<string>>()), Times.AtLeastOnce);
            _instructionMapper.Verify(x => x.ToEnums(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
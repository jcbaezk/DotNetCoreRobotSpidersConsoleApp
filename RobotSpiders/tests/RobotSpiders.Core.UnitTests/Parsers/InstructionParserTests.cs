using FluentAssertions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Parsers;
using Xunit;

namespace RobotSpiders.Core.UnitTests.Parsers
{
    public class InstructionParserTests
    {
        [Theory]
        [InlineData('L', Instruction.RotateLeft)]
        [InlineData('R', Instruction.RotateRight)]
        [InlineData('F', Instruction.MoveForward)]
        public void Parse_ShouldParseCharacterToEnum(char inputValue, Instruction expectedValue)
        {
            var parser = new InstructionParser();

            var result = parser.Parse(inputValue);

            result.Should().Be(expectedValue);
        }
    }
}
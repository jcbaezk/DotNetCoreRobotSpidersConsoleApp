using AutoFixture;
using FluentAssertions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Process.Movers;
using Xunit;

namespace RobotSpiders.Process.UnitTests.Movers
{
    public class LeftRotatorTests
    {
        private readonly LeftRotator _leftRotator;
        private readonly Fixture _fixture;

        public LeftRotatorTests()
        {
            _leftRotator = new LeftRotator();
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(Instruction.MoveForward)]
        [InlineData(Instruction.RotateRight)]
        public void CanMove_ShouldReturnFalseGivenAnIncorrectInstruction(Instruction instruction)
        {
            var result = _leftRotator.CanMove(instruction);

           result.Should().BeFalse();
        }

        [Fact]
        public void CanMove_ShouldReturnTrueGivenACorrectInstruction()
        {
            var result = _leftRotator.CanMove(Instruction.RotateLeft);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Orientation.Up, Orientation.Left)]
        [InlineData(Orientation.Left, Orientation.Down)]
        [InlineData(Orientation.Down, Orientation.Right)]
        [InlineData(Orientation.Right, Orientation.Up)]
        public void Move_ShouldChangeTheOrientation(Orientation currentOrientation, Orientation expectedOrientation)
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = _fixture.Create<int>();
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = currentOrientation

            }; 

            _leftRotator.Move(position, new Wall());

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }
    }
}
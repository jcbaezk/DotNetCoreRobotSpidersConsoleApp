using AutoFixture;
using FluentAssertions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Process.Movers;
using Xunit;

namespace RobotSpiders.Process.UnitTests.Movers
{
    public class RightRotatorTests
    {
        private readonly RightRotator _rightRotator;
        private readonly Fixture _fixture;

        public RightRotatorTests()
        {
            _rightRotator = new RightRotator();
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(Instruction.MoveForward)]
        [InlineData(Instruction.RotateLeft)]
        public void CanMove_ShouldReturnFalseGivenAnIncorrectInstruction(Instruction instruction)
        {
            var result = _rightRotator.CanMove(instruction);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanMove_ShouldReturnTrueGivenACorrectInstruction()
        {
            var result = _rightRotator.CanMove(Instruction.RotateRight);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(Orientation.Up, Orientation.Right)]
        [InlineData(Orientation.Left, Orientation.Up)]
        [InlineData(Orientation.Down, Orientation.Left)]
        [InlineData(Orientation.Right, Orientation.Down)]
        public void Move_ShouldReturnPositionWithChangedOrientation(Orientation currentOrientation, Orientation expectedOrientation)
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = _fixture.Create<int>();
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = currentOrientation

            };

            _rightRotator.Move(position, new Wall());

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }
    }
}
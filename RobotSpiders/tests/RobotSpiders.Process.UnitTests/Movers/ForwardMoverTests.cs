using AutoFixture;
using FluentAssertions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Process.Movers;
using Xunit;

namespace RobotSpiders.Process.UnitTests.Movers
{
    public class ForwardMoverTests
    {
        private readonly ForwardMover _forwardMover;
        private readonly Fixture _fixture;
        private readonly Wall _wall;
        

        public ForwardMoverTests()
        {
            _forwardMover = new ForwardMover();
            _fixture = new Fixture();
            _wall = new Wall
            {
                HorizontalSize = 7,
                VerticalSize = 15
            }; 
        }

        [Theory]
        [InlineData(Instruction.RotateLeft)]
        [InlineData(Instruction.RotateRight)]
        public void CanMove_ShouldReturnFalseGivenAnIncorrectInstruction(Instruction instruction)
        {
            var result = _forwardMover.CanMove(instruction);

            result.Should().BeFalse();
        }

        [Fact]
        public void CanMove_ShouldReturnTrueGivenACorrectInstruction()
        {
            var result = _forwardMover.CanMove(Instruction.MoveForward);

            result.Should().BeTrue();
        }

        [Fact]
        public void Move_ShouldNotMoveUpGivenTheTopOfTheWallHasBeenReached()
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = _wall.VerticalSize - 1;
            var expectedOrientation = Orientation.Up;
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldNotMoveDownGivenTheBottomOfTheWallHasBeenReached()
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = 0;
            var expectedOrientation = Orientation.Down;
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };
            
            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldNotMoveToTheLeftEdgeOfTheWallHasBeenReached()
        {
            var expectedXValue = 0;
            var expectedYValue = _fixture.Create<int>();
            var expectedOrientation = Orientation.Left;
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };
           
            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldNotMoveToTheRightGivenTheRightEdgeOfTheWallHasBeenReached()
        {
            var expectedXValue = _wall.HorizontalSize - 1;
            var expectedYValue = _fixture.Create<int>();
            var expectedOrientation = Orientation.Right;
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }
       
        [Fact]
        public void Move_ShouldMoveUpGivenTheTopOfTheWallHasNotBeenReached()
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = _wall.VerticalSize - 1;
            var expectedOrientation = Orientation.Up;
            var position = new Position
            {
                X = expectedXValue,
                Y = _wall.VerticalSize - 2,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldMoveDownGivenTheBottomOfTheWallHasNotBeenReached()
        {
            var expectedXValue = _fixture.Create<int>();
            var expectedYValue = 1;
            var expectedOrientation = Orientation.Down;
            var position = new Position
            {
                X = expectedXValue,
                Y = expectedYValue + 1,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldMoveTheLeftEdgeOfTheWallHasBeenReached()
        {
            var expectedXValue = 1;
            var expectedYValue = _fixture.Create<int>();
            var expectedOrientation = Orientation.Left;
            var position = new Position
            {
                X = expectedXValue + 1,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

        [Fact]
        public void Move_ShouldMoveGivenTheRightEdgeOfTheWallHasNotBeenReached()
        {
            var expectedXValue = _wall.HorizontalSize - 2;
            var expectedYValue = _fixture.Create<int>();
            var expectedOrientation = Orientation.Right;
            var position = new Position
            {
                X = expectedXValue - 1,
                Y = expectedYValue,
                Orientation = expectedOrientation
            };

            _forwardMover.Move(position, _wall);

            position.X.Should().Be(expectedXValue);
            position.Y.Should().Be(expectedYValue);
            position.Orientation.Should().Be(expectedOrientation);
        }

    }
}
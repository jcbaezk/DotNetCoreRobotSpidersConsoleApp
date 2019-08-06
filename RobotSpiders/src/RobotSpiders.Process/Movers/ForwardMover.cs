using System;
using System.ComponentModel.Design;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Movers
{
    public class ForwardMover : ISpiderMover
    {
        public bool CanMove(Instruction instruction)
        {
            return instruction == Instruction.MoveForward;
        }

        public void Move(Position position, Wall wall)
        {
            if (position.Orientation == Orientation.Down && position.Y > 0)
                position.Y--;
            else if (position.Orientation == Orientation.Up && position.Y < wall.VerticalSize - 1)
                position.Y++;
            else if (position.Orientation == Orientation.Left && position.X > 0)
                position.X--;
            else if (position.Orientation == Orientation.Right && position.X < wall.HorizontalSize - 1)
                position.X++;
        }

    }
}
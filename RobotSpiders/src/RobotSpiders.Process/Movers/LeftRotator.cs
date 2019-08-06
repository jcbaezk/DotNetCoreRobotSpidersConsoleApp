using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Movers
{
    public class LeftRotator : ISpiderMover
    {
        public bool CanMove(Instruction instruction)
        {
            return instruction == Instruction.RotateLeft;
        }

        public void Move(Position position, Wall wall)
        {
            if (position.Orientation == Orientation.Up)
                position.Orientation = Orientation.Left;
            else if (position.Orientation == Orientation.Right)
                position.Orientation = Orientation.Up;
            else if (position.Orientation == Orientation.Down)
                position.Orientation = Orientation.Right;
            else if (position.Orientation == Orientation.Left)
                position.Orientation = Orientation.Down;
        }
    }
}
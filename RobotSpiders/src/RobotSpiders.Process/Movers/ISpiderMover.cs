using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Movers
{
    public interface ISpiderMover
    {
        bool CanMove(Instruction instruction);
        void Move(Position position, Wall wall);
    }
}
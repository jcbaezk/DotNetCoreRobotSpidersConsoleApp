using System.Collections.Generic;
using RobotSpiders.Core.Domain;
using RobotSpiders.Process.Movers;

namespace RobotSpiders.Process.Explorers
{
    public class SpiderController : ISpiderController
    {
        private readonly IEnumerable<ISpiderMover> _spiderMovers;

        public SpiderController(IEnumerable<ISpiderMover> spiderMovers)
        {
            _spiderMovers = spiderMovers;
        }

        public void Control(Spider spider, Wall wall)
        {
            foreach (var instruction in spider.Instructions)
            {
                MovePosition(spider.Position, instruction, wall);
            }
        }

        private void MovePosition(Position currentPosition, Instruction instruction, Wall wall)
        {
            foreach (var mover in _spiderMovers)
            {
                if (mover.CanMove(instruction))
                {
                    mover.Move(currentPosition, wall);
                }
            }
        }
    }
}
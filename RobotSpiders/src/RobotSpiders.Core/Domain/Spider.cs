using System.Collections.Generic;

namespace RobotSpiders.Core.Domain
{
    public class Spider
    {
        public Position Position { get; set; }
        public IEnumerable<Instruction> Instructions { get; set; }
    }
}
using System.Collections.Generic;

namespace RobotSpiders.Core.Domain
{
    public class Building
    {
        public Wall Wall { get; set; }

        public IEnumerable<Spider> Spiders { get; set; }
    }
}
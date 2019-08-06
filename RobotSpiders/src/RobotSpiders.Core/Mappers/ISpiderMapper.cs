using System.Collections.Generic;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Mappers
{
    public interface ISpiderMapper
    {
        IEnumerable<Spider> ToDomains(IEnumerable<string> inputs);
    }
}
using System.Collections.Generic;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Mappers
{
    public interface IInstructionMapper
    {
        IEnumerable<Instruction> ToEnums(string input);
    }
}
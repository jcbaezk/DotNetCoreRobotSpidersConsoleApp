using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Parsers
{
    public interface IInstructionParser
    {
        Instruction Parse(char instructionInput);
    }
}
using RobotSpiders.Core.Constants;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Parsers
{
    public class InstructionParser : IInstructionParser
    {
        public Instruction Parse(char instructionInput)
        {
            if (instructionInput == InputInstructions.MoveForward)
                return Instruction.MoveForward;
            else if (instructionInput == InputInstructions.RotateLeft)
                return Instruction.RotateLeft;
            else if (instructionInput == InputInstructions.RotateRight)
                return Instruction.RotateRight;
            return Instruction.DoNothing;
        }
    }
}
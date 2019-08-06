using System.Collections.Generic;
using System.Linq;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Parsers;

namespace RobotSpiders.Core.Mappers
{
    public class InstructionMapper : IInstructionMapper
    {
        private readonly IInstructionParser _instructionParser;

        public InstructionMapper(IInstructionParser instructionParser)
        {
            _instructionParser = instructionParser;
        }

        public IEnumerable<Instruction> ToEnums(string input)
        {
            return input.ToCharArray().Select(x => _instructionParser.Parse(x));
        }
    }
}
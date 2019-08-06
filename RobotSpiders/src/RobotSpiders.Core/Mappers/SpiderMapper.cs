using System;
using System.Collections.Generic;
using System.Linq;
using RobotSpiders.Core.Constants;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Exceptions;

namespace RobotSpiders.Core.Mappers
{
    public class SpiderMapper : ISpiderMapper
    {
        private readonly IPositionMapper _positionMapper;
        private readonly IInstructionMapper _instructionMapper;

        public SpiderMapper(IPositionMapper positionMapper, IInstructionMapper instructionMapper)
        {
            _positionMapper = positionMapper;
            _instructionMapper = instructionMapper;
        }

        public IEnumerable<Spider> ToDomains(IEnumerable<string> inputs)
        {
            const int spiderRows = 2;
            var rowsMapped = 0;
            var numberOfRows = inputs.Count();
            var spiders = new List<Spider>();

            while (rowsMapped < numberOfRows)
            {
                var spiderInput = inputs.Skip(rowsMapped).Take(spiderRows);
                var spider = ToDomain(spiderInput.First(), spiderInput.Last());
                spiders.Add(spider);
                rowsMapped += spiderRows;

            }

            return spiders;
        }

        private Spider ToDomain(string positionInput, string instructionsInput)
        {
            var positionInputs = positionInput.Split(' ');
            if (IsPositionInputValid(positionInputs) && IsInstructionsInputValid(instructionsInput))
            {
                return new Spider
                {
                    Position = _positionMapper.ToDomain(positionInputs),
                    Instructions = _instructionMapper.ToEnums(instructionsInput)
                };
            }

            throw new SpiderInputException();
        }

        private static bool IsInstructionsInputValid(string instructionsInput)
        {
            // TODO: Add any additional validation that might be required
            return instructionsInput.ToCharArray()
                .All(x => x == InputInstructions.MoveForward || 
                          x == InputInstructions.RotateLeft || 
                          x == InputInstructions.RotateRight);
        }

        private static bool IsPositionInputValid(IReadOnlyList<string> positionInputs)
        {
            // TODO: Add any additional validation that might be required
            const int expectedNumberOfInputs = 3;
            
            return positionInputs.Count == expectedNumberOfInputs &&
                   Enum.IsDefined(typeof(Orientation), positionInputs[2]);
        }
    }
}
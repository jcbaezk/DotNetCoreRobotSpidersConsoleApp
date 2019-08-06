using System.Collections.Generic;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Exceptions;

namespace RobotSpiders.Core.Mappers
{
    public class WallMapper : IWallMapper
    {
        public Wall ToDomain(string input)
        {
            var inputs = input.Split(' ');
            if (IsInputValid(inputs))
            {
                return new Wall
                {
                    HorizontalSize = int.Parse(inputs[0]),
                    VerticalSize = int.Parse(inputs[1])
                };
            }
            
            throw new WallInputException();
        }

        private static bool IsInputValid(IReadOnlyList<string> inputs)
        {
            const int expectedNumberOfInputs = 2;

            return inputs != null &&
                   inputs.Count == expectedNumberOfInputs &&
                   int.TryParse(inputs[0], out var horizontalSize) && 
                   horizontalSize > 0 &&
                   int.TryParse(inputs[1], out var verticalSize) && 
                   verticalSize > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Mappers
{
    public class PositionMapper : IPositionMapper
    {
        public Position ToDomain(IReadOnlyList<string> positionInputs)
        {
            return new Position
            {
                X = GetPositionValue(positionInputs[0]),
                Y = GetPositionValue(positionInputs[1]),
                Orientation = (Orientation)Enum.Parse(typeof(Orientation), positionInputs[2])
            };
        }

        public string ToTextFileString(Position position)
        {
            return $"{position.X} {position.Y} {position.Orientation.ToString()}";
        }

        private static int GetPositionValue(string positionInput)
        {
            return int.TryParse(positionInput, out var horizontalValue) && horizontalValue >= 0 ? horizontalValue : 0;
        }
    }
}
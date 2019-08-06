using System.Collections.Generic;
using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Mappers
{
    public interface IPositionMapper
    {
        Position ToDomain(IReadOnlyList<string> positionInputs);
        string ToTextFileString(Position position);
    }
}
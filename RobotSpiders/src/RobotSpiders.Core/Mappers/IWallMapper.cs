using RobotSpiders.Core.Domain;

namespace RobotSpiders.Core.Mappers
{
    public interface IWallMapper
    {
        Wall ToDomain(string input);
    }
}
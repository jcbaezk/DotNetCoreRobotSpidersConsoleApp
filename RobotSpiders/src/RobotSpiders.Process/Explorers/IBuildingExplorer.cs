using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Explorers
{
    public interface IBuildingExplorer
    {
        void Explore(Building building);
    }
}
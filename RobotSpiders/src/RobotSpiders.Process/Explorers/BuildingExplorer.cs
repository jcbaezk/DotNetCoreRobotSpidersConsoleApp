using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Explorers
{
    public class BuildingExplorer : IBuildingExplorer
    {
        private readonly ISpiderController _spiderController;

        public BuildingExplorer(ISpiderController spiderController)
        {
            _spiderController = spiderController;
        }

        public void Explore(Building building)
        {
            foreach (var spider in building.Spiders)
            {
                _spiderController.Control(spider, building.Wall);
            }
        }
    }
}
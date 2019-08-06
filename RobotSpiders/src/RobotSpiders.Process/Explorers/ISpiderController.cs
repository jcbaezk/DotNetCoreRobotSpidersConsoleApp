using RobotSpiders.Core.Domain;

namespace RobotSpiders.Process.Explorers
{
    public interface ISpiderController
    {
        void Control(Spider spider, Wall wall);
    }
}
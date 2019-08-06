using RobotSpiders.Core.Domain;

namespace RobotSpiders.Exporters.Exporters
{
    public interface IFileOutputWriter
    {
        void Write(Building building, string filePath);
    }
}
using RobotSpiders.Core.Domain;

namespace RobotSpiders.InputReaders.Readers
{
    public interface IFileInputReader
    {
        Building Read(string filePath);
    }
}
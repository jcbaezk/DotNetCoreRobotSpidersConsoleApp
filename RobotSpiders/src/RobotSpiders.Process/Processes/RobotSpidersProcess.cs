using RobotSpiders.Exporters.Exporters;
using RobotSpiders.InputReaders.Readers;
using RobotSpiders.Process.Explorers;

namespace RobotSpiders.Process.Processes
{
    public class RobotSpidersProcess : IRobotSpidersProcess
    {
        private readonly IFileInputReader _fileInputReader;
        private readonly IBuildingExplorer _buildingExplorer;
        private readonly IFileOutputWriter _fileOutputWriter;


        public RobotSpidersProcess(IFileInputReader fileInputReader, IBuildingExplorer buildingExplorer, IFileOutputWriter fileOutputWriter)
        {
            _fileInputReader = fileInputReader;
            _buildingExplorer = buildingExplorer;
            _fileOutputWriter = fileOutputWriter;
        }

        public void Start(string filePath)
        {
            var building = _fileInputReader.Read(filePath);
            _buildingExplorer.Explore(building);
            _fileOutputWriter.Write(building, filePath);
        }
    }
}
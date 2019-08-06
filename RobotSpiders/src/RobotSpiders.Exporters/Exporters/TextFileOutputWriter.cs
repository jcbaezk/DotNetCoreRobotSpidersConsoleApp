using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Mappers;

namespace RobotSpiders.Exporters.Exporters
{
    public class TextFileOutputWriter : IFileOutputWriter
    {
        private readonly IFileSystem _fileSystem;
        private readonly IPositionMapper _positionMapper;
        private const string OutputFileName = "SpiderPositions";
        private const string OutputFileExtension = "txt";

        public TextFileOutputWriter(IFileSystem fileSystem, IPositionMapper positionMapper)
        {
            _fileSystem = fileSystem;
            _positionMapper = positionMapper;
        }
        public void Write(Building building, string filePath)
        {
            var outputPath = $@"{_fileSystem.Path.GetDirectoryName(filePath)}\{OutputFileName}_{DateTime.Now.Ticks}.{OutputFileExtension}";
            var outputLines = new List<string>();

            foreach (var spider in building.Spiders)
            {
                outputLines.Add(_positionMapper.ToTextFileString(spider.Position));
            }

            _fileSystem.File.WriteAllLines(outputPath, outputLines);
        }
    }
}
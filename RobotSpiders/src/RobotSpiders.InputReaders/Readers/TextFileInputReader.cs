using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Exceptions;
using RobotSpiders.Core.Mappers;

namespace RobotSpiders.InputReaders.Readers
{
    public class TextFileInputReader : IFileInputReader
    {
        private readonly IFileSystem _fileSystem;
        private readonly IWallMapper _wallMapper;
        private readonly ISpiderMapper _spiderMapper;

        public TextFileInputReader(IFileSystem fileSystem, IWallMapper wallMapper, ISpiderMapper spiderMapper)
        {
            _fileSystem = fileSystem;
            _wallMapper = wallMapper;
            _spiderMapper = spiderMapper;
        }

        public Building Read(string filePath)
        {
            var lines = _fileSystem.File.ReadAllLines(filePath);
            if (AreLinesValid(lines))
            {
                var wallInputLine = lines.First();
                var spiderInputLines = lines.Skip(1);

                return new Building
                {
                    Wall = _wallMapper.ToDomain(wallInputLine),
                    Spiders = _spiderMapper.ToDomains(spiderInputLines)
                };
            }
            
            throw new InputException();
        }

        private static bool AreLinesValid(IReadOnlyList<string> lines)
        {
            const int minimumNumberOfRows = 3;
            return lines.Count >= minimumNumberOfRows &&
                lines.All(x => !string.IsNullOrWhiteSpace(x)) &&
                HasCorrectNumberOfSpiderLines(lines);
        }

        private static bool HasCorrectNumberOfSpiderLines(IReadOnlyList<string> lines)
        {
            return (lines.Count - 1) % 2 == 0;
        }
    }
}
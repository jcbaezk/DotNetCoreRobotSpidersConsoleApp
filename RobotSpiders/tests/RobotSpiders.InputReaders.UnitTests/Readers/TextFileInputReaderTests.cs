using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using FluentAssertions;
using Moq;
using RobotSpiders.Core.Domain;
using RobotSpiders.Core.Exceptions;
using RobotSpiders.Core.Mappers;
using RobotSpiders.InputReaders.Readers;
using Xunit;

namespace RobotSpiders.InputReaders.UnitTests.Readers
{
    public class TextFileInputReaderTests
    {
        private readonly Mock<IWallMapper> _wallMapper;
        private readonly Mock<ISpiderMapper> _spiderMapper;
        private readonly Fixture _fixture;
        private const string InputFilePath = @"c:\inputfile.txt";

        public TextFileInputReaderTests()
        {
            _wallMapper = new Mock<IWallMapper>();
            _spiderMapper = new Mock<ISpiderMapper>();
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ToDomain_ShouldThrowExceptionGivenInputDoesNotHaveEnoughRows(int invalidNumberOfRows)
        {
            var inputLines = _fixture.CreateMany<string>(invalidNumberOfRows);
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { InputFilePath, new MockFileData(string.Join('\r', inputLines)) }
            });
            var reader = new TextFileInputReader(fileSystem, _wallMapper.Object, _spiderMapper.Object);

            Action act = () => reader.Read(InputFilePath);

            act.Should().Throw<InputException>();
        }

        [Theory]
        [InlineData("\rrow2\rrow3")]
        [InlineData("row1\r\rrow3")]
        [InlineData("row1\rrow2\r")]
        public void ToDomain_ShouldThrowExceptionGivenInputHasEmptyRows(string invalidContent)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { InputFilePath, new MockFileData(invalidContent) }
            });
            var reader = new TextFileInputReader(fileSystem, _wallMapper.Object, _spiderMapper.Object);

            Action act = () => reader.Read(InputFilePath);

            act.Should().Throw<InputException>();
        }

        [Fact]
        public void ToDomain_ShouldThrowExceptionGivenInputDoesNotHaveCorrectNumberOfRows()
        {
            const int wallInputRows = 1;
            var invalidNumberOfSpiderRows = GetEvenNumber() + 1;
            var inputLines = _fixture.CreateMany<string>(invalidNumberOfSpiderRows + wallInputRows);
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { InputFilePath, new MockFileData(string.Join('\r', inputLines)) }
            });
            var reader = new TextFileInputReader(fileSystem, _wallMapper.Object, _spiderMapper.Object);

            Action act = () => reader.Read(InputFilePath);

            act.Should().Throw<InputException>();
        }

        [Fact]
        public void Read_ShouldReturnBuildingObjectGivenValidInputData()
        {
            var expectedBuilding = _fixture.Create<Building>();
            var inputLines = _fixture.CreateMany<string>();
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { InputFilePath, new MockFileData(string.Join('\r', inputLines)) }
            });
            _wallMapper.Setup(x => x.ToDomain(It.IsAny<string>())).Returns(expectedBuilding.Wall);
            _spiderMapper.Setup(x => x.ToDomains(It.IsAny<IEnumerable<string>>())).Returns(expectedBuilding.Spiders);
            var reader = new TextFileInputReader(fileSystem, _wallMapper.Object, _spiderMapper.Object);

            var result = reader.Read(InputFilePath);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedBuilding);
        }

        private static int GetEvenNumber()
        {
            return new Random().Next(0, 60) * 2;
        }
    }
}
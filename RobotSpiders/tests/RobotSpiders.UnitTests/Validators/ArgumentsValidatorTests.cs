using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using RobotSpiders.Validators;
using Xunit;

namespace RobotSpiders.UnitTests.Validators
{
    public class ArgumentsValidatorTests
    {
        private const string ExistentFile = @"c:\theinputfile.txt";
        private readonly ArgumentsValidator _validator;

        public ArgumentsValidatorTests()
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {ExistentFile, new MockFileData("the spider input")}
            });

            _validator = new ArgumentsValidator(fileSystem);
        }

        [Fact]
        public void IsValid_ShouldReturnFalseGivenNullArguments()
        {
            string[] nullArguments = null;

            var result = _validator.IsValid(nullArguments);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldReturnFalseGivenEmptyArguments()
        {
            string[] nullArguments = { };

            var result = _validator.IsValid(nullArguments);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldReturnFalseGivenMoreThanOneArgument()
        {
            string[] arguments = { "first", "second" };

            var result = _validator.IsValid(arguments);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void IsValid_ShouldReturnFalseGivenInvalidArgument(string invalidArgument)
        {
            string[] arguments = { invalidArgument };

            var result = _validator.IsValid(arguments);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldReturnFalseGivenTheFilePathInTheArgumentDoesNotExist()
        {
            string[] arguments = { @"c:\thisdoesnotexist.txt" };

            var result = _validator.IsValid(arguments);

            result.Should().BeFalse();
        }

        [Fact]
        public void IsValid_ShouldReturnTrueGivenOneArgumentThatIsNotNullOrEmpty()
        {
            string[] arguments = { ExistentFile };

            var result = _validator.IsValid(arguments);

            result.Should().BeTrue();
        }
    }
}
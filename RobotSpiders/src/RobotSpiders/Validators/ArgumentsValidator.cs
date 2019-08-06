using System.IO.Abstractions;

namespace RobotSpiders.Validators
{
    public class ArgumentsValidator : IArgumentsValidator
    {
        private readonly IFileSystem _fileSystem;

        public ArgumentsValidator(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool IsValid(string[] arguments)
        {
            return arguments != null &&
                   arguments.Length == 1 &&
                   !string.IsNullOrWhiteSpace(arguments[0]) &&
                   PathExists(arguments[0]);
        }

        private bool PathExists(string argument)
        {
            return _fileSystem.File.Exists(argument);
        }
    }
}
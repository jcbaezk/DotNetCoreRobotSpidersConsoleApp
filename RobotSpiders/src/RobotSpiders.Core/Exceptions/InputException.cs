using System;

namespace RobotSpiders.Core.Exceptions
{
    public class InputException : Exception
    {
        public InputException() : base("Input data does not have the expected structure")
        {
        }
    }
}
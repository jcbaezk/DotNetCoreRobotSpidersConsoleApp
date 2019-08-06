using System;

namespace RobotSpiders.Core.Exceptions
{
    public class WallInputException : Exception
    {
        public WallInputException() : base("Wall input is not valid")
        {
        }
    }
}
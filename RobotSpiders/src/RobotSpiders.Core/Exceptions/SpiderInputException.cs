using System;

namespace RobotSpiders.Core.Exceptions
{
    public class SpiderInputException : Exception
    {
        public SpiderInputException() : base("Spider input is not valid")
        {
        }
    }
}
using System;

namespace BusinessLogic.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
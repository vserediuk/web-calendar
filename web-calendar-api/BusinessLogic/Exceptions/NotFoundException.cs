using System;

namespace BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
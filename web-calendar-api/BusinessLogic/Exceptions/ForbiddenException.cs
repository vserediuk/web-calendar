using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}

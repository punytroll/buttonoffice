using System;

namespace ButtonOffice.UnitTest
{
    internal class ExceptionMissingException : Exception
    {
        public ExceptionMissingException(String ExceptionName)
            : base(ExceptionName + " should have been thrown.")
        {
        }
    }
}

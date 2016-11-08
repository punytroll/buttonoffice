using System;

namespace ButtonOffice.UnitTest
{
    internal class AssertException : Exception
    {
        private readonly AssertMessages _AssertMessage;

        public AssertMessages AssertMessage
        {
            get
            {
                return _AssertMessage;
            }
        }

        public AssertException(AssertMessages AssertMessage)
        {
            _AssertMessage = AssertMessage;
        }
    }
}

namespace ButtonOffice.UnitTest
{
    internal class AssertException : System.Exception
    {
        private ButtonOffice.AssertMessages _AssertMessage;

        public ButtonOffice.AssertMessages AssertMessage
        {
            get
            {
                return _AssertMessage;
            }
        }

        public AssertException(ButtonOffice.AssertMessages AssertMessage)
        {
            _AssertMessage = AssertMessage;
        }
    }
}

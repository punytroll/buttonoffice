namespace ButtonOffice.UnitTest
{
    internal class TraceListener : System.Diagnostics.TraceListener
    {
        private System.Collections.Generic.List<ButtonOffice.AssertMessages> _Assertions;

        public TraceListener()
        {
            _Assertions = new System.Collections.Generic.List<ButtonOffice.AssertMessages>();
        }

        public override void Fail(System.String Message, System.String DetailMessage)
        {
            _Assertions.Add((ButtonOffice.AssertMessages)(System.Enum.Parse(typeof(ButtonOffice.AssertMessages), Message)));
        }

        public override void Write(System.String Message)
        {
        }

        public override void WriteLine(System.String Message)
        {
        }

        public System.Boolean HasNoAssertions()
        {
            return _Assertions.Count == 0;
        }
    }
}

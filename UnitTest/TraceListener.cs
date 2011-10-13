namespace ButtonOffice.UnitTest
{
    internal class TraceListener : System.Diagnostics.TraceListener
    {
        public TraceListener()
        {
        }

        public override void Fail(System.String Message, System.String DetailMessage)
        {
            throw new ButtonOffice.UnitTest.AssertException((ButtonOffice.AssertMessages)(System.Enum.Parse(typeof(ButtonOffice.AssertMessages), Message)));
        }

        public override void Write(System.String Message)
        {
        }

        public override void WriteLine(System.String Message)
        {
        }
    }
}

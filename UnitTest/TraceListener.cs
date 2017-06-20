using System;

namespace ButtonOffice.UnitTest
{
    internal class TraceListener : System.Diagnostics.TraceListener
    {
        public override void Fail(String Message, String DetailMessage)
        {
            if(String.IsNullOrEmpty(Message) == false)
            {
                throw new AssertException((AssertMessages)(Enum.Parse(typeof(AssertMessages), Message)));
            }
        }

        public override void Write(String Message)
        {
        }

        public override void WriteLine(String Message)
        {
        }
    }
}

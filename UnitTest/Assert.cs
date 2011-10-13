namespace ButtonOffice.UnitTest
{
    internal class Assert
    {
        internal static void AreEqual<Type>(Type First, Type Second) where Type : System.IComparable
        {
            System.Diagnostics.Debug.Assert(First.CompareTo(Second) == 0);
        }

        internal static void NoAssertion(ButtonOffice.UnitTest.Action Action)
        {
            System.Diagnostics.TraceListener DefaultTraceListenern = System.Diagnostics.Debug.Listeners[0];

            System.Diagnostics.Debug.Listeners.Clear();

            ButtonOffice.UnitTest.TraceListener TraceListener = new ButtonOffice.UnitTest.TraceListener();

            System.Diagnostics.Debug.Listeners.Add(TraceListener);
            try
            {
                Action();
            }
            catch(ButtonOffice.UnitTest.AssertException)
            {
                System.Diagnostics.Debug.Assert(false);
            }
            System.Diagnostics.Debug.Listeners.Remove(TraceListener);
            System.Diagnostics.Debug.Listeners.Add(DefaultTraceListenern);
        }

        internal static void Assertion(ButtonOffice.AssertMessages AssertMessage, ButtonOffice.UnitTest.Action Action)
        {
            System.Diagnostics.TraceListener DefaultTraceListenern = System.Diagnostics.Debug.Listeners[0];

            System.Diagnostics.Debug.Listeners.Clear();

            ButtonOffice.UnitTest.TraceListener TraceListener = new ButtonOffice.UnitTest.TraceListener();

            System.Diagnostics.Debug.Listeners.Add(TraceListener);
            try
            {
                Action();
                System.Diagnostics.Debug.Assert(false);
            }
            catch(ButtonOffice.UnitTest.AssertException Exception)
            {
                if(Exception.AssertMessage != AssertMessage)
                {
                    System.Diagnostics.Debug.Assert(false);
                }
            }
            System.Diagnostics.Debug.Listeners.Remove(TraceListener);
            System.Diagnostics.Debug.Listeners.Add(DefaultTraceListenern);

        }
    }
}

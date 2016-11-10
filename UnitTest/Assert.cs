using System;
using System.Diagnostics;

namespace ButtonOffice.UnitTest
{
    internal class Assert
    {
        internal static void AreEqual<Type>(Type First, Type Second) where Type : IComparable
        {
            Debug.Assert(First.CompareTo(Second) == 0);
        }

        internal static void NoAssertion(Action Action)
        {
            var DefaultTraceListenern = Debug.Listeners[0];

            Debug.Listeners.Clear();

            var TraceListener = new TraceListener();

            Debug.Listeners.Add(TraceListener);
            try
            {
                Action();
            }
            catch(AssertException)
            {
                Debug.Assert(false);
            }
            Debug.Listeners.Remove(TraceListener);
            Debug.Listeners.Add(DefaultTraceListenern);
        }

        internal static void Assertion(AssertMessages AssertMessage, Action Action)
        {
            var DefaultTraceListenern = Debug.Listeners[0];

            Debug.Listeners.Clear();

            var TraceListener = new TraceListener();

            Debug.Listeners.Add(TraceListener);
            try
            {
                Action();
                Debug.Assert(false);
            }
            catch(AssertException Exception)
            {
                if(Exception.AssertMessage != AssertMessage)
                {
                    Debug.Assert(false);
                }
            }
            Debug.Listeners.Remove(TraceListener);
            Debug.Listeners.Add(DefaultTraceListenern);

        }
    }
}

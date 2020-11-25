using System;
using System.Diagnostics;

namespace ButtonOffice
{
    internal static class Assert
    {
        [Conditional("ASSERT")]
        public static void True(Boolean Value)
        {
            if(Value == false)
            {
                throw new Exception("Assertion failed.");
            }
        }
    }
}

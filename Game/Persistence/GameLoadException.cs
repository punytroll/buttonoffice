using System;

namespace ButtonOffice
{
    public class GameLoadException : Exception
    {
        public GameLoadException(Exception Exception) :
            base(Exception.Message, Exception)
        {
        }
    }
}

namespace System
{
    public class Pair<FirstType, SecondType>
    {
        private FirstType _First;
        private SecondType _Second;

        public FirstType First
        {
            get
            {
                return _First;
            }
            set
            {
                _First = value;
            }
        }

        public SecondType Second
        {
            get
            {
                return _Second;
            }
            set
            {
                _Second = value;
            }
        }

        public Pair(FirstType First, SecondType Second)
        {
            _First = First;
            _Second = Second;
        }
    }
}

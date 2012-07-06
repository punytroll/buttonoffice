namespace ButtonOffice
{
    internal class RandomNumberGenerator
    {
        private static readonly System.Random _Random;

        static RandomNumberGenerator()
        {
            _Random = new System.Random();
        }

        public static System.Boolean GetBoolean()
        {
            return _Random.NextDouble() < 0.5;
        }

        public static System.Boolean GetBoolean(System.Double TruePercentage)
        {
            return _Random.NextDouble() < TruePercentage;
        }

        public static System.Single GetSingle()
        {
            return _Random.NextDouble().ToSingle();
        }

        public static System.Single GetSingle(System.Single Mean, System.Single Spread)
        {
            return Mean + _Random.NextDouble().ToSingle() * Spread - Spread / 2.0f;
        }

        public static System.Single GetSingleFromExponentialDistribution(System.Single MeanInterval)
        {
            return MeanInterval * -(System.Math.Log(_Random.NextDouble()).ToSingle());
        }

        public static System.UInt32 GetUInt32(System.UInt32 Mean, System.UInt32 Spread)
        {
            return Mean + _Random.Next().ToUInt32() % Spread - Spread / 2;
        }
    }
}

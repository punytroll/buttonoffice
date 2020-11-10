using System;

namespace ButtonOffice
{
    internal class RandomNumberGenerator
    {
        private static readonly Random _Random;
        
        static RandomNumberGenerator()
        {
            _Random = new Random();
        }
        
        public static Boolean GetBoolean()
        {
            return _Random.NextDouble() < 0.5;
        }
        
        public static Boolean GetBoolean(Double TruePercentage)
        {
            return _Random.NextDouble() < TruePercentage;
        }
        
        public static Double GetDouble(Double Mean, Double Spread)
        {
            return Mean + _Random.NextDouble() * Spread - Spread / 2.0;
        }
        
        public static Double GetDoubleFromExponentialDistribution(Double MeanInterval)
        {
            return MeanInterval * -(Math.Log(_Random.NextDouble()));
        }
        
        public static Single GetSingle(Single Mean, Single Spread)
        {
            return Mean + _Random.NextDouble().ToSingle() * Spread - Spread / 2.0f;
        }
        
        public static UInt32 GetUInt32(UInt32 Mean, UInt32 Spread)
        {
            return Mean + _Random.Next().ToUInt32() % Spread - Spread / 2;
        }
    }
}

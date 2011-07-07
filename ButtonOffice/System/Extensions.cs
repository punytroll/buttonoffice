public static partial class Extensions
{
    public static System.Single ToSingle(this System.Int32 Int32)
    {
        return System.Convert.ToSingle(Int32);
    }

    public static System.UInt32 ToUInt32(this System.Int32 Int32)
    {
        return System.Convert.ToUInt32(Int32);
    }

    public static System.Int32 GetIntegerAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Truncate(Single));
    }

    public static System.Int32 ToInt32(this System.UInt32 UInt32)
    {
        return System.Convert.ToInt32(UInt32);
    }

    public static System.Int32 ToInt32(this System.UInt64 UInt64)
    {
        return System.Convert.ToInt32(UInt64);
    }

    public static System.Single ToSingle(this System.Double Double)
    {
        return System.Convert.ToSingle(Double);
    }

    public static System.Single NextSingle(this System.Random Random)
    {
        return System.Convert.ToSingle(Random.NextDouble());
    }

    public static System.Single NextSingle(this System.Random Random, System.Single Mean, System.Single Spread)
    {
        return System.Convert.ToSingle(Mean + Random.NextDouble() * Spread - Spread / 2.0f);
    }

    public static System.Int32 NextInt32(this System.Random Random, System.Int32 Mean, System.Int32 Spread)
    {
        return Mean + Random.Next() % Spread - Spread / 2;
    }

    public static System.UInt32 NextUInt32(this System.Random Random, System.UInt32 Mean, System.UInt32 Spread)
    {
        return Mean + Random.Next().ToUInt32() % Spread - Spread / 2;
    }

    public static System.Double GetDoubleFromExponentialDistribution(this System.Random Random, System.Double MeanInterval)
    {
        return MeanInterval * -System.Math.Log(Random.NextDouble());
    }

    public static System.Single GetSingleFromExponentialDistribution(this System.Random Random, System.Single MeanInterval)
    {
        return MeanInterval * -System.Convert.ToSingle(System.Math.Log(Random.NextDouble()));
    }

    public static System.UInt64 GetUInt64FromExponentialDistribution(this System.Random Random, System.UInt64 MeanInterval)
    {
        return MeanInterval * System.Convert.ToUInt64(-System.Math.Log(Random.NextDouble()));
    }
}

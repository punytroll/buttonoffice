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

    public static System.Single GetInteger(this System.Single Single)
    {
        return System.Convert.ToSingle(System.Math.Truncate(System.Convert.ToDouble(Single)));
    }

    public static System.Drawing.PointF GetTruncated(this System.Drawing.PointF Point)
    {
        return new System.Drawing.PointF(System.Convert.ToSingle(System.Math.Truncate(System.Convert.ToDouble(Point.X))), System.Convert.ToSingle(System.Math.Truncate(System.Convert.ToDouble(Point.Y))));
    }
}

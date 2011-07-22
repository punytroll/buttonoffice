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

    public static System.Int64 ToInt64(this System.Int32 Int32)
    {
        return System.Convert.ToInt64(Int32);
    }

    public static System.Int32 GetTruncatedAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Truncate(Single));
    }

    public static System.Int32 GetFlooredAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Floor(System.Convert.ToDouble(Single)));
    }

    public static System.Single GetFloored(this System.Single Single)
    {
        return System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Single)));
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

    public static System.Drawing.PointF GetFloored(this System.Drawing.PointF Point)
    {
        return new System.Drawing.PointF(System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Point.X))), System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Point.Y))));
    }

    public static System.Single GetDistanceSquared(this System.Drawing.PointF Point, System.Drawing.PointF OtherPoint)
    {
        System.Single DeltaX = OtherPoint.X - Point.X;
        System.Single DeltaY = OtherPoint.Y - Point.Y;

        return DeltaX * DeltaX + DeltaY * DeltaY;
    }

    public static Type GetFirst<Type>(this System.Collections.Generic.List<Type> List)
    {
        return List[0];
    }

    public static Type GetLast<Type>(this System.Collections.Generic.List<Type> List)
    {
        return List[List.Count - 1];
    }

    public static Type PopFirst<Type>(this System.Collections.Generic.List<Type> List)
    {
        Type Result = List[0];

        List.RemoveAt(0);

        return Result;
    }
}

using System;
using System.Drawing;

public static class Extensions
{
    /// <summary>
    /// Converts the Int32 value to a Single value.
    /// </summary>
    public static Single ToSingle(this Int32 Int32)
    {
        return Convert.ToSingle(Int32);
    }

    /// <summary>
    /// Converts the Int32 value to an UInt32 value.
    /// </summary>
    public static UInt32 ToUInt32(this Int32 Int32)
    {
        return Convert.ToUInt32(Int32);
    }

    /// <summary>
    /// Converts the Int32 value to an Int64 value.
    /// </summary>
    public static System.Int64 ToInt64(this Int32 Int32)
    {
        return Convert.ToInt64(Int32);
    }

    /// <summary>
    /// Rounds the Single value towards 0 and converts it to an Int32 value.
    /// </summary>
    public static Int32 GetTruncatedAsInt32(this Single Single)
    {
        return Convert.ToInt32(Math.Truncate(Single));
    }

    /// <summary>
    /// Rounds the Single value towards 0 and converts it to an Int32 value.
    /// </summary>
    public static Int32 GetTruncatedAsInt32(this Double Double)
    {
        return Convert.ToInt32(Math.Truncate(Double));
    }

    /// <summary>
    /// Rounds the Single value towards −∞ and converts it to an Int32 value.
    /// </summary>
    public static Int32 GetFlooredAsInt32(this Single Single)
    {
        return Convert.ToInt32(Math.Floor(Convert.ToDouble(Single)));
    }

    /// <summary>
    /// Rounds the Single value towards -∞.
    /// </summary>
    public static Single GetFloored(this Single Single)
    {
        return Convert.ToSingle(Math.Floor(Convert.ToDouble(Single)));
    }

    /// <summary>
    /// Converts the UInt64 value to an Int32 value.
    /// </summary>
    public static Int32 ToInt32(this UInt64 UInt64)
    {
        return Convert.ToInt32(UInt64);
    }

    /// <summary>
    /// Converts the Double value to a Single value.
    /// </summary>
    public static Single ToSingle(this Double Double)
    {
        return Convert.ToSingle(Double);
    }

    /// <summary>
    /// Rounds both components of the PointF value towards -∞.
    /// </summary>
    public static PointF GetFloored(this PointF Point)
    {
        return new PointF(Convert.ToSingle(Math.Floor(Convert.ToDouble(Point.X))), Convert.ToSingle(Math.Floor(Convert.ToDouble(Point.Y))));
    }

    /// <summary>
    /// Calculates the squared distance between two PointF values.
    /// </summary>
    public static Single GetDistanceSquared(this System.Drawing.PointF This, System.Drawing.PointF That)
    {
        var DeltaX = That.X - This.X;
        var DeltaY = That.Y - This.Y;

        return DeltaX * DeltaX + DeltaY * DeltaY;
    }

    /// <summary>
    /// Retrieves the first element from a list.
    /// </summary>
    public static Type GetFirst<Type>(this System.Collections.Generic.List<Type> List)
    {
        return List[0];
    }

    /// <summary>
    /// Retrieves the last element from a list.
    /// </summary>
    public static Type GetLast<Type>(this System.Collections.Generic.List<Type> List)
    {
        return List[List.Count - 1];
    }

    /// <summary>
    /// Retrieves and removes the first element from a list.
    /// </summary>
    public static Type PopFirst<Type>(this System.Collections.Generic.List<Type> List)
    {
        var Result = List[0];

        List.RemoveAt(0);

        return Result;
    }

    /// <summary>
    /// Return a PointF value with the mid point coordinates of the rectangle.
    /// </summary>
    /// <param name="Rectangle"></param>
    /// <returns></returns>
    public static System.Drawing.PointF GetMidPoint(this System.Drawing.RectangleF Rectangle)
    {
        return new System.Drawing.PointF(Rectangle.X + Rectangle.Width / 2.0f, Rectangle.Y + Rectangle.Height / 2.0f);
    }

    public static System.Collections.Generic.List<Type> GetShuffledCopy<Type>(this System.Collections.Generic.IList<Type> List)
    {
        var Result = new System.Collections.Generic.List<Type>(List);
        var RandomNumberGenerator = new System.Random();
        var N = Result.Count;

        while(N > 1)
        {
            N--;

            var K = RandomNumberGenerator.Next(N + 1);
            var Value = Result[K];

            Result[K] = Result[N];
            Result[N] = Value;
        }

        return Result;
    }
}

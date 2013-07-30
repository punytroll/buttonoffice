public static partial class Extensions
{
    /// <summary>
    /// Converts the Int32 value to a Single value.
    /// </summary>
    public static System.Single ToSingle(this System.Int32 Int32)
    {
        return System.Convert.ToSingle(Int32);
    }

    /// <summary>
    /// Converts the Int32 value to an UInt32 value.
    /// </summary>
    public static System.UInt32 ToUInt32(this System.Int32 Int32)
    {
        return System.Convert.ToUInt32(Int32);
    }

    /// <summary>
    /// Converts the Int32 value to an Int64 value.
    /// </summary>
    public static System.Int64 ToInt64(this System.Int32 Int32)
    {
        return System.Convert.ToInt64(Int32);
    }

    /// <summary>
    /// Rounds the Single value towards 0 and converts it to an Int32 value.
    /// </summary>
    public static System.Int32 GetTruncatedAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Truncate(Single));
    }

    /// <summary>
    /// Rounds the Single value towards −∞ and converts it to an Int32 value.
    /// </summary>
    public static System.Int32 GetFlooredAsInt32(this System.Single Single)
    {
        return System.Convert.ToInt32(System.Math.Floor(System.Convert.ToDouble(Single)));
    }

    /// <summary>
    /// Rounds the Single value towards -∞.
    /// </summary>
    public static System.Single GetFloored(this System.Single Single)
    {
        return System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Single)));
    }

    /// <summary>
    /// Converts the UInt64 value to an Int32 value.
    /// </summary>
    public static System.Int32 ToInt32(this System.UInt64 UInt64)
    {
        return System.Convert.ToInt32(UInt64);
    }

    /// <summary>
    /// Converts the Double value to a Single value.
    /// </summary>
    public static System.Single ToSingle(this System.Double Double)
    {
        return System.Convert.ToSingle(Double);
    }

    /// <summary>
    /// Rounds both components of the PointF value towards -∞.
    /// </summary>
    public static System.Drawing.PointF GetFloored(this System.Drawing.PointF Point)
    {
        return new System.Drawing.PointF(System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Point.X))), System.Convert.ToSingle(System.Math.Floor(System.Convert.ToDouble(Point.Y))));
    }

    /// <summary>
    /// Calculates the squared distance between two PointF values.
    /// </summary>
    public static System.Single GetDistanceSquared(this System.Drawing.PointF This, System.Drawing.PointF That)
    {
        System.Single DeltaX = That.X - This.X;
        System.Single DeltaY = That.Y - This.Y;

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
        Type Result = List[0];

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

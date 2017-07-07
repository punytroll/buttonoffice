using System.Drawing;

namespace ButtonOffice
{
    public static class Extensions
    {
        /// <summary>
        /// Return a Vector2 value with the mid point coordinates of the rectangle.
        /// </summary>
        public static Vector2 GetMidPointDouble(this RectangleF Rectangle)
        {
            return new Vector2(Rectangle.X + Rectangle.Width / 2.0, Rectangle.Y + Rectangle.Height / 2.0);
        }
    }
}

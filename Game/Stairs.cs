using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Stairs : PersistentObject
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private RectangleF _Rectangle;

        public Stairs()
        {
            _BackgroundColor = Data.StairsBackgroundColor;
            _BorderColor = Data.StairsBorderColor;
        }

        public Color GetBackgroundColor()
        {
            return _BackgroundColor;
        }

        public Color GetBorderColor()
        {
            return _BorderColor;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public PointF GetMidLocation()
        {
            return _Rectangle.GetMidPoint();
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public Single GetRight()
        {
            return _Rectangle.Right;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public Single GetX()
        {
            return _Rectangle.X;
        }

        public Single GetY()
        {
            return _Rectangle.Y;
        }

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetRectangle(RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("rectangle", _Rectangle);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
        }
    }
}

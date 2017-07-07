using System;
using System.Drawing;

namespace ButtonOffice
{
    public abstract class Building : PersistentObject
    {
        protected Color _BackgroundColor;
        protected Color _BorderColor;
        protected RectangleF _Rectangle;

        public Color BackgroundColor => _BackgroundColor;

        public Color BorderColor => _BorderColor;

        public Boolean Contains(Vector2 Location)
        {
            return (Location.X >= _Rectangle.X) && (Location.X <= _Rectangle.X + _Rectangle.Width) && (Location.Y >= _Rectangle.Y) && (Location.Y <= _Rectangle.Y + _Rectangle.Height);
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public Vector2 GetMidLocation()
        {
            return _Rectangle.GetMidPointDouble();
        }

        public RectangleF GetVisualRectangle()
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
            _UpdateInterior();
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
            _UpdateInterior();
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
            _UpdateInterior();
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
            _UpdateInterior();
        }

        protected virtual void _UpdateInterior()
        {
        }

        public virtual void Move(Game Game, Double DeltaGameMinutes)
        {
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

using System;
using System.Drawing;

namespace ButtonOffice
{
    internal class EntityPrototype
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Boolean _HasLocation;
        private RectangleF _Rectangle;
        private Type _Type;

        public Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
            set
            {
                _BackgroundColor = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
            }
        }

        public PointF Location
        {
            get
            {
                return _Rectangle.Location;
            }
        }

        public RectangleF Rectangle
        {
            get
            {
                return _Rectangle;
            }
        }

        public Type Type
        {
            get
            {
                return _Type;
            }
        }

        public EntityPrototype(Type Type)
        {
            _HasLocation = false;
            _Type = Type;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public void SetLocationFromGamingLocation(PointF Location)
        {
            _Rectangle.Location = new PointF(Location.X - (_Rectangle.Width / 2.0f).GetFloored(), Location.Y - (_Rectangle.Height / 2.0f).GetFloored());
            _HasLocation = true;
        }

        public Boolean HasLocation()
        {
            return _HasLocation;
        }

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }
    }
}

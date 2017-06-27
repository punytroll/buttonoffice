using System;
using System.Drawing;

namespace ButtonOffice
{
    internal enum EntityType
    {
        Accountant,
        Bathroom,
        Cat,
        ITTech,
        Janitor,
        Office,
        Stairs,
        Worker
    }

    internal class EntityPrototype
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Boolean _HasLocation;
        private RectangleF _Rectangle;
        private Boolean _SnapToBlocksHorizontally;
        private readonly EntityType _EntityType;

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

        public PointF Location => _Rectangle.Location;

        public RectangleF Rectangle => _Rectangle;

        public Boolean SnapToBlocksHorizontally
        {
            set
            {
                _SnapToBlocksHorizontally = value;
            }
        }

        public EntityType EntityType => _EntityType;

        public EntityPrototype(EntityType EntityType)
        {
            _HasLocation = false;
            _SnapToBlocksHorizontally = true;
            _EntityType = EntityType;
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
            if(_SnapToBlocksHorizontally == true)
            {
                _Rectangle.Location = new PointF((Location.X - (_Rectangle.Width / 2.0f)).GetRounded(), (Location.Y - _Rectangle.Height / 2.0f).GetRounded());
            }
            else
            {
                _Rectangle.Location = new PointF(Location.X - (_Rectangle.Width / 2.0f), (Location.Y - _Rectangle.Height / 2.0f).GetRounded());
            }
            _HasLocation = true;
        }

        public Boolean HasLocation()
        {
            return _HasLocation;
        }

        public void SetHeight(Double Height)
        {
            _Rectangle.Height = Convert.ToSingle(Height);
        }

        public void SetWidth(Double Width)
        {
            _Rectangle.Width = Convert.ToSingle(Width);
        }
    }
}

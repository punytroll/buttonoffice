using System;

namespace ButtonOffice
{
    public class Rectangle
    {
        private Double _Height;
        private Double _Width;
        private Double _Left;
        private Double _Bottom;

        public Double Bottom
        {
            get
            {
                return _Bottom;
            }
            set
            {
                _Bottom = value;
            }
        }

        public Double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }

        public Double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
            }
        }

        public Vector2 MidLocation => new Vector2(_Left + _Width / 2.0, _Bottom + _Height / 2.0);

        public Double Right => _Left + _Width;

        public Double Top => _Bottom + Height;

        public Double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }

        public Rectangle()
        {
        }

        public Rectangle(Double Left, Double Bottom, Double Width, Double Height)
        {
            _Left = Left;
            _Bottom = Bottom;
            _Width = Width;
            _Height = Height;
        }

        public Boolean Contains(Vector2 Location)
        {
            return (Location.X >= Left) && (Location.X <= Right) && (Location.Y >= Bottom) && (Location.Y <= Top);
        }
    }
}

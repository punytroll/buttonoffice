using System;
using System.Drawing;

namespace ButtonOffice
{
    internal enum DrawType
    {
        Rectangle,
        Ellipse
    }
    
    internal class EntityPrototype
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Func<RectangleF, Boolean> _GameFunction;
        private Boolean _HasLocation;
        private RectangleF _Rectangle;
        private Boolean _SnapToBlocksHorizontally;
        private DrawType _DrawType;
        
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
        
        public DrawType DrawType
        {
            get
            {
                return _DrawType;
            }
            set
            {
                _DrawType = value;
            }
        }
        
        public EntityPrototype()
        {
            _HasLocation = false;
            _SnapToBlocksHorizontally = true;
            _DrawType = DrawType.Rectangle;
        }
        
        public Boolean CallGameFunction()
        {
            return _GameFunction(_Rectangle);
        }
        
        public void SetGameFunction(Func<RectangleF, Boolean> GameFunction)
        {
            _GameFunction = GameFunction;
        }
        
        public Single GetHeight()
        {
            return _Rectangle.Height;
        }
        
        public Single GetWidth()
        {
            return _Rectangle.Width;
        }
        
        public void SetLocationFromGamingLocation(Vector2 Location)
        {
            if(_SnapToBlocksHorizontally == true)
            {
                _Rectangle.X = (Location.X.ToSingle() - (_Rectangle.Width / 2.0f)).GetRounded();
            }
            else
            {
                _Rectangle.X = Location.X.ToSingle() - (_Rectangle.Width / 2.0f);
            }
            _Rectangle.Y = (Location.Y.ToSingle() - _Rectangle.Height / 2.0f).GetRounded();
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

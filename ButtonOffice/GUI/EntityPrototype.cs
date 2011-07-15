namespace ButtonOffice
{
    internal class EntityPrototype
    {
        private System.Drawing.Color _BackgroundColor;
        private System.Drawing.Color _BorderColor;
        private System.Boolean _HasLocation;
        private System.Drawing.RectangleF _Rectangle;
        private ButtonOffice.Type _Type;

        public System.Drawing.Color BackgroundColor
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

        public System.Drawing.Color BorderColor
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

        public System.Drawing.PointF Location
        {
            get
            {
                return _Rectangle.Location;
            }
        }

        public System.Drawing.RectangleF Rectangle
        {
            get
            {
                return _Rectangle;
            }
        }

        public ButtonOffice.Type Type
        {
            get
            {
                return _Type;
            }
        }

        public EntityPrototype(ButtonOffice.Type Type)
        {
            _HasLocation = false;
            _Type = Type;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public void SetLocationFromGamingLocation(System.Drawing.PointF Location)
        {
            _Rectangle.Location = new System.Drawing.PointF(Location.X - (_Rectangle.Width / 2.0f).GetFloored(), Location.Y - (_Rectangle.Height / 2.0f).GetFloored());
            _HasLocation = true;
        }

        public System.Boolean HasLocation()
        {
            return _HasLocation;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }
    }
}

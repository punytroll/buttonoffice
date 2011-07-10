namespace ButtonOffice
{
    internal class Desk
    {
        private ButtonOffice.Janitor _Janitor;
        private System.Single _MinutesUntilComputerBroken;
        private System.Drawing.RectangleF _Rectangle ;
        private ButtonOffice.Office _Office;
        private ButtonOffice.Person _Person;
        private System.Single _TrashLevel;

        public ButtonOffice.Janitor Janitor
        {
            get
            {
                return _Janitor;
            }
            set
            {
                _Janitor = value;
            }
        }

        public ButtonOffice.Office Office
        {
            get
            {
                return _Office;
            }
            set
            {
                _Office = value;
            }
        }

        public ButtonOffice.Person Person
        {
            get
            {
                return _Person;
            }
            set
            {
                _Person = value;
            }
        }

        public System.Single TrashLevel
        {
            get
            {
                return _TrashLevel;
            }
            set
            {
                _TrashLevel = value;
            }
        }

        public Desk()
        {
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return new System.Drawing.PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
        }

        public System.Single GetMinutesUntilComputerBroken()
        {
            return _MinutesUntilComputerBroken;
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.Single GetX()
        {
            return _Rectangle.X;
        }

        public System.Single GetY()
        {
            return _Rectangle.Y;
        }

        public System.Boolean IsComputerBroken()
        {
            return _MinutesUntilComputerBroken < 0.0f;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetMinutesUntilComputerBroken(System.Single MinutesUntilComputerBroken)
        {
            _MinutesUntilComputerBroken = MinutesUntilComputerBroken;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
        }
    }
}

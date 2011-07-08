namespace ButtonOffice
{
    internal class Person
    {
        private ButtonOffice.ActionState _ActionState;
        private System.Single _AnimationFraction;
        private ButtonOffice.AnimationState _AnimationState;
        private System.UInt64 _ArrivesAtMinute;
        private System.UInt64 _ArrivesAtDayMinute;
        private System.Drawing.Color _BackgroundColor;
        private System.Drawing.Color _BorderColor;
        private ButtonOffice.Desk _Desk;
        private System.UInt64 _LeavesAtMinute;
        private ButtonOffice.LivingSide _LivingSide;
        private System.Drawing.RectangleF _Rectangle;
        private System.String _Name;
        private System.UInt64 _Wage;
        private System.Drawing.PointF _WalkTo;
        private System.UInt64 _WorkMinutes;
        private ButtonOffice.Type _Type;

        public ButtonOffice.ActionState ActionState
        {
            get
            {
                return _ActionState;
            }
            set
            {
                _ActionState = value;
            }
        }

        public System.Single AnimationFraction
        {
            get
            {
                return _AnimationFraction;
            }
            set
            {
                _AnimationFraction = value;
            }
        }

        public ButtonOffice.AnimationState AnimationState
        {
            get
            {
                return _AnimationState;
            }
            set
            {
                _AnimationState = value;
            }
        }

        public System.UInt64 ArrivesAtMinute
        {
            get
            {
                return _ArrivesAtMinute;
            }
            set
            {
                _ArrivesAtMinute = value;
            }
        }

        public System.UInt64 ArrivesAtDayMinute
        {
            get
            {
                return _ArrivesAtDayMinute;
            }
            set
            {
                _ArrivesAtDayMinute = value;
            }
        }

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

        public ButtonOffice.Desk Desk
        {
            get
            {
                return _Desk;
            }
            set
            {
                _Desk = value;
            }
        }

        public System.UInt64 LeavesAtMinute
        {
            get
            {
                return _LeavesAtMinute;
            }
            set
            {
                _LeavesAtMinute = value;
            }
        }

        public ButtonOffice.LivingSide LivingSide
        {
            get
            {
                return _LivingSide;
            }
            set
            {
                _LivingSide = value;
            }
        }

        public System.String Name
        {
            get
            {
                return _Name;
            }
        }

        public ButtonOffice.Type Type
        {
            get
            {
                return _Type;
            }
        }

        public System.UInt64 Wage
        {
            get
            {
                return _Wage;
            }
            set
            {
                _Wage = value;
            }
        }

        public System.Drawing.PointF WalkTo
        {
            get
            {
                return _WalkTo;
            }
            set
            {
                _WalkTo = value;
            }
        }

        public System.UInt64 WorkMinutes
        {
            get
            {
                return _WorkMinutes;
            }
            set
            {
                _WorkMinutes = value;
            }
        }

        protected Person(ButtonOffice.Type Type)
        {
            _Name = "Hagen";
            _Type = Type;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
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

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetLocation(System.Drawing.PointF Location)
        {
            _Rectangle.Location = Location;
        }

        public void SetRectangle(System.Single X, System.Single Y, System.Single Width, System.Single Height)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
            _Rectangle.Width = Width;
            _Rectangle.Height = Height;
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

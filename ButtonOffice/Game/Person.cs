namespace ButtonOffice
{
    internal abstract class Person
    {
        protected System.Single _ActionFraction;
        protected ButtonOffice.ActionState _ActionState;
        protected System.Single _AnimationFraction;
        protected ButtonOffice.AnimationState _AnimationState;
        protected System.UInt64 _ArrivesAtMinute;
        protected System.UInt64 _ArrivesAtDayMinute;
        protected System.Drawing.Color _BackgroundColor;
        protected System.Drawing.Color _BorderColor;
        protected ButtonOffice.Desk _Desk;
        protected System.UInt64 _LeavesAtMinute;
        protected ButtonOffice.LivingSide _LivingSide;
        private System.Drawing.RectangleF _Rectangle;
        private System.String _Name;
        protected System.UInt64 _Wage;
        protected System.Drawing.PointF _WalkTo;
        protected System.UInt64 _WorkMinutes;
        private ButtonOffice.Type _Type;

        public System.Drawing.Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
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

        protected Person(ButtonOffice.Type Type)
        {
            _ActionFraction = 0.0f;
            _ActionState = ButtonOffice.ActionState.New;
            _AnimationState = ButtonOffice.AnimationState.Hidden;
            _AnimationFraction = 0.0f;
            if(ButtonOffice.RandomNumberGenerator.GetBoolean() == true)
            {
                _LivingSide = ButtonOffice.LivingSide.Left;
                SetLocation(-10.0f, 0.0f);
            }
            else
            {
                _LivingSide = ButtonOffice.LivingSide.Right;
                SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
            }
            _Rectangle.Height = ButtonOffice.RandomNumberGenerator.GetSingle(ButtonOffice.Data.PersonHeight, ButtonOffice.Data.PersonHeightSpread);
            _Rectangle.Width = ButtonOffice.RandomNumberGenerator.GetSingle(ButtonOffice.Data.PersonWidth, ButtonOffice.Data.PersonWidthSpread);
            _Name = "Hagen";
            _Type = Type;
        }

        public void AssignDesk(ButtonOffice.Desk Desk)
        {
            System.Diagnostics.Debug.Assert(Desk != null);
            if(_Desk != null)
            {
                System.Diagnostics.Debug.Assert(_Desk.GetPerson() == this);
                _Desk.SetPerson(null);
            }
            _Desk = Desk;
            _Desk.SetPerson(this);
        }

        public void Fire()
        {
            System.Diagnostics.Debug.Assert(_Desk != null);
            _Desk.SetPerson(null);
            _Desk = null;
        }

        public System.Single GetActionFraction()
        {
            return _ActionFraction;
        }

        public System.Single GetAnimationFraction()
        {
            return _AnimationFraction;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return new System.Drawing.PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
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

        public System.Boolean IsHidden()
        {
            return _AnimationState == ButtonOffice.AnimationState.Hidden;
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
        
        protected void _PlanNextWorkDay(ButtonOffice.Game Game)
        {
            System.UInt64 MinuteOfDay = Game.GetMinuteOfDay();

            _ArrivesAtMinute = Game.GetFirstMinuteOfToday() + _ArrivesAtDayMinute;
            if(_ArrivesAtMinute + _WorkMinutes < Game.GetTotalMinutes())
            {
                _ArrivesAtMinute += 1440;
            }
            _LeavesAtMinute = _ArrivesAtMinute + _WorkMinutes;
        }

        public abstract void Move(ButtonOffice.Game Game, System.Single GameMinutes);
    }
}

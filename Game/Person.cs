using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public abstract class Person : PersistentObject
    {
        protected Single _ActionFraction;
        protected Single _AnimationFraction;
        protected AnimationState _AnimationState;
        protected UInt64 _ArrivesAtMinute;
        protected UInt64 _ArrivesAtMinuteOfDay;
        private Boolean _AtDesk;
        protected Color _BackgroundColor;
        protected Color _BorderColor;
        protected Desk _Desk;
        protected UInt64 _LeavesAtMinute;
        protected LivingSide _LivingSide;
        protected Mind _Mind;
        private RectangleF _Rectangle;
        private String _Name;
        protected UInt64 _Wage;
        protected UInt64 _WorkMinutes;

        public Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
        }

        public String Name
        {
            get
            {
                return _Name;
            }
        }

        protected Person()
        {
            _ActionFraction = 0.0f;
            _AnimationState = ButtonOffice.AnimationState.Hidden;
            _AnimationFraction = 0.0f;
            if(RandomNumberGenerator.GetBoolean() == true)
            {
                _LivingSide = LivingSide.Left;
                SetLocation(-10.0f, 0.0f);
            }
            else
            {
                _LivingSide = LivingSide.Right;
                SetLocation(Data.WorldBlockWidth + 10.0f, 0.0f);
            }
            _Mind = new Mind();
            _Rectangle.Height = RandomNumberGenerator.GetSingle(Data.PersonHeight, Data.PersonHeightSpread);
            _Rectangle.Width = RandomNumberGenerator.GetSingle(Data.PersonWidth, Data.PersonWidthSpread);
            _Name = "Hagen";
        }

        public void AssignDesk(Desk Desk)
        {
            Debug.Assert(Desk != null);
            if(_Desk != null)
            {
                Debug.Assert(_Desk.GetPerson() == this);
                _Desk.SetPerson(null);
            }
            _Desk = Desk;
            _Desk.SetPerson(this);
        }

        public void Fire()
        {
            Debug.Assert(_Desk != null);
            _Desk.SetPerson(null);
            _Desk = null;
        }

        public Single GetActionFraction()
        {
            return _ActionFraction;
        }

        public Single GetAnimationFraction()
        {
            return _AnimationFraction;
        }

        public AnimationState GetAnimationState()
        {
            return _AnimationState;
        }

        public UInt64 GetArrivesAtMinute()
        {
            return _ArrivesAtMinute;
        }

        public UInt64 GetArrivesAtMinuteOfDay()
        {
            return _ArrivesAtMinuteOfDay;
        }

        public Boolean GetAtDesk()
        {
            return _AtDesk;
        }

        public Desk GetDesk()
        {
            return _Desk;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public UInt64 GetLeavesAtMinute()
        {
            return _LeavesAtMinute;
        }

        public LivingSide GetLivingSide()
        {
            return _LivingSide;
        }

        public PointF GetMidLocation()
        {
            return new PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public UInt64 GetWage()
        {
            return _Wage;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public UInt64 GetWorkMinutes()
        {
            return _WorkMinutes;
        }

        public Single GetX()
        {
            return _Rectangle.X;
        }

        public Single GetY()
        {
            return _Rectangle.Y;
        }

        public Boolean IsHidden()
        {
            return _AnimationState == AnimationState.Hidden;
        }

        public void SetActionFraction(Single ActionFraction)
        {
            _ActionFraction = ActionFraction;
        }

        public void SetAnimationFraction(Single AnimationFraction)
        {
            _AnimationFraction = AnimationFraction;
        }

        public void SetAnimationState(AnimationState AnimationState)
        {
            _AnimationState = AnimationState;
        }

        public void SetAtDesk(Boolean AtDesk)
        {
            _AtDesk = AtDesk;
        }

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetLocation(PointF Location)
        {
            _Rectangle.Location = Location;
        }

        public void SetRectangle(Single X, Single Y, Single Width, Single Height)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
            _Rectangle.Width = Width;
            _Rectangle.Height = Height;
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetWorkDayMinutes(UInt64 ArrivesAtMinute, UInt64 LeavesAtMinute)
        {
            _ArrivesAtMinute = ArrivesAtMinute;
            _LeavesAtMinute = LeavesAtMinute;
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
        }

        public void Move(Game Game, Single DeltaMinutes)
        {
            _Mind.Move(Game, this, DeltaMinutes);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("action-fraction", _ActionFraction);
            ObjectStore.Save("animation-fraction", _AnimationFraction);
            ObjectStore.Save("animation-state", _AnimationState);
            ObjectStore.Save("arrives-at-minute", _ArrivesAtMinute);
            ObjectStore.Save("arrives-at-minute-of-day", _ArrivesAtMinuteOfDay);
            ObjectStore.Save("at-desk", _AtDesk);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("desk", _Desk);
            ObjectStore.Save("leaves-at-minute", _LeavesAtMinute);
            ObjectStore.Save("living-side", _LivingSide);
            ObjectStore.Save("mind", _Mind);
            ObjectStore.Save("name", _Name);
            ObjectStore.Save("rectangle", _Rectangle);
            ObjectStore.Save("wage", _Wage);
            ObjectStore.Save("work-minutes", _WorkMinutes);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _ActionFraction = ObjectStore.LoadSingleProperty("action-fraction");
            _AnimationFraction = ObjectStore.LoadSingleProperty("animation-fraction");
            _AnimationState = ObjectStore.LoadAnimationStateProperty("animation-state");
            _ArrivesAtMinute = ObjectStore.LoadUInt64Property("arrives-at-minute");
            _ArrivesAtMinuteOfDay = ObjectStore.LoadUInt64Property("arrives-at-minute-of-day");
            _AtDesk = ObjectStore.LoadBooleanProperty("at-desk");
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Desk = ObjectStore.LoadDeskProperty("desk");
            _LeavesAtMinute = ObjectStore.LoadUInt64Property("leaves-at-minute");
            _LivingSide = ObjectStore.LoadLivingSideProperty("living-side");
            _Mind = ObjectStore.LoadMindProperty("mind");
            _Name = ObjectStore.LoadStringProperty("name");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
            _Wage = ObjectStore.LoadUInt64Property("wage");
            _WorkMinutes = ObjectStore.LoadUInt64Property("work-minutes");
        }
    }
}

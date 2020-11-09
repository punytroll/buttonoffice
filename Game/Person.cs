using ButtonOffice.AI;
using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public abstract class Person : Actor
    {
        protected Double _ActionFraction;
        protected Double _AnimationFraction;
        protected AnimationState _AnimationState;
        protected UInt64 _ArrivesAtMinute;
        protected UInt64 _ArrivesAtMinuteOfDay;
        private Boolean _AtDesk;
        protected Color _BackgroundColor;
        protected Color _BorderColor;
        protected Desk _Desk;
        private Double _Height;
        protected UInt64 _LeavesAtMinute;
        protected LivingSide _LivingSide;
        private String _Name;
        protected UInt64 _Wage;
        private Double _Width;
        protected UInt64 _WorkMinutes;
        private Double _X;
        private Double _Y;

        public Color BackgroundColor => _BackgroundColor;

        public Color BorderColor => _BorderColor;

        public Desk Desk => _Desk;

        public String Name => _Name;

        protected Person()
        {
            _ActionFraction = 0.0;
            _AnimationState = AnimationState.Hidden;
            _AnimationFraction = 0.0;
            if(RandomNumberGenerator.GetBoolean() == true)
            {
                _LivingSide = LivingSide.Left;
            }
            else
            {
                _LivingSide = LivingSide.Right;
            }
            _Height = RandomNumberGenerator.GetDouble(Data.PersonHeightMean, Data.PersonHeightSpread);
            _Width = RandomNumberGenerator.GetDouble(Data.PersonWidthMean, Data.PersonWidthSpread);
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

        public Boolean Contains(Vector2 Location)
        {
            return (Math.Abs(_X - Location.X) <= _Width) && (Location.Y >= _Y) && (Location.Y <= _Y + _Height);
        }

        public void Fire()
        {
            Debug.Assert(_Desk != null);
            _Desk.SetPerson(null);
            _Desk = null;
        }

        public Double GetActionFraction()
        {
            return _ActionFraction;
        }

        public Double GetAnimationFraction()
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

        public UInt64 GetLeavesAtMinute()
        {
            return _LeavesAtMinute;
        }

        public LivingSide GetLivingSide()
        {
            return _LivingSide;
        }

        public Vector2 GetLocation()
        {
            return new Vector2(_X, _Y);
        }

        public Vector2 GetMidLocation()
        {
            return new Vector2(_X, _Y + _Height / 2.0);
        }

        public RectangleF GetVisualRectangle()
        {
            return new RectangleF(Convert.ToSingle(_X - _Width / 2.0f), Convert.ToSingle(_Y), Convert.ToSingle(_Width), Convert.ToSingle(_Height));
        }

        public UInt64 GetWage()
        {
            return _Wage;
        }

        public UInt64 GetWorkMinutes()
        {
            return _WorkMinutes;
        }

        public Double GetX()
        {
            return _X;
        }

        public Double GetY()
        {
            return _Y;
        }

        public Boolean IsHidden()
        {
            return _AnimationState == AnimationState.Hidden;
        }

        public void SetActionFraction(Double ActionFraction)
        {
            _ActionFraction = ActionFraction;
        }

        public void SetAnimationFraction(Double AnimationFraction)
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

        public void SetWorkDayMinutes(UInt64 ArrivesAtMinute, UInt64 LeavesAtMinute)
        {
            _ArrivesAtMinute = ArrivesAtMinute;
            _LeavesAtMinute = LeavesAtMinute;
        }

        public void SetX(Double X)
        {
            _X = X;
        }

        public void SetY(Double Y)
        {
            _Y = Y;
        }

        public void Update(Game Game, Double DeltaGameMinutes)
        {
            _Mind.Update(Game, this, DeltaGameMinutes);
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
            ObjectStore.Save("height", _Height);
            ObjectStore.Save("leaves-at-minute", _LeavesAtMinute);
            ObjectStore.Save("living-side", _LivingSide);
            ObjectStore.Save("name", _Name);
            ObjectStore.Save("wage", _Wage);
            ObjectStore.Save("width", _Width);
            ObjectStore.Save("work-minutes", _WorkMinutes);
            ObjectStore.Save("x", _X);
            ObjectStore.Save("y", _Y);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _ActionFraction = ObjectStore.LoadDoubleProperty("action-fraction");
            _AnimationFraction = ObjectStore.LoadDoubleProperty("animation-fraction");
            _AnimationState = ObjectStore.LoadAnimationStateProperty("animation-state");
            _ArrivesAtMinute = ObjectStore.LoadUInt64Property("arrives-at-minute");
            _ArrivesAtMinuteOfDay = ObjectStore.LoadUInt64Property("arrives-at-minute-of-day");
            _AtDesk = ObjectStore.LoadBooleanProperty("at-desk");
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Desk = ObjectStore.LoadDeskProperty("desk");
            _Height = ObjectStore.LoadDoubleProperty("height");
            _LeavesAtMinute = ObjectStore.LoadUInt64Property("leaves-at-minute");
            _LivingSide = ObjectStore.LoadLivingSideProperty("living-side");
            _Name = ObjectStore.LoadStringProperty("name");
            _Wage = ObjectStore.LoadUInt64Property("wage");
            _Width = ObjectStore.LoadDoubleProperty("width");
            _WorkMinutes = ObjectStore.LoadUInt64Property("work-minutes");
            _X = ObjectStore.LoadDoubleProperty("x");
            _Y = ObjectStore.LoadDoubleProperty("y");
        }
    }
}

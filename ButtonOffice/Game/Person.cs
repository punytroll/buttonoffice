namespace ButtonOffice
{
    internal abstract class Person : ButtonOffice.PersistentObject
    {
        protected System.Single _ActionFraction;
        protected System.Single _AnimationFraction;
        protected ButtonOffice.AnimationState _AnimationState;
        protected System.UInt64 _ArrivesAtMinute;
        protected System.UInt64 _ArrivesAtMinuteOfDay;
        private System.Boolean _AtDesk;
        protected System.Drawing.Color _BackgroundColor;
        protected System.Drawing.Color _BorderColor;
        protected ButtonOffice.Desk _Desk;
        protected ButtonOffice.Goal _Goal;
        protected System.UInt64 _LeavesAtMinute;
        protected ButtonOffice.LivingSide _LivingSide;
        private System.Drawing.RectangleF _Rectangle;
        private System.String _Name;
        protected System.UInt64 _Wage;
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

        public ButtonOffice.AnimationState GetAnimationState()
        {
            return _AnimationState;
        }

        public System.UInt64 GetArrivesAtMinute()
        {
            return _ArrivesAtMinute;
        }

        public System.UInt64 GetArrivesAtMinuteOfDay()
        {
            return _ArrivesAtMinuteOfDay;
        }

        public System.Boolean GetAtDesk()
        {
            return _AtDesk;
        }

        public ButtonOffice.Desk GetDesk()
        {
            return _Desk;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.UInt64 GetLeavesAtMinute()
        {
            return _LeavesAtMinute;
        }

        public ButtonOffice.LivingSide GetLivingSide()
        {
            return _LivingSide;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return new System.Drawing.PointF(_Rectangle.X + _Rectangle.Width / 2.0f, _Rectangle.Y + _Rectangle.Height / 2.0f);
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.UInt64 GetWage()
        {
            return _Wage;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.UInt64 GetWorkMinutes()
        {
            return _WorkMinutes;
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

        public void SetActionFraction(System.Single ActionFraction)
        {
            _ActionFraction = ActionFraction;
        }

        public void SetAnimationFraction(System.Single AnimationFraction)
        {
            _AnimationFraction = AnimationFraction;
        }

        public void SetAnimationState(ButtonOffice.AnimationState AnimationState)
        {
            _AnimationState = AnimationState;
        }

        public void SetAtDesk(System.Boolean AtDesk)
        {
            _AtDesk = AtDesk;
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

        public void SetWorkDayMinutes(System.UInt64 ArrivesAtMinute, System.UInt64 LeavesAtMinute)
        {
            _ArrivesAtMinute = ArrivesAtMinute;
            _LeavesAtMinute = LeavesAtMinute;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
        }

        public void Move(ButtonOffice.Game Game, System.Single DeltaMinutes)
        {
            System.Collections.Generic.List<ButtonOffice.Goal> ParentGoals = new System.Collections.Generic.List<ButtonOffice.Goal>();
            ButtonOffice.Goal CurrentGoal = _Goal;

            while(CurrentGoal != null)
            {
                ButtonOffice.Goal NextGoal = null;

                if(CurrentGoal.HasSubGoals() == true)
                {
                    NextGoal = CurrentGoal.GetFirstSubGoal();
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Inactive)
                {
                    CurrentGoal.Activate(Game, this);
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Active)
                {
                    CurrentGoal.Execute(Game, this, DeltaMinutes);
                }
                if(CurrentGoal.GetState() == ButtonOffice.GoalState.Done)
                {
                    System.Collections.Generic.Stack<ButtonOffice.Goal> TerminateGoals = new System.Collections.Generic.Stack<ButtonOffice.Goal>();

                    TerminateGoals.Push(CurrentGoal);
                    while(TerminateGoals.Count > 0)
                    {
                        ButtonOffice.Goal TerminateGoal = TerminateGoals.Peek();

                        if(TerminateGoal.HasSubGoals() == true)
                        {
                            while(TerminateGoal.HasSubGoals() == true)
                            {
                                TerminateGoals.Push(TerminateGoal.GetFirstSubGoal());
                                TerminateGoal.RemoveFirstSubGoal();
                            }
                        }
                        else
                        {
                            TerminateGoals.Pop().Terminate(Game, this);
                        }
                    }
                    if(ParentGoals.Count > 0)
                    {
                        ParentGoals.GetLast().RemoveFirstSubGoal();
                    }
                    else
                    {
                        _Goal = null;
                    }
                    CurrentGoal = null;
                    NextGoal = null;
                }
                if(NextGoal != null)
                {
                    ParentGoals.Add(CurrentGoal);
                    CurrentGoal = NextGoal;
                }
                else
                {
                    CurrentGoal = null;
                }
            }
        }

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("person");

            Result.AppendChild(GameSaver.CreateProperty("action-fraction", _ActionFraction));
            Result.AppendChild(GameSaver.CreateProperty("animation-fraction", _AnimationFraction));
            Result.AppendChild(GameSaver.CreateProperty("animation-state", _AnimationState));
            Result.AppendChild(GameSaver.CreateProperty("arrives-at-minute", _ArrivesAtMinute));
            Result.AppendChild(GameSaver.CreateProperty("arrives-at-minute-of-day", _ArrivesAtMinuteOfDay));
            Result.AppendChild(GameSaver.CreateProperty("at-desk", _AtDesk));
            Result.AppendChild(GameSaver.CreateProperty("background-color", _BackgroundColor));
            Result.AppendChild(GameSaver.CreateProperty("border-color", _BorderColor));
            Result.AppendChild(GameSaver.CreateProperty("desk", _Desk));
            Result.AppendChild(GameSaver.CreateProperty("goal", _Goal));
            Result.AppendChild(GameSaver.CreateProperty("leaves-at-minute", _LeavesAtMinute));
            Result.AppendChild(GameSaver.CreateProperty("living-side", _LivingSide));
            Result.AppendChild(GameSaver.CreateProperty("name", _Name));
            Result.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));
            Result.AppendChild(GameSaver.CreateProperty("type", _Type));
            Result.AppendChild(GameSaver.CreateProperty("wage", _Wage));
            Result.AppendChild(GameSaver.CreateProperty("work-minutes", _WorkMinutes));

            return Result;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _ActionFraction = GameLoader.LoadSingleProperty(Element, "action-fraction");
            _AnimationFraction = GameLoader.LoadSingleProperty(Element, "animation-fraction");
            _AnimationState = GameLoader.LoadAnimationStateProperty(Element, "animation-state");
            _ArrivesAtMinute = GameLoader.LoadUInt64Property(Element, "arrives-at-minute");
            _ArrivesAtMinuteOfDay = GameLoader.LoadUInt64Property(Element, "arrives-at-minute-of-day");
            _AtDesk = GameLoader.LoadBooleanProperty(Element, "at-desk");
            _BackgroundColor = GameLoader.LoadColorProperty(Element, "background-color");
            _BorderColor = GameLoader.LoadColorProperty(Element, "border-color");
            _Desk = GameLoader.LoadDeskProperty(Element, "desk");
            _Goal = GameLoader.LoadGoalProperty(Element, "goal");
            _LeavesAtMinute = GameLoader.LoadUInt64Property(Element, "leaves-at-minute");
            _LivingSide = GameLoader.LoadLivingSideProperty(Element, "living-side");
            _Name = GameLoader.LoadStringProperty(Element, "name");
            _Rectangle = GameLoader.LoadRectangleProperty(Element, "rectangle");
            _Type = GameLoader.LoadTypeProperty(Element, "type");
            _Wage = GameLoader.LoadUInt64Property(Element, "wage");
            _WorkMinutes = GameLoader.LoadUInt64Property(Element, "work-minutes");
        }
    }
}

namespace ButtonOffice
{
    internal abstract class Goal : ButtonOffice.PersistentObject
    {
        private ButtonOffice.GoalState _State;
        public System.Collections.Generic.List<ButtonOffice.Goal> SubGoals;

        public Goal()
        {
            SubGoals = new System.Collections.Generic.List<ButtonOffice.Goal>();
            _State = ButtonOffice.GoalState.Inactive;
        }

        public void AppendSubGoal(ButtonOffice.Goal Goal)
        {
            SubGoals.Add(Goal);
        }

        public ButtonOffice.GoalState GetState()
        {
            return _State;
        }

        public void RemoveSubGoal()
        {
            SubGoals.RemoveAt(0);
        }

        public void SetState(ButtonOffice.GoalState State)
        {
            _State = State;
        }

        public void Activate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Inactive);
            _State = ButtonOffice.GoalState.Active;
            _OnActivate(Game, Person);
        }

        protected virtual void _OnActivate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Execute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Active);
            _OnExecute(Game, Person, DeltaMinutes);
        }

        protected virtual void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
        }

        public void Terminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(SubGoals.Count == 0);
            _OnTerminate(Game, Person);
            _State = ButtonOffice.GoalState.Terminated;
        }

        protected virtual void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("goal");

            Result.AppendChild(GameSaver.CreateProperty("state", _State));

            System.Xml.XmlElement SubGoalsElement = GameSaver.CreateElement("sub-goals");

            foreach(ButtonOffice.Goal Goal in SubGoals)
            {
                SubGoalsElement.AppendChild(GameSaver.CreateProperty("goal", Goal));
            }
            Result.AppendChild(SubGoalsElement);

            return Result;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _State = GameLoader.LoadGoalState(Element, "state");
            foreach(ButtonOffice.Goal Goal in GameLoader.LoadGoalList(Element, "sub-goals", "goal"))
            {
                SubGoals.Add(Goal);
            }
        }
    }
}

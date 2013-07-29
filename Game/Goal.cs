namespace ButtonOffice
{
    public class Goal : ButtonOffice.PersistentObject
    {
        private ButtonOffice.GoalState _State;
        private readonly System.Collections.Generic.List<ButtonOffice.Goal> _SubGoals;

        public Goal()
        {
            _SubGoals = new System.Collections.Generic.List<ButtonOffice.Goal>();
            _State = ButtonOffice.GoalState.Pristine;
        }

        public void AppendSubGoal(ButtonOffice.Goal Goal)
        {
            _SubGoals.Add(Goal);
        }

        public ButtonOffice.Goal GetFirstSubGoal()
        {
            return _SubGoals.GetFirst();
        }

        public ButtonOffice.GoalState GetState()
        {
            return _State;
        }

        public System.Boolean HasSubGoals()
        {
            return _SubGoals.Count > 0;
        }

        public void RemoveFirstSubGoal()
        {
            _SubGoals.RemoveAt(0);
        }

        public void Abort(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Ready || _State == ButtonOffice.GoalState.Executing || _State == ButtonOffice.GoalState.Pristine, ButtonOffice.AssertMessages.CurrentStateIsNotReadyOrExecuting.ToString());
            _OnAbort(Game, Person);
            _State = ButtonOffice.GoalState.Done;
        }

        protected virtual void _OnAbort(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Finish(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Executing, ButtonOffice.AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnFinish(Game, Person);
            _State = ButtonOffice.GoalState.Done;
        }

        protected virtual void _OnFinish(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Initialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Pristine, ButtonOffice.AssertMessages.CurrentStateIsNotPrestine.ToString());
            _State = ButtonOffice.GoalState.Ready;
            _OnInitialize(Game, Person);
        }

        protected virtual void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Execute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Executing, ButtonOffice.AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnExecute(Game, Person, DeltaMinutes);
        }

        protected virtual void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
        }

        public void Resume(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Ready, ButtonOffice.AssertMessages.CurrentStateIsNotReady.ToString());
            _OnResume(Game, Person);
            _State = ButtonOffice.GoalState.Executing;
        }

        protected virtual void _OnResume(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Suspend(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Executing, ButtonOffice.AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnSuspend(Game, Person);
            _State = ButtonOffice.GoalState.Ready;
        }

        protected virtual void _OnSuspend(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
        }

        public void Terminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_State == ButtonOffice.GoalState.Done, ButtonOffice.AssertMessages.CurrentStateIsNotDone.ToString());
            System.Diagnostics.Debug.Assert(_SubGoals.Count == 0);
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

            foreach(ButtonOffice.Goal Goal in _SubGoals)
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
                _SubGoals.Add(Goal);
            }
        }
    }
}

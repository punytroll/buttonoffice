using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice
{
    public class Goal : PersistentObject
    {
        private GoalState _State;
        private readonly List<Goal> _SubGoals;

        public Goal()
        {
            _SubGoals = new List<Goal>();
            _State = GoalState.Pristine;
        }

        public void AppendSubGoal(Goal Goal)
        {
            _SubGoals.Add(Goal);
        }

        public Goal GetFirstSubGoal()
        {
            return _SubGoals.GetFirst();
        }

        public GoalState GetState()
        {
            return _State;
        }

        public Boolean HasSubGoals()
        {
            return _SubGoals.Count > 0;
        }

        public void RemoveFirstSubGoal()
        {
            _SubGoals.RemoveAt(0);
        }

        public void Abort(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Ready || _State == GoalState.Executing || _State == GoalState.Pristine, AssertMessages.CurrentStateIsNotReadyOrExecuting.ToString());
            _OnAbort(Game, Person);
            _State = GoalState.Done;
        }

        protected virtual void _OnAbort(Game Game, Person Person)
        {
        }

        public void Finish(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnFinish(Game, Person);
            _State = GoalState.Done;
        }

        protected virtual void _OnFinish(Game Game, Person Person)
        {
        }

        public void Initialize(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Pristine, AssertMessages.CurrentStateIsNotPrestine.ToString());
            _State = GoalState.Ready;
            _OnInitialize(Game, Person);
        }

        protected virtual void _OnInitialize(Game Game, Person Person)
        {
        }

        public void Execute(Game Game, Person Person, Single DeltaMinutes)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnExecute(Game, Person, DeltaMinutes);
        }

        protected virtual void _OnExecute(Game Game, Person Person, Single DeltaMinutes)
        {
        }

        public void Resume(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Ready, AssertMessages.CurrentStateIsNotReady.ToString());
            _OnResume(Game, Person);
            _State = GoalState.Executing;
        }

        protected virtual void _OnResume(Game Game, Person Person)
        {
        }

        public void Suspend(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnSuspend(Game, Person);
            _State = GoalState.Ready;
        }

        protected virtual void _OnSuspend(Game Game, Person Person)
        {
        }

        public void Terminate(Game Game, Person Person)
        {
            Debug.Assert(_State == GoalState.Done, AssertMessages.CurrentStateIsNotDone.ToString());
            Debug.Assert(_SubGoals.Count == 0);
            _OnTerminate(Game, Person);
            _State = GoalState.Terminated;
        }

        protected virtual void _OnTerminate(Game Game, Person Person)
        {
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("state", _State);
            ObjectStore.Save("sub-goals", _SubGoals);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _State = ObjectStore.LoadGoalState("state");
            foreach(var Goal in ObjectStore.LoadGoals("sub-goals"))
            {
                _SubGoals.Add(Goal);
            }
        }
    }
}

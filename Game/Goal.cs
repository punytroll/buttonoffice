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

        public void Abort(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Ready || _State == GoalState.Executing || _State == GoalState.Pristine, AssertMessages.CurrentStateIsNotReadyOrExecuting.ToString());
            _OnAbort(Game, Actor);
            _State = GoalState.Done;
        }

        protected virtual void _OnAbort(Game Game, PersistentObject Actor)
        {
        }

        public void Finish(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnFinish(Game, Actor);
            _State = GoalState.Done;
        }

        protected virtual void _OnFinish(Game Game, PersistentObject Actor)
        {
        }

        public void Initialize(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Pristine, AssertMessages.CurrentStateIsNotPrestine.ToString());
            _State = GoalState.Ready;
            _OnInitialize(Game, Actor);
        }

        protected virtual void _OnInitialize(Game Game, PersistentObject Actor)
        {
        }

        public void Execute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnExecute(Game, Actor, DeltaGameMinutes);
        }

        protected virtual void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
        }

        public void Resume(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Ready, AssertMessages.CurrentStateIsNotReady.ToString());
            _OnResume(Game, Actor);
            _State = GoalState.Executing;
        }

        protected virtual void _OnResume(Game Game, PersistentObject Actor)
        {
        }

        public void Suspend(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Executing, AssertMessages.CurrentStateIsNotExecuting.ToString());
            _OnSuspend(Game, Actor);
            _State = GoalState.Ready;
        }

        protected virtual void _OnSuspend(Game Game, PersistentObject Actor)
        {
        }

        public void Terminate(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_State == GoalState.Done, AssertMessages.CurrentStateIsNotDone.ToString());
            Debug.Assert(_SubGoals.Count == 0);
            _OnTerminate(Game, Actor);
            _State = GoalState.Terminated;
        }

        protected virtual void _OnTerminate(Game Game, PersistentObject Actor)
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

using ButtonOffice.AI;
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

        public void Abort(Game Game, Actor Actor)
        {
            Debug.Assert(_State == GoalState.Ready || _State == GoalState.Executing || _State == GoalState.Pristine);
            _State = GoalState.Done;
        }

        public void Finish(Game Game, Actor Actor)
        {
            Debug.Assert(_State == GoalState.Executing);
            _OnFinish(Game, Actor);
            _State = GoalState.Done;
        }

        protected virtual void _OnFinish(Game Game, Actor Actor)
        {
        }

        public void Initialize(Game Game, Actor Actor)
        {
            Debug.Assert(_State == GoalState.Pristine);
            _State = GoalState.Ready;
            _OnInitialize(Game, Actor);
        }

        protected virtual void _OnInitialize(Game Game, Actor Actor)
        {
        }

        public void Execute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Debug.Assert(_State == GoalState.Executing || _State == GoalState.Ready);
            _OnExecute(Game, Actor, DeltaGameMinutes);
        }

        protected virtual void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
        }

        public void Terminate(Game Game, Actor Actor)
        {
            Debug.Assert(_State == GoalState.Done);
            Debug.Assert(_SubGoals.Count == 0);
            _OnTerminate(Game, Actor);
            _State = GoalState.Terminated;
        }

        protected virtual void _OnTerminate(Game Game, Actor Actor)
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

using System;
using System.Collections.Generic;

namespace ButtonOffice
{
    public class Mind : PersistentObject
    {
        protected Goal _RootGoal;

        public Mind()
        {
            _RootGoal = null;
        }

        public void Move(Game Game, Person Person, Single DeltaMinutes)
        {
            var ParentGoals = new List<Goal>();
            var CurrentGoal = _RootGoal;

            while(CurrentGoal != null)
            {
                Goal NextGoal = null;

                if(CurrentGoal.HasSubGoals() == true)
                {
                    NextGoal = CurrentGoal.GetFirstSubGoal();
                }
                if(CurrentGoal.GetState() == GoalState.Pristine)
                {
                    CurrentGoal.Initialize(Game, Person);
                }
                if(CurrentGoal.GetState() == GoalState.Ready)
                {
                    CurrentGoal.Resume(Game, Person);
                }
                if(CurrentGoal.GetState() == GoalState.Executing)
                {
                    CurrentGoal.Execute(Game, Person, DeltaMinutes);
                }
                if(CurrentGoal.GetState() == GoalState.Done)
                {
                    var TerminateGoals = new Stack<Goal>();

                    TerminateGoals.Push(CurrentGoal);
                    while(TerminateGoals.Count > 0)
                    {
                        var TerminateGoal = TerminateGoals.Peek();

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
                            if((TerminateGoal.GetState() == GoalState.Pristine) || (TerminateGoal.GetState() == GoalState.Ready) || (TerminateGoal.GetState() == GoalState.Executing))
                            {
                                TerminateGoal.Abort(Game, Person);
                            }
                            TerminateGoal.Terminate(Game, Person);
                            TerminateGoals.Pop();
                        }
                    }
                    if(ParentGoals.Count > 0)
                    {
                        ParentGoals.GetLast().RemoveFirstSubGoal();
                    }
                    else
                    {
                        _RootGoal = null;
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

        public void SetRootGoal(Goal RootGoal)
        {
            _RootGoal = RootGoal;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("root-goal", _RootGoal);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _RootGoal = ObjectStore.LoadGoalProperty("root-goal");
        }
    }
}

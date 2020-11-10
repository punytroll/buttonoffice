using System;
using System.Collections.Generic;

namespace ButtonOffice.AI.Goals
{
    public class Mind : ButtonOffice.AI.Mind
    {
        protected Goal _RootGoal;
        
        public Mind()
        {
            _RootGoal = null;
        }
        
        public override void Update(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var ParentGoals = new List<Goal>();
            var CurrentGoal = _RootGoal;
            
            while(CurrentGoal != null)
            {
                var Result = BehaviorResult.Running;
                
                if((Result == BehaviorResult.Running) && (CurrentGoal.GetState() == GoalState.Pristine))
                {
                    Result = CurrentGoal.Initialize(Game, Actor);
                }
                if((Result == BehaviorResult.Running) && ((CurrentGoal.GetState() == GoalState.Executing) || (CurrentGoal.GetState() == GoalState.Initialized)))
                {
                    Result = CurrentGoal.Execute(Game, Actor, DeltaGameMinutes);
                }
                if((Result == BehaviorResult.Succeeded) || (Result == BehaviorResult.Failed))
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
                            TerminateGoal.Terminate(Game, Actor);
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
                }
                if(CurrentGoal.HasSubGoals() == true)
                {
                    ParentGoals.Add(CurrentGoal);
                    CurrentGoal = CurrentGoal.GetFirstSubGoal();
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

using ButtonOffice.AI.BehaviorTrees;
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
            Console.WriteLine("================================");
            
            var ParentGoals = new List<Goal>();
            var CurrentGoal = _RootGoal;
            
            while(CurrentGoal != null)
            {
                if(CurrentGoal.GetState() == GoalState.Pristine)
                {
                    CurrentGoal.Initialize(Game, Actor);
                }
                if(CurrentGoal.GetState() == GoalState.Executing)
                {
                    CurrentGoal.Execute(Game, Actor, DeltaGameMinutes);
                }
                if((CurrentGoal.GetState() == GoalState.Succeeded) || (CurrentGoal.GetState() == GoalState.Failed))
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
        
        public override void SetThought(String Thought)
        {
            _RootGoal = BehaviorFactory.CreateBehavior(Thought);
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

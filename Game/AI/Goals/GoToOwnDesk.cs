using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class GoToOwnDesk : Goal
    {
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var Desk = Person.Desk;
            
            Debug.Assert(Desk != null);
            
            var WalkToDesk = new WalkToDesk();
            
            WalkToDesk.SetDesk(Desk);
            AppendSubGoal(WalkToDesk);
            
            return BehaviorResult.Running;
        }
        
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
            
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;
                
                Debug.Assert(Person != null);
                Person.SetAtDesk(true);
                Result = BehaviorResult.Succeeded;
            }
            
            return Result;
        }
    }
}

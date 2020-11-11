using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class GoToOwnDesk : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var Desk = Person.Desk;
            
            Debug.Assert(Desk != null);
            
            var WalkToDesk = new WalkToDesk();
            
            WalkToDesk.SetDesk(Desk);
            AppendSubGoal(WalkToDesk);
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;
                
                Debug.Assert(Person != null);
                Person.SetAtDesk(true);
                Succeed();
            }
        }
    }
}

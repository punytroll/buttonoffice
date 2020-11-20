using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class StandUpFromOwnDesk : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("StandUpFromOwnDesk.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("StandUpFromOwnDesk.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAtDesk(false);
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("StandUpFromOwnDesk.Terminate");
        }
    }
}

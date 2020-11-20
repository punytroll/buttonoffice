using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class SitDownAtOwnDesk : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("SitDownAtOwnDesk.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("SitDownAtOwnDesk.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAtDesk(true);
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("SitDownAtOwnDesk.Terminate");
        }
    }
}

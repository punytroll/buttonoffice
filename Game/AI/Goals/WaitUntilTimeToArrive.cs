using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class WaitUntilTimeToArrive : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("WaitUntilTimeToArrive.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("WaitUntilTimeToArrive.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetArrivesAtMinute())
            {
                Succeed();
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("WaitUntilTimeToArrive.Terminate");
        }
    }
}

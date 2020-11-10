using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class WaitUntilTimeToArrive : Goal
    {
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetArrivesAtMinute())
            {
                Result = BehaviorResult.Succeeded;
            }
            
            return Result;
        }
    }
}

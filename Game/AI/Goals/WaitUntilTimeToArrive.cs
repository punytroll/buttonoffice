using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class WaitUntilTimeToArrive : Goal
    {
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetArrivesAtMinute())
            {
                Succeed();
            }
        }
    }
}

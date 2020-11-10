using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class PlanNextWorkDay : Goal
    {
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var ArrivesAtMinute = Game.GetFirstMinuteOfToday() + Person.GetArrivesAtMinuteOfDay();
            
            if(ArrivesAtMinute + Person.GetWorkMinutes() < Game.GetTotalMinutes())
            {
                ArrivesAtMinute += 1440;
            }
            Person.SetWorkDayMinutes(ArrivesAtMinute, ArrivesAtMinute + Person.GetWorkMinutes());
            
            return BehaviorResult.Succeeded;
        }
    }
}

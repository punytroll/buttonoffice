using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class PlanNextWorkDay : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("PlanNextWorkDay.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("PlanNextWorkDay.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var ArrivesAtMinute = Game.GetFirstMinuteOfToday() + Person.GetArrivesAtMinuteOfDay();
            
            if(ArrivesAtMinute + Person.GetWorkMinutes() < Game.GetTotalMinutes())
            {
                ArrivesAtMinute += 1440;
            }
            Person.SetWorkDayMinutes(ArrivesAtMinute, ArrivesAtMinute + Person.GetWorkMinutes());
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("PlanNextWorkDay.Terminate");
        }
    }
}

using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class CollectWage : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("CollectWage.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("CollectWage.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Game.SpendMoney(Person.GetWage(), Person.GetMidLocation());
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("CollectWage.Terminate");
        }
    }
}

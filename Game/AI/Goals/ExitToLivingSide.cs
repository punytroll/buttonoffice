using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class ExitToLivingSide : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("ExitToLivingSide.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("ExitToLivingSide.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Hidden);
            Person.SetAnimationFraction(0.0);
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("ExitToLivingSide.Terminate");
        }
    }
}

using ButtonOffice.AI.Goals;
using System;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class EnterFromLivingSide : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("EnterFromLivingSide.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("EnterFromLivingSide.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0);
            if(Person.GetLivingSide() == LivingSide.Left)
            {
                Person.SetX(Game.LeftBorder - 10.0);
            }
            else
            {
                Person.SetX(Game.RightBorder + 10.0);
            }
            Person.SetY(0.0);
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("EnterFromLivingSide.Terminate");
        }
    }
}

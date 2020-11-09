using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class GoToWork : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
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
            AppendSubGoal(new GoToOwnDesk());
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
        }
    }
}

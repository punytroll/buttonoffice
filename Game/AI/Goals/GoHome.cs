using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class GoHome : Goal
    {
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Game.SpendMoney(Person.GetWage(), Person.GetMidLocation());
            
            var WalkToLocation = new TravelToLocation();
            
            if(Person.GetLivingSide() == LivingSide.Left)
            {
                WalkToLocation.SetLocation(new Vector2(Game.LeftBorder - 10.0, 0.0));
            }
            else
            {
                WalkToLocation.SetLocation(new Vector2(Game.RightBorder + 10.0, 0.0));
            }
            Person.SetAtDesk(false);
            AppendSubGoal(WalkToLocation);
            
            return BehaviorResult.Running;
        }
        
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
            
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;
                
                Debug.Assert(Person != null);
                Person.SetAnimationState(AnimationState.Hidden);
                Person.SetAnimationFraction(0.0);
                Result = BehaviorResult.Succeeded;
            }
            
            return Result;
        }
    }
}

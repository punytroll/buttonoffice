using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class Accounting : Goal
    {
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Accounting);
            Person.SetAnimationFraction(0.0);
            
            return BehaviorResult.Running;
        }
        
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Result = BehaviorResult.Succeeded;
            }
            else
            {
                if(Person.Desk.Computer.IsBroken() == false)
                {
                    Person.Desk.Computer.Use(DeltaGameMinutes);
                    if(Person.Desk.Computer.IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0);
                        Person.SetAnimationState(AnimationState.Standing);
                        Person.SetAnimationFraction(0.0);
                        Game.EnqueueBrokenThing(Person.Desk.Computer);
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + Data.AccountantWorkSpeed * DeltaGameMinutes);
                        while(Person.GetActionFraction() >= 1.0)
                        {
                            Person.SetActionFraction(Person.GetActionFraction() - 1.0);
                            Person.Desk.TrashLevel += 2.0;
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.AccountantWorkSpeed * DeltaGameMinutes);
                        while(Person.GetAnimationFraction() >= 1.0)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0);
                        }
                    }
                }
            }
            
            return Result;
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0);
        }
    }
}

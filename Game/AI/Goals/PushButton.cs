using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class PushButton : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.PushingButton);
            Person.SetAnimationFraction(0.0);
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
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
                        Person.SetActionFraction(Person.GetActionFraction() + Data.WorkerWorkSpeed * DeltaGameMinutes);
                        while(Person.GetActionFraction() >= 1.0)
                        {
                            var Revenue = 100L * Game.GetCurrentBonusPromille() / 1000L;
                            
                            Person.SetActionFraction(Person.GetActionFraction() - 1.0);
                            Person.Desk.TrashLevel += 1.0;
                            Game.EarnMoney(Revenue, Person.GetMidLocation());
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.WorkerWorkSpeed * DeltaGameMinutes);
                        while(Person.GetAnimationFraction() >= 1.0)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0);
                        }
                    }
                }
            }
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

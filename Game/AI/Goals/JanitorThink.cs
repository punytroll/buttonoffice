using System;

namespace ButtonOffice.AI.Goals
{
    internal class JanitorThink : Goal
    {
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new CleanDesks());
                AppendSubGoal(new GoHome());
            }
            
            return BehaviorResult.Running;
        }
    }
}

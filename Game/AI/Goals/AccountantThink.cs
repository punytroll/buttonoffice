using System;

namespace ButtonOffice.AI.Goals
{
    internal class AccountantThink : Goal
    {
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new Accounting());
                AppendSubGoal(new GoHome());
            }
            
            return BehaviorResult.Running;
        }
    }
}

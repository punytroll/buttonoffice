using System;

namespace ButtonOffice.AI.Goals
{
    internal class WorkerThink : Goal
    {
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new PushButton());
                AppendSubGoal(new GoHome());
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;

namespace ButtonOffice.AI.Goals
{
    internal class CleanDesks : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            var Janitor = Actor as Janitor;
            
            Debug.Assert(Janitor != null);
            foreach(var Office in Game.Offices.OrderBy(delegate(Office Office)
                                                       {
                                                           var Result = Office.Floor - Janitor.Desk.GetY();
                                                           
                                                           if(Result < 0.0)
                                                           {
                                                               Result = (Game.HighestFloor - Game.LowestFloor) - Result;
                                                           }
                                                           
                                                           return Result;
                                                       }).ThenBy((Office) => Office.Left))
            {
                Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                Janitor.EnqueueCleaningTarget(Office.FourthDesk);
            }
            Janitor.SetAtDesk(false);
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Janitor = Actor as Janitor;
            
            Debug.Assert(Janitor != null);
            if(Game.GetTotalMinutes() > Janitor.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    var CleaningTarget = Janitor.PeekCleaningTarget();
                    
                    if(CleaningTarget != null)
                    {
                        var WalkToLocation = new TravelToLocation();
                        
                        WalkToLocation.SetLocation(new Vector2(CleaningTarget.GetX() + CleaningTarget.GetWidth() / 2.0, CleaningTarget.GetY()));
                        AppendSubGoal(WalkToLocation);
                        
                        var CleanDesk = new CleanDesk();
                        
                        CleanDesk.SetCleaningTarget(CleaningTarget);
                        AppendSubGoal(CleanDesk);
                    }
                    else
                    {
                        Finish(Game, Actor);
                    }
                }
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            var Janitor = Actor as Janitor;
            
            Debug.Assert(Janitor != null);
            Janitor.ClearCleaningTargets();
        }
    }
}

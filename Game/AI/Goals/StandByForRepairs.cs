using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class StandByForRepairs : Goal
    {
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var ITTech = Actor as ITTech;
            
            Debug.Assert(ITTech != null);
            if(Game.GetTotalMinutes() > ITTech.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    var BrokenThing = Game.DequeueBrokenThing();
                    
                    if(BrokenThing != null)
                    {
                        ITTech.SetRepairingTarget(BrokenThing);
                        
                        var FindPathToLocation = new TravelToLocation();
                        
                        if(BrokenThing is Computer)
                        {
                            var Computer = (Computer)BrokenThing;
                            
                            FindPathToLocation.SetLocation(new Vector2(Computer.GetX() + Computer.GetWidth() / 2.0, Computer.GetY().GetFloored()));
                        }
                        else if(BrokenThing is Lamp)
                        {
                            var Lamp = (Lamp)BrokenThing;
                            
                            FindPathToLocation.SetLocation(new Vector2(Lamp.Left + Lamp.Width / 2.0, Lamp.Office.Floor));
                        }
                        AppendSubGoal(FindPathToLocation);
                        AppendSubGoal(new Repair());
                        AppendSubGoal(new GoToOwnDesk());
                        ITTech.SetAtDesk(false);
                    }
                }
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            var ITTech = Actor as ITTech;
            
            Debug.Assert(ITTech != null);
            if(ITTech.GetRepairingTarget() != null)
            {
                Game.EnqueueBrokenThing(ITTech.GetRepairingTarget());
            }
        }
    }
}

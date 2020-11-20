using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class SetTravelLocationToOwnDesk : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("SetTravelLocationToOwnDesk.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("SetTravelLocationToOwnDesk.Execute");
            
            var Desk = Actor.Mind.Memory.Get<Desk>("desk");
            
            Actor.Mind.Memory.Add("travel-to-location", new Vector2(Desk.GetX() + Desk.GetWidth() / 2.0, Desk.GetY()));
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("SetTravelLocationToOwnDesk.Terminate");
        }
    }
}

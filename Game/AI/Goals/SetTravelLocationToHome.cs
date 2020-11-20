using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class SetTravelLocationToHome : Goal
    {
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("SetTravelLocationToHome.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("SetTravelLocationToHome.Execute");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            if(Person.GetLivingSide() == LivingSide.Left)
            {
                Actor.Mind.Memory.Add("travel-to-location", new Vector2(Game.LeftBorder - 10.0, 0.0));
            }
            else
            {
                Actor.Mind.Memory.Add("travel-to-location", new Vector2(Game.RightBorder + 10.0, 0.0));
            }
            Succeed();
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("SetTravelLocationToHome.Terminate");
        }
    }
}

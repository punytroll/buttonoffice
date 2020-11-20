using ButtonOffice.AI.Goals;
using ButtonOffice.Transportation;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class TravelToMemoryLocation : Goal
    {
        private readonly Queue<TravelAction> _TravelActions;
        
        public TravelToMemoryLocation()
        {
            _TravelActions = new Queue<TravelAction>();
        }
        
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("TravelToMemoryLocation.Initialize");
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var Path = Game.Transportation.GetShortestPath(Person.GetLocation(), Actor.Mind.Memory.Get<Vector2>("travel-to-location"));
            
            if(Path != null)
            {
                foreach(var Edge in Path)
                {
                    _TravelActions.Enqueue(Edge.CreateTravelAction());
                }
            }
            else
            {
                Fail();
            }
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("TravelToMemoryLocation.Execute");
            if(_TravelActions.Count > 0)
            {
                var TravelAction = _TravelActions.Peek();
                
                Debug.Assert(TravelAction.State == TravelActionState.Running);
                TravelAction.Execute(Game, Actor, DeltaGameMinutes);
                if(TravelAction.State == TravelActionState.Succeeded)
                {
                    Console.WriteLine("    Succeeded");
                    _TravelActions.Dequeue();
                    if(_TravelActions.Count == 0)
                    {
                        Succeed();
                    }
                }
                else if(TravelAction.State == TravelActionState.Failed)
                {
                    Console.WriteLine("    Failed");
                    Fail();
                }
                else
                {
                    Console.WriteLine("    Running");
                }
            }
            else
            {
                Succeed();
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("TravelToMemoryLocation.Termiante");
            Actor.Mind.Memory.Remove("travel-to-location");
        }
    }
}

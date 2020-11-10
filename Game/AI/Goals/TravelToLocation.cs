using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class TravelToLocation : Goal
    {
        private Vector2 _Location;
        
        public void SetLocation(Vector2 Location)
        {
            _Location = Location;
        }
        
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            var Result = BehaviorResult.Running;
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var Path = Game.Transportation.GetShortestPath(Person.GetLocation(), _Location);
            
            if(Path != null)
            {
                foreach(var Edge in Path)
                {
                    AppendSubGoal(Edge.CreateUseGoal());
                }
            }
            else
            {
                Result = BehaviorResult.Failed;
            }
            
            return Result;
        }
        
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
            
            if(HasSubGoals() == false)
            {
                Result = BehaviorResult.Succeeded;
            }
            
            return Result;
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("location", _Location);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Location = ObjectStore.LoadVector2Property("location");
        }
    }
}

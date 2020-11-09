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

        protected override void _OnInitialize(Game Game, Actor Actor)
        {
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
                Abort(Game, Person);
            }
        }

        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
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

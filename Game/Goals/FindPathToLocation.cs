using System;
using System.Diagnostics;

namespace ButtonOffice
{
    internal class FindPathToLocation : Goal
    {
        private Vector2 _Location;

        public void SetLocation(Vector2 Location)
        {
            _Location = Location;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var Path = Game.Transportation.GetPath(new Vector2(Person.GetX(), Person.GetY()), _Location);

            if(Path != null)
            {
                foreach(var Edge in Path)
                {
                    var CreateUseGoalFunction = Edge.CreateUseGoalFunction;

                    Debug.Assert(CreateUseGoalFunction != null);
                    AppendSubGoal(CreateUseGoalFunction());
                }
            }
            else
            {
                Abort(Game, Person);
            }
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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

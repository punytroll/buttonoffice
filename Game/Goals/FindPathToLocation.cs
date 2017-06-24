using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    internal class FindPathToLocation : Goal
    {
        private PointF _Location;

        public void SetLocation(PointF Location)
        {
            _Location = Location;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var Path = Game.GetPath(new PointF(Convert.ToSingle(Person.GetX()), Convert.ToSingle(Person.GetY())), _Location);

            if(Path != null)
            {
                foreach(var Edge in Path)
                {
                    var CreateUseGoalFunction = Edge.CreateUseGoalFunction;

                    Debug.Assert(CreateUseGoalFunction != null);

                    var UseGoal = CreateUseGoalFunction(Edge.ToX, Edge.ToY);

                    AppendSubGoal(UseGoal);
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
            _Location = ObjectStore.LoadPointProperty("location");
        }
    }
}

using System;
using System.Diagnostics;

namespace ButtonOffice
{
    internal class WalkOnSameFloor : Goal
    {
        private Double _X;

        public void SetX(Double X)
        {
            _X = X;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0);
            Person.SetAnimationState(AnimationState.Walking);
            Person.SetAnimationFraction(0.0);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var DeltaX = _X - Person.GetX();

            if(Math.Abs(DeltaX) > 0.1)
            {
                if(DeltaX > 0.0)
                {
                    DeltaX = Data.PersonSpeed * DeltaGameMinutes;
                }
                else
                {
                    DeltaX = -Data.PersonSpeed * DeltaGameMinutes;
                }
                Person.SetX(Convert.ToSingle(Person.GetX() + DeltaX));
            }
            else
            {
                Person.SetX(Convert.ToSingle(_X));
                Finish(Game, Person);
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("x", _X);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _X = ObjectStore.LoadDoubleProperty("x");
        }
    }
}

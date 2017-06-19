using System;
using System.Diagnostics;

namespace ButtonOffice.Goals
{
    internal class UseStairs : Goal
    {
        private Int32? _TargetFloor;
        private Stairs _Stairs;

        public void SetStairs(Stairs Stairs)
        {
            _Stairs = Stairs;
        }

        public void SetTargetFloor(Int32 TargetFloor)
        {
            _TargetFloor = TargetFloor;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_Stairs != null);
            Debug.Assert(_TargetFloor != null);

            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Walking);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            Debug.Assert(_Stairs != null);
            Debug.Assert(_TargetFloor != null);

            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var DeltaY = Data.StairsSpeed * DeltaMinutes;

            if(Person.GetY() > _TargetFloor)
            {
                DeltaY *= -1.0f;
            }

            var NewY = Person.GetY() + DeltaY;

            if(DeltaY < 0.0f)
            {
                if(NewY <= _TargetFloor)
                {
                    Person.SetLocation(Person.GetX(), _TargetFloor.Value);
                    Finish(Game, Person);
                }
                else
                {
                    Person.SetLocation(Person.GetX(), NewY);
                }
            }
            else
            {
                if(NewY >= _TargetFloor)
                {
                    Person.SetLocation(Person.GetX(), _TargetFloor.Value);
                    Finish(Game, Person);
                }
                else
                {
                    Person.SetLocation(Person.GetX(), NewY);
                }
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            Debug.Assert(_TargetFloor != null);
            base.Save(ObjectStore);
            ObjectStore.Save("stairs", _Stairs);
            ObjectStore.Save("target-floor", _TargetFloor.Value);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Stairs = ObjectStore.LoadStairsProperty("stairs");
            _TargetFloor = ObjectStore.LoadInt32Property("target-floor");
        }
    }
}

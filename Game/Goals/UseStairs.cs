using System;
using System.Diagnostics;

namespace ButtonOffice.Goals
{
    internal class UseStairs : Goal
    {
        private Boolean _Downwards;
        private Stairs _Stairs;

        public void SetStairs(Stairs Stairs)
        {
            _Stairs = Stairs;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Walking);
            Person.SetAnimationFraction(0.0f);
            _Downwards = _Stairs.GetY() < Person.GetY();
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var DeltaY = 0.0f;

            if(_Downwards == true)
            {
                DeltaY = -Data.StairsSpeed * DeltaMinutes;
            }
            else
            {
                DeltaY = Data.StairsSpeed * DeltaMinutes;
            }

            var NewY = Person.GetY() + DeltaY;

            if(_Downwards == true)
            {
                if(NewY <= _Stairs.GetY())
                {
                    Person.SetLocation(Person.GetX(), _Stairs.GetY());
                    Finish(Game, Person);
                }
                else
                {
                    Person.SetLocation(Person.GetX(), NewY);
                }
            }
            else
            {
                if(NewY > _Stairs.GetY() + 1.0f)
                {
                    Person.SetLocation(Person.GetX(), _Stairs.GetY() + 1.0f);
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
            base.Save(ObjectStore);
            ObjectStore.Save("downwards", _Downwards);
            ObjectStore.Save("stairs", _Stairs);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Downwards = ObjectStore.LoadBooleanProperty("downwards");
            _Stairs = ObjectStore.LoadStairsProperty("stairs");
        }
    }
}

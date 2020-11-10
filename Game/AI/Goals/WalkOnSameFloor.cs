using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class WalkOnSameFloor : Goal
    {
        private Double _X;
        
        public void SetX(Double X)
        {
            _X = X;
        }
        
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0);
            Person.SetAnimationState(AnimationState.Walking);
            Person.SetAnimationFraction(0.0);
            
            return BehaviorResult.Running;
        }
        
        protected override BehaviorResult _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var Result = BehaviorResult.Running;
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
                Person.SetX(Person.GetX() + DeltaX);
            }
            else
            {
                Person.SetX(_X);
                Result = BehaviorResult.Succeeded;
            }
            
            return Result;
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
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

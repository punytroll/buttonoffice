using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
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
        
        protected override BehaviorResult _OnInitialize(Game Game, Actor Actor)
        {
            Debug.Assert(_Stairs != null);
            Debug.Assert(_TargetFloor != null);
            
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
            
            Debug.Assert(_Stairs != null);
            Debug.Assert(_TargetFloor != null);
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var DeltaY = Convert.ToSingle(Data.StairsSpeed * DeltaGameMinutes);
            
            if(Person.GetY() > _TargetFloor)
            {
                DeltaY *= -1.0f;
            }
            
            var NewY = Person.GetY() + DeltaY;
            
            if(DeltaY < 0.0f)
            {
                if(NewY <= _TargetFloor)
                {
                    Person.SetY(_TargetFloor.Value);
                    Result = BehaviorResult.Succeeded;
                }
                else
                {
                    Person.SetY(NewY);
                }
            }
            else
            {
                if(NewY >= _TargetFloor)
                {
                    Person.SetY(_TargetFloor.Value);
                    Result = BehaviorResult.Succeeded;
                }
                else
                {
                    Person.SetY(NewY);
                }
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

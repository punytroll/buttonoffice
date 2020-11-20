using ButtonOffice.AI;
using System;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
    internal class TravelActionUseStairs : TravelAction
    {
        public Int32? TargetFloor
        {
            get;
            set;
        }
        
        public override void Execute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("TravelActionUseStairs.Execute");
            Debug.Assert(TargetFloor != null);
            
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var DeltaY = Convert.ToSingle(Data.StairsSpeed * DeltaGameMinutes);
            
            if(Person.GetY() > TargetFloor)
            {
                DeltaY *= -1.0f;
            }
            
            var NewY = Person.GetY() + DeltaY;
            
            if(DeltaY < 0.0f)
            {
                if(NewY <= TargetFloor)
                {
                    Person.SetY(TargetFloor.Value);
                    State = TravelActionState.Succeeded;
                }
                else
                {
                    Person.SetY(NewY);
                }
            }
            else
            {
                if(NewY >= TargetFloor)
                {
                    Person.SetY(TargetFloor.Value);
                    State = TravelActionState.Succeeded;
                }
                else
                {
                    Person.SetY(NewY);
                }
            }
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            Debug.Assert(TargetFloor != null);
            base.Save(ObjectStore);
            ObjectStore.Save("target-floor", TargetFloor.Value);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            TargetFloor = ObjectStore.LoadInt32Property("target-floor");
        }
    }
}

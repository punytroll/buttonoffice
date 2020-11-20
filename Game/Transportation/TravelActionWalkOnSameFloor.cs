using ButtonOffice.AI;
using System;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
    internal class TravelActionWalkOnSameFloor : TravelAction
    {
        public Double X
        {
            get;
            set;
        }
        
        public override void Execute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("TravelActionWalkOnSameFloor.Execute");
            var Person = Actor as Person;
            
            Debug.Assert(Person != null);
            
            var DeltaX = X - Person.GetX();
            
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
                Person.SetX(X);
                State = TravelActionState.Succeeded;
            }
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("x", X);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            X = ObjectStore.LoadDoubleProperty("x");
        }
    }
}

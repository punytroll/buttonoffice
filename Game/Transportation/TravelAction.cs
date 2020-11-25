using ButtonOffice.AI;
using System;

namespace ButtonOffice.Transportation
{
    internal abstract class TravelAction : PersistentObject
    {
        public TravelActionState State
        {
            get;
            protected set;
        }
        
        public TravelAction()
        {
            State = TravelActionState.Running;
        }
        
        public abstract void Execute(Game Game, Actor Actor, Double DeltaGameMinutes);
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("state", State);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            State = ObjectStore.LoadTravelActionState("state");
        }
    }
}

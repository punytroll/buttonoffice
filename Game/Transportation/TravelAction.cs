using ButtonOffice.AI;
using System;

namespace ButtonOffice.Transportation
{
    internal abstract class TravelAction : PersistentObject
    {
        public TravelActionState State;
        
        public TravelAction()
        {
            State = TravelActionState.Running;
        }
        
        public abstract void Execute(Game Game, Actor Actor, Double DeltaGameMinutes);
    }
}

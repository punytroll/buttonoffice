using System;
using System.Collections.Generic;

namespace ButtonOffice
{
    public abstract class Mind : PersistentObject
    {
        public Mind()
        {
        }
        
        public abstract void Move(Game Game, PersistentObject Actor, Double DeltaGameMinutes);
    }
}

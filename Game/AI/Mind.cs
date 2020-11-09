using System;

namespace ButtonOffice.AI
{
    public abstract class Mind : PersistentObject
    {
        public abstract void Update(Game Game, Actor Actor, Double DeltaGameMinutes);
    }
}

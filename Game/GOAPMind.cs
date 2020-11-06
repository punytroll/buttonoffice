using System;
using System.Collections.Generic;

namespace ButtonOffice
{
    public class GOAPMind : Mind
    {
        public GOAPMind()
        {
        }

        public override void Move(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
        }
    }
}

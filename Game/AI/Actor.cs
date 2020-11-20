using System;

namespace ButtonOffice.AI
{
    public class Actor : ButtonOffice.PersistentObject
    {
        public Mind Mind
        {
            get;
            protected set;
        }
        
        protected Actor()
        {
            Mind = null;
        }
        
        public virtual void Update(Game Game, Double DeltaGameMinutes)
        {
            if(Mind != null)
            {
                Mind.Update(Game, this, DeltaGameMinutes);
            }
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("mind", Mind);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            Mind = ObjectStore.LoadMindProperty("mind");
        }
    }
}

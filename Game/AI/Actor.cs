using System;

namespace ButtonOffice.AI
{
    public class Actor : ButtonOffice.PersistentObject
    {
        protected Mind _Mind;
        
        protected Actor()
        {
            _Mind = null;
        }
        
        public void Update(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(_Mind != null)
            {
                _Mind.Update(Game, Actor, DeltaGameMinutes);
            }
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("mind", _Mind);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Mind = ObjectStore.LoadMindProperty("mind");
        }
    }
}

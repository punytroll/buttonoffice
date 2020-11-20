using System;

namespace ButtonOffice.AI
{
    public abstract class Mind : PersistentObject
    {
        public Memory Memory
        {
            get;
            private set;
        }
        
        public Mind()
        {
            Memory = new Memory();
        }
        
        public abstract void Update(Game Game, Actor Actor, Double DeltaGameMinutes);
        public abstract void SetThought(String Thought);
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("memory", Memory);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            Memory = ObjectStore.LoadMemoryProperty("memory");
        }
    }
}

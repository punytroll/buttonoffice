namespace ButtonOffice
{
    public abstract class PersistentObject
    {
        public virtual void Save(SaveObjectStore ObjectStore)
        {
        }

        public virtual void Load(LoadObjectStore ObjectStore)
        {
        }
    }
}

namespace ButtonOffice
{
    public interface PersistentObject
    {
        void Save(SaveObjectStore ObjectStore);
        void Load(LoadObjectStore ObjectStore);
    }
}

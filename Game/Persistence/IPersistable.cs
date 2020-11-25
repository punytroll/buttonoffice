namespace ButtonOffice.Persistence
{
    internal interface IPersistable
    {
        void Save(SaveObjectStore ObjectStore);
        void Load(LoadObjectStore ObjectStore);
    }
}

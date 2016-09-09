namespace ButtonOffice
{
    public interface PersistentObject
    {
        void Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element);
        void Load(LoadObjectStore ObjectStore);
    }
}

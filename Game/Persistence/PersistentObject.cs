namespace ButtonOffice
{
    public interface PersistentObject
    {
        void Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element);
        void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element);
    }
}

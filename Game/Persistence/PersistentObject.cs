namespace ButtonOffice
{
    public interface PersistentObject
    {
        System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver);
        void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element);
    }
}

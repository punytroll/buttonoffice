namespace ButtonOffice
{
    internal interface IPersistentObject
    {
        System.Xml.XmlElement Save(ButtonOffice.GameSaver SaveGameProcessor);
    }
}

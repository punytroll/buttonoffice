namespace ButtonOffice
{
    internal interface IPersistentObject
    {
        System.Xml.XmlElement Save(ButtonOffice.SaveGameProcessor SaveGameProcessor);
    }
}

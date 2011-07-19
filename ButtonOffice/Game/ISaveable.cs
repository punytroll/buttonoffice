namespace ButtonOffice
{
    internal interface ISaveable
    {
        System.Xml.XmlElement Save(ButtonOffice.SaveGameProcessor SaveGameProcessor);
    }
}

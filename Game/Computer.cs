namespace ButtonOffice
{
    public class Computer : ButtonOffice.PersistentObject
    {
        private System.Single _MinutesUntilBroken;

        public Computer()
        {
            _MinutesUntilBroken = ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer);
        }

        public System.Single GetMinutesUntilBroken()
        {
            return _MinutesUntilBroken;
        }

        public System.Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetMinutesUntilBroken(System.Single MinutesUntilBroken)
        {
            _MinutesUntilBroken = MinutesUntilBroken;
        }

        public System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("computer");

            Result.AppendChild(GameSaver.CreateProperty("minutes-until-broken", _MinutesUntilBroken));

            return Result;
        }

        public void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            _MinutesUntilBroken = GameLoader.LoadSingleProperty(Element, "minutes-until-broken");
        }
    }
}

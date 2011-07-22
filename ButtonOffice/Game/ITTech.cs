namespace ButtonOffice
{
    internal class ITTech : ButtonOffice.Person
    {
        private System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> _RepairingTarget;

        public ITTech() :
            base(ButtonOffice.Type.ITTech)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.ITTechBackgroundColor;
            _BorderColor = ButtonOffice.Data.ITTechBorderColor;
            _Goal = new ButtonOffice.Goals.ITTechThink();
            _RepairingTarget = null;
            _Wage = ButtonOffice.Data.ITTechWage;
            _WorkMinutes = ButtonOffice.Data.ITTechWorkMinutes;
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> GetRepairingTarget()
        {
            return _RepairingTarget;
        }

        public void SetRepairingTarget(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> RepairingTarget)
        {
            _RepairingTarget = RepairingTarget;
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);

            Result.AppendChild(GameSaver.CreateProperty("repairing-target", _RepairingTarget));

            return Result;
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
            _RepairingTarget = GameLoader.LoadBrokenThingProperty(Element, "repairing-target");
        }
    }
}

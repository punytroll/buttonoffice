namespace ButtonOffice
{
    internal class ITTech : ButtonOffice.Person
    {
        private System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> _RepairingTarget;

        public ITTech()
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.ITTechBackgroundColor;
            _BorderColor = ButtonOffice.Data.ITTechBorderColor;
            _RepairingTarget = null;
            _Wage = ButtonOffice.Data.ITTechWage;
            _WorkMinutes = ButtonOffice.Data.ITTechWorkMinutes;
            _Mind.SetRootGoal(new ButtonOffice.Goals.ITTechThink());
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> GetRepairingTarget()
        {
            return _RepairingTarget;
        }

        public void SetRepairingTarget(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> RepairingTarget)
        {
            _RepairingTarget = RepairingTarget;
        }

        public override void Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element)
        {
            base.Save(GameSaver, Element);
            Element.AppendChild(GameSaver.CreateProperty("repairing-target", _RepairingTarget));
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _RepairingTarget = ObjectStore.LoadBrokenThingProperty("repairing-target");
        }
    }
}

namespace ButtonOffice
{
    internal class Worker : ButtonOffice.Person
    {
        public Worker() :
            base(ButtonOffice.Type.Worker)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.WorkerBackgroundColor;
            _BorderColor = ButtonOffice.Data.WorkerBorderColor;
            _Goal = new ButtonOffice.Goals.WorkerThink();
            _Wage = ButtonOffice.Data.WorkerWage;
            _WorkMinutes = ButtonOffice.Data.WorkerWorkMinutes;
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            return base.Save(GameSaver);
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
        }
    }
}

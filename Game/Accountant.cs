namespace ButtonOffice
{
    public class Accountant : ButtonOffice.Person
    {
        private System.UInt64 _BonusPromille;

        public Accountant() :
            base(ButtonOffice.Type.Accountant)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.AccountantStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.AccountantBackgroundColor;
            _BorderColor = ButtonOffice.Data.AccountantBorderColor;
            _BonusPromille = ButtonOffice.Data.AccountantBonusPromille;
            _Wage = ButtonOffice.Data.AccountantWage;
            _WorkMinutes = ButtonOffice.Data.AccountantWorkMinutes;
            _Mind.SetRootGoal(new ButtonOffice.Goals.AccountantThink());
        }

        public System.UInt64 GetBonusPromille()
        {
            return _BonusPromille;
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);
            
            Result.AppendChild(GameSaver.CreateProperty("bonus-promille", _BonusPromille));

            return Result;
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
            _BonusPromille = GameLoader.LoadUInt64Property(Element, "bonus-promille");
        }
    }
}

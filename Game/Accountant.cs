namespace ButtonOffice
{
    public class Accountant : ButtonOffice.Person
    {
        private System.UInt64 _BonusPromille;

        public Accountant()
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

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("bonus-promille", _BonusPromille);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BonusPromille = ObjectStore.LoadUInt64Property("bonus-promille");
        }
    }
}

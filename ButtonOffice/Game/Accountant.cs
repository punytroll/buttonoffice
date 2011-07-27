namespace ButtonOffice
{
    internal class Accountant : ButtonOffice.Person
    {
        private System.UInt64 _BonusPromille;

        public Accountant() :
            base(ButtonOffice.Type.Accountant)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.AccountantStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.AccountantBackgroundColor;
            _BorderColor = ButtonOffice.Data.AccountantBorderColor;
            _BonusPromille = 50;
            _Goal = new ButtonOffice.Goals.AccountantThink();
            _Wage = ButtonOffice.Data.AccountantWage;
            _WorkMinutes = ButtonOffice.Data.AccountantWorkMinutes;
        }

        public System.UInt64 GetBonusPromille()
        {
            return _BonusPromille;
        }
    }
}

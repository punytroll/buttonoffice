using System;
using ButtonOffice.Goals;

namespace ButtonOffice
{
    public class Accountant : Person
    {
        private UInt64 _BonusPromille;

        public Accountant()
        {
            _ArrivesAtMinuteOfDay = RandomNumberGenerator.GetUInt32(Data.AccountantStartMinute, 300) % 1440;
            _BackgroundColor = Data.AccountantBackgroundColor;
            _BorderColor = Data.AccountantBorderColor;
            _BonusPromille = Data.AccountantBonusPromille;
            _Wage = Data.AccountantWage;
            _WorkMinutes = Data.AccountantWorkMinutes;
            
            var Mind = new GoalMind();
            
            Mind.SetRootGoal(new AccountantThink());
            _Mind = Mind;
        }

        public UInt64 GetBonusPromille()
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

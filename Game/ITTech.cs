using System;
using ButtonOffice.Goals;

namespace ButtonOffice
{
    internal class ITTech : Person
    {
        private PersistentObject _RepairingTarget;

        public ITTech()
        {
            _ArrivesAtMinuteOfDay = RandomNumberGenerator.GetUInt32(Data.ITTechStartMinute, 300) % 1440;
            _BackgroundColor = Data.ITTechBackgroundColor;
            _BorderColor = Data.ITTechBorderColor;
            _RepairingTarget = null;
            _Wage = Data.ITTechWage;
            _WorkMinutes = Data.ITTechWorkMinutes;
            
            var Mind = new GoalMind();
            
            Mind.SetRootGoal(new ITTechThink());
            _Mind = Mind;
        }

        public PersistentObject GetRepairingTarget()
        {
            return _RepairingTarget;
        }

        public void SetRepairingTarget(PersistentObject RepairingTarget)
        {
            _RepairingTarget = RepairingTarget;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("repairing-target", _RepairingTarget);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _RepairingTarget = ObjectStore.LoadObjectProperty("repairing-target");
        }
    }
}

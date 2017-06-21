using System;

namespace ButtonOffice
{
    public class Computer : PersistentObject
    {
        private Double _MinutesUntilBroken;

        public Computer()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetDoubleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer);
        }

        public Double GetMinutesUntilBroken()
        {
            return _MinutesUntilBroken;
        }

        public Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetMinutesUntilBroken(Double MinutesUntilBroken)
        {
            _MinutesUntilBroken = MinutesUntilBroken;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("minutes-until-broken", _MinutesUntilBroken);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _MinutesUntilBroken = ObjectStore.LoadDoubleProperty("minutes-until-broken");
        }
    }
}

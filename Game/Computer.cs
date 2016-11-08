using System;

namespace ButtonOffice
{
    public class Computer : PersistentObject
    {
        private Single _MinutesUntilBroken;

        public Computer()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer);
        }

        public Single GetMinutesUntilBroken()
        {
            return _MinutesUntilBroken;
        }

        public Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetMinutesUntilBroken(Single MinutesUntilBroken)
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
            _MinutesUntilBroken = ObjectStore.LoadSingleProperty("minutes-until-broken");
        }
    }
}

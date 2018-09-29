using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Computer : PersistentObject
    {
        private Double _MinutesUntilBroken;
        private RectangleF _Rectangle;

        public Computer()
        {
            _Rectangle.Width = Data.ComputerWidth;
            _Rectangle.Height = Data.ComputerHeight;
            _MinutesUntilBroken = RandomNumberGenerator.GetDoubleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer);
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public Single GetX()
        {
            return _Rectangle.X;
        }

        public Single GetY()
        {
            return _Rectangle.Y;
        }

        public Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetRepaired()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetDoubleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer);
        }

        public void Use(Double DeltaGameMinutes)
        {
            _MinutesUntilBroken -= DeltaGameMinutes;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("minutes-until-broken", _MinutesUntilBroken);
            ObjectStore.Save("rectangle", _Rectangle);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _MinutesUntilBroken = ObjectStore.LoadDoubleProperty("minutes-until-broken");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
        }
    }
}

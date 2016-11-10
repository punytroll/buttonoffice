using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Lamp : PersistentObject
    {
        private Single _MinutesUntilBroken;
        private RectangleF _Rectangle;

        public Lamp()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp);
            _Rectangle.Height = Data.LampHeight;
            _Rectangle.Width = Data.LampWidth;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public Single GetMinutesUntilBroken()
        {
            return _MinutesUntilBroken;
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

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetMinutesUntilBroken(Single MinutesUntilBroken)
        {
            _MinutesUntilBroken = MinutesUntilBroken;
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("minutes-until-broken", _MinutesUntilBroken);
            ObjectStore.Save("rectangle", _Rectangle);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _MinutesUntilBroken = ObjectStore.LoadSingleProperty("minutes-until-broken");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
        }
    }
}

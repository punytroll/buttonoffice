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

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.Single GetX()
        {
            return _Rectangle.X;
        }

        public System.Single GetY()
        {
            return _Rectangle.Y;
        }

        public System.Boolean IsBroken()
        {
            return _MinutesUntilBroken < 0.0f;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetMinutesUntilBroken(System.Single MinutesUntilBroken)
        {
            _MinutesUntilBroken = MinutesUntilBroken;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(System.Single Y)
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

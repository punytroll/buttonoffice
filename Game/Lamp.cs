using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Lamp : PersistentObject
    {
        private Double _Bottom;
        private Double _Height;
        private Double _Left;
        private Double _MinutesUntilBroken;
        private Office _Office;
        private Double _Width;

        public Color BackgroundColor
        {
            get
            {
                if(_MinutesUntilBroken > 0.0)
                {
                    return Color.Yellow;
                }
                else
                {
                    return Color.Gray;
                }
            }
        }

        public Double Bottom
        {
            set
            {
                _Bottom = value;
            }
        }

        public Double Height => _Height;

        public Double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
            }
        }

        public Office Office
        {
            get
            {
                return _Office;
            }
            set
            {
                if(value == null)
                {
                    Debug.Assert(_Office != null);
                    _Office = null;
                }
                else
                {
                    Debug.Assert(_Office == null);
                    _Office = value;
                }
            }
        }

        public Double Width => _Width;

        public Lamp()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetDoubleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp);
            _Office = null;
            _Height = Data.LampHeight;
            _Width = Data.LampWidth;
        }

        public RectangleF GetVisualRectangle()
        {
            return new RectangleF(Convert.ToSingle(_Left), Convert.ToSingle(_Bottom), Convert.ToSingle(_Width), Convert.ToSingle(_Height));
        }

        public void Update(Game Game, Double DeltaGameMinutes)
        {
            if(_MinutesUntilBroken > 0.0)
            {
                _MinutesUntilBroken -= DeltaGameMinutes;
                if(_MinutesUntilBroken <= 0.0)
                {
                    Game.EnqueueBrokenThing(this);
                }
            }
        }

        public void SetRepaired()
        {
            _MinutesUntilBroken = RandomNumberGenerator.GetDoubleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("bottom", _Bottom);
            ObjectStore.Save("height", _Height);
            ObjectStore.Save("left", _Left);
            ObjectStore.Save("minutes-until-broken", _MinutesUntilBroken);
            ObjectStore.Save("office", _Office);
            ObjectStore.Save("width", _Width);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Bottom = ObjectStore.LoadDoubleProperty("bottom");
            _Height = ObjectStore.LoadDoubleProperty("height");
            _Left = ObjectStore.LoadDoubleProperty("left");
            _MinutesUntilBroken = ObjectStore.LoadDoubleProperty("minutes-until-broken");
            _Office = ObjectStore.LoadOfficeProperty("office");
            _Width = ObjectStore.LoadDoubleProperty("width");
        }
    }
}

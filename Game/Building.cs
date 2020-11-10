using System;
using System.Drawing;

namespace ButtonOffice
{
    public abstract class Building : PersistentObject
    {
        protected Color _BackgroundColor;
        protected Color _BorderColor;
        private Double _Floor;
        private Double _Height;
        private Double _Left;
        private Double _Width;

        public Color BackgroundColor => _BackgroundColor;

        public Color BorderColor => _BorderColor;

        public Double Floor
        {
            get
            {
                return _Floor;
            }
            set
            {
                _Floor = value;
                _UpdateInterior();
            }
        }

        public Double Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
                _UpdateInterior();
            }
        }

        public Double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
                _UpdateInterior();
            }
        }

        public Double Right => _Left + _Width;

        public Double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                _UpdateInterior();
            }
        }

        public RectangleF GetVisualRectangle()
        {
            return new RectangleF(Convert.ToSingle(_Left), Convert.ToSingle(_Floor), Convert.ToSingle(_Width), Convert.ToSingle(_Height));
        }

        public Boolean Contains(Vector2 Location)
        {
            return (Location.X >= _Left) && (Location.X <= _Left + _Width) && (Location.Y >= _Floor) && (Location.Y <= _Floor + _Height);
        }

        public Vector2 GetMidLocation()
        {
            return new Vector2(_Left + _Width / 2.0, _Floor + _Height / 2.0);
        }

        public abstract Boolean CanDestroy();

        protected virtual void _UpdateInterior()
        {
        }

        public virtual void Update(Game Game, Double DeltaGameMinutes)
        {
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("floor", _Floor);
            ObjectStore.Save("height", _Height);
            ObjectStore.Save("left", _Left);
            ObjectStore.Save("width", _Width);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Floor = ObjectStore.LoadDoubleProperty("floor");
            _Height = ObjectStore.LoadDoubleProperty("height");
            _Left = ObjectStore.LoadDoubleProperty("left");
            _Width = ObjectStore.LoadDoubleProperty("width");
        }
    }
}

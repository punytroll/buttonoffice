using System;
using System.Diagnostics;
using System.Drawing;
using ButtonOffice.Goals;

namespace ButtonOffice
{
    public class Cat : PersistentObject
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Double _Height;
        private Mind _Mind;
        private Office _Office;
        private Double _Width;
        private Double _X;
        private Double _Y;

        public Color BackgroundColor => _BackgroundColor;

        public Color BorderColor => _BorderColor;

        public Office Office => _Office;

        public Cat()
        {
            _BackgroundColor = Data.CatBackgroundColor;
            _BorderColor = Data.CatBorderColor;
            _Height = Data.CatHeight;
            _Mind = new Mind();
            _Mind.SetRootGoal(new CatThink());
            _Width = Data.CatWidth;
        }

        public void AssignOffice(Office Office)
        {
            Debug.Assert(Office != null);
            if(_Office != null)
            {
                Debug.Assert(_Office.Cat == this);
                _Office.Cat = null;
            }
            _Office = Office;
            _Office.Cat = this;
        }

        public RectangleF GetVisualRectangle()
        {
            return new RectangleF(Convert.ToSingle(_X - _Width / 2.0f), Convert.ToSingle(_Y), Convert.ToSingle(_Width), Convert.ToSingle(_Height));
        }

        public Double GetLeft()
        {
            return _X - _Width / 2.0;
        }

        public Double GetRight()
        {
            return _X + _Width / 2.0;
        }

        public Double GetX()
        {
            return _X;
        }

        public void SetX(Double X)
        {
            _X = X;
        }

        public void SetY(Double Y)
        {
            _Y = Y;
        }

        public void Move(Game Game, Double DeltaGameMinutes)
        {
            _Mind.Move(Game, this, DeltaGameMinutes);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("height", _Height);
            ObjectStore.Save("mind", _Mind);
            ObjectStore.Save("office", _Office);
            ObjectStore.Save("width", _Width);
            ObjectStore.Save("x", _X);
            ObjectStore.Save("y", _Y);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Height = ObjectStore.LoadDoubleProperty("height");
            _Mind = ObjectStore.LoadMindProperty("mind");
            _Office = ObjectStore.LoadOfficeProperty("office");
            _Width = ObjectStore.LoadDoubleProperty("width");
            _X = ObjectStore.LoadDoubleProperty("x");
            _Y = ObjectStore.LoadDoubleProperty("y");
        }
    }
}

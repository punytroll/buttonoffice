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
        private Mind _Mind;
        private Office _Office;
        private RectangleF _Rectangle;

        public Color BackgroundColor => _BackgroundColor;

        public Color BorderColor => _BorderColor;

        public Office Office => _Office;

        public Cat()
        {
            _BackgroundColor = Data.CatBackgroundColor;
            _BorderColor = Data.CatBorderColor;
            _Mind = new Mind();
            _Mind.SetRootGoal(new CatThink());
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

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public Single GetRight()
        {
            return _Rectangle.Right;
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

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetRectangle(RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
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

        public void Move(Game Game, Double DeltaGameMinutes)
        {
            _Mind.Move(Game, this, DeltaGameMinutes);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("mind", _Mind);
            ObjectStore.Save("office", _Office);
            ObjectStore.Save("rectangle", _Rectangle);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Mind = ObjectStore.LoadMindProperty("mind");
            _Office = ObjectStore.LoadOfficeProperty("office");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
        }
    }
}

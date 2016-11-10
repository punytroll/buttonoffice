using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Office : PersistentObject
    {
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Cat _Cat;
        private Desk _FirstDesk;
        private Lamp _FirstLamp;
        private Desk _FourthDesk;
        private RectangleF _Rectangle;
        private Desk _SecondDesk;
        private Lamp _SecondLamp;
        private Desk _ThirdDesk;
        private Lamp _ThirdLamp;

        public Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
        }

        public Cat Cat
        {
            get
            {
                return _Cat;
            }
            set
            {
                _Cat = value;
            }
        }

        public Desk FirstDesk
        {
            get
            {
                return _FirstDesk;
            }
        }

        public Desk SecondDesk
        {
            get
            {
                return _SecondDesk;
            }
        }

        public Desk ThirdDesk
        {
            get
            {
                return _ThirdDesk;
            }
        }

        public Desk FourthDesk
        {
            get
            {
                return _FourthDesk;
            }
        }

        public Lamp FirstLamp
        {
            get
            {
                return _FirstLamp;
            }
        }

        public Lamp SecondLamp
        {
            get
            {
                return _SecondLamp;
            }
        }

        public Lamp ThirdLamp
        {
            get
            {
                return _ThirdLamp;
            }
        }

        public Office()
        {
            _BackgroundColor = Data.OfficeBackgroundColor;
            _BorderColor = Data.OfficeBorderColor;
            _FirstDesk = new Desk();
            _FirstDesk.Office = this;
            _SecondDesk = new Desk();
            _SecondDesk.Office = this;
            _ThirdDesk = new Desk();
            _ThirdDesk.Office = this;
            _FourthDesk = new Desk();
            _FourthDesk.Office = this;
            _FirstLamp = new Lamp();
            _SecondLamp = new Lamp();
            _ThirdLamp = new Lamp();
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public PointF GetMidLocation()
        {
            return _Rectangle.GetMidPoint();
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
            _UpdateInterior();
        }

        public void SetRectangle(RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
            _UpdateInterior();
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
            _UpdateInterior();
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
            _UpdateInterior();
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
            _UpdateInterior();
        }

        private void _UpdateInterior()
        {
            _FirstDesk.SetLocation(_Rectangle.X + Data.DeskOneX, _Rectangle.Y);
            _SecondDesk.SetLocation(_Rectangle.X + Data.DeskTwoX, _Rectangle.Y);
            _ThirdDesk.SetLocation(_Rectangle.X + Data.DeskThreeX, _Rectangle.Y);
            _FourthDesk.SetLocation(_Rectangle.X + Data.DeskFourX, _Rectangle.Y);
            _FirstLamp.SetLocation(_Rectangle.X + Data.LampOneX, _Rectangle.Y + _Rectangle.Height - _FirstLamp.GetHeight());
            _SecondLamp.SetLocation(_Rectangle.X + Data.LampTwoX, _Rectangle.Y + _Rectangle.Height - _SecondLamp.GetHeight());
            _ThirdLamp.SetLocation(_Rectangle.X + Data.LampThreeX, _Rectangle.Y + _Rectangle.Height - _ThirdLamp.GetHeight());
        }

        public void Move(Game Game, Single GameMinutes)
        {
            if(_FirstLamp.IsBroken() == false)
            {
                _FirstLamp.SetMinutesUntilBroken(_FirstLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_FirstLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(this, BrokenThing.FirstLamp));
                }
            }
            if(_SecondLamp.IsBroken() == false)
            {
                _SecondLamp.SetMinutesUntilBroken(_SecondLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_SecondLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(this, BrokenThing.SecondLamp));
                }
            }
            if(_ThirdLamp.IsBroken() == false)
            {
                _ThirdLamp.SetMinutesUntilBroken(_ThirdLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_ThirdLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(this, BrokenThing.ThirdLamp));
                }
            }
            if(_Cat != null)
            {
                _Cat.Move(Game, GameMinutes);
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("cat", _Cat);
            ObjectStore.Save("first-desk", _FirstDesk);
            ObjectStore.Save("first-lamp", _FirstLamp);
            ObjectStore.Save("fourth-desk", _FourthDesk);
            ObjectStore.Save("rectangle", _Rectangle);
            ObjectStore.Save("second-desk", _SecondDesk);
            ObjectStore.Save("second-lamp", _SecondLamp);
            ObjectStore.Save("third-desk", _ThirdDesk);
            ObjectStore.Save("third-lamp", _ThirdLamp);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _Cat = ObjectStore.LoadCatProperty("cat");
            _FirstDesk = ObjectStore.LoadDeskProperty("first-desk");
            _FirstLamp = ObjectStore.LoadLampProperty("first-lamp");
            _FourthDesk = ObjectStore.LoadDeskProperty("fourth-desk");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
            _SecondDesk = ObjectStore.LoadDeskProperty("second-desk");
            _SecondLamp = ObjectStore.LoadLampProperty("second-lamp");
            _ThirdDesk = ObjectStore.LoadDeskProperty("third-desk");
            _ThirdLamp = ObjectStore.LoadLampProperty("third-lamp");
        }
    }
}

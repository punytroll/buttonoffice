namespace ButtonOffice
{
    public class Office : ButtonOffice.PersistentObject
    {
        private System.Drawing.Color _BackgroundColor;
        private System.Drawing.Color _BorderColor;
        private ButtonOffice.Cat _Cat;
        private ButtonOffice.Desk _FirstDesk;
        private ButtonOffice.Lamp _FirstLamp;
        private ButtonOffice.Desk _FourthDesk;
        private System.Drawing.RectangleF _Rectangle;
        private ButtonOffice.Desk _SecondDesk;
        private ButtonOffice.Lamp _SecondLamp;
        private ButtonOffice.Desk _ThirdDesk;
        private ButtonOffice.Lamp _ThirdLamp;

        public System.Drawing.Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
        }

        public ButtonOffice.Cat Cat
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

        public ButtonOffice.Desk FirstDesk
        {
            get
            {
                return _FirstDesk;
            }
        }

        public ButtonOffice.Desk SecondDesk
        {
            get
            {
                return _SecondDesk;
            }
        }

        public ButtonOffice.Desk ThirdDesk
        {
            get
            {
                return _ThirdDesk;
            }
        }

        public ButtonOffice.Desk FourthDesk
        {
            get
            {
                return _FourthDesk;
            }
        }

        public ButtonOffice.Lamp FirstLamp
        {
            get
            {
                return _FirstLamp;
            }
        }

        public ButtonOffice.Lamp SecondLamp
        {
            get
            {
                return _SecondLamp;
            }
        }

        public ButtonOffice.Lamp ThirdLamp
        {
            get
            {
                return _ThirdLamp;
            }
        }

        public Office()
        {
            _BackgroundColor = ButtonOffice.Data.OfficeBackgroundColor;
            _BorderColor = ButtonOffice.Data.OfficeBorderColor;
            _FirstDesk = new Desk();
            _FirstDesk.Office = this;
            _SecondDesk = new Desk();
            _SecondDesk.Office = this;
            _ThirdDesk = new Desk();
            _ThirdDesk.Office = this;
            _FourthDesk = new Desk();
            _FourthDesk.Office = this;
            _FirstLamp = new ButtonOffice.Lamp();
            _SecondLamp = new ButtonOffice.Lamp();
            _ThirdLamp = new ButtonOffice.Lamp();
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetMidLocation()
        {
            return _Rectangle.GetMidPoint();
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.Single GetRight()
        {
            return _Rectangle.Right;
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

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
            _UpdateInterior();
        }

        public void SetRectangle(System.Drawing.RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
            _UpdateInterior();
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
            _UpdateInterior();
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
            _UpdateInterior();
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
            _UpdateInterior();
        }

        private void _UpdateInterior()
        {
            _FirstDesk.SetLocation(_Rectangle.X + ButtonOffice.Data.DeskOneX, _Rectangle.Y);
            _SecondDesk.SetLocation(_Rectangle.X + ButtonOffice.Data.DeskTwoX, _Rectangle.Y);
            _ThirdDesk.SetLocation(_Rectangle.X + ButtonOffice.Data.DeskThreeX, _Rectangle.Y);
            _FourthDesk.SetLocation(_Rectangle.X + ButtonOffice.Data.DeskFourX, _Rectangle.Y);
            _FirstLamp.SetLocation(_Rectangle.X + ButtonOffice.Data.LampOneX, _Rectangle.Y + _Rectangle.Height - _FirstLamp.GetHeight());
            _SecondLamp.SetLocation(_Rectangle.X + ButtonOffice.Data.LampTwoX, _Rectangle.Y + _Rectangle.Height - _SecondLamp.GetHeight());
            _ThirdLamp.SetLocation(_Rectangle.X + ButtonOffice.Data.LampThreeX, _Rectangle.Y + _Rectangle.Height - _ThirdLamp.GetHeight());
        }

        public void Move(ButtonOffice.Game Game, System.Single GameMinutes)
        {
            if(_FirstLamp.IsBroken() == false)
            {
                _FirstLamp.SetMinutesUntilBroken(_FirstLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_FirstLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(this, ButtonOffice.BrokenThing.FirstLamp));
                }
            }
            if(_SecondLamp.IsBroken() == false)
            {
                _SecondLamp.SetMinutesUntilBroken(_SecondLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_SecondLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(this, ButtonOffice.BrokenThing.SecondLamp));
                }
            }
            if(_ThirdLamp.IsBroken() == false)
            {
                _ThirdLamp.SetMinutesUntilBroken(_ThirdLamp.GetMinutesUntilBroken() - GameMinutes);
                if(_ThirdLamp.IsBroken() == true)
                {
                    Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(this, ButtonOffice.BrokenThing.ThirdLamp));
                }
            }
            if(_Cat != null)
            {
                _Cat.Move(Game, GameMinutes);
            }
        }

        public virtual void Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element)
        {
            Element.AppendChild(GameSaver.CreateProperty("background-color", _BackgroundColor));
            Element.AppendChild(GameSaver.CreateProperty("border-color", _BorderColor));
            Element.AppendChild(GameSaver.CreateProperty("cat", _Cat));
            Element.AppendChild(GameSaver.CreateProperty("first-desk", _FirstDesk));
            Element.AppendChild(GameSaver.CreateProperty("first-lamp", _FirstLamp));
            Element.AppendChild(GameSaver.CreateProperty("fourth-desk", _FourthDesk));
            Element.AppendChild(GameSaver.CreateProperty("rectangle", _Rectangle));
            Element.AppendChild(GameSaver.CreateProperty("second-desk", _SecondDesk));
            Element.AppendChild(GameSaver.CreateProperty("second-lamp", _SecondLamp));
            Element.AppendChild(GameSaver.CreateProperty("third-desk", _ThirdDesk));
            Element.AppendChild(GameSaver.CreateProperty("third-lamp", _ThirdLamp));
        }

        public virtual void Load(LoadObjectStore ObjectStore)
        {
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

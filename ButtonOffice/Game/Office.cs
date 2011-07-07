namespace ButtonOffice
{
    internal class Office
    {
        private System.Drawing.RectangleF _Rectangle;
        private ButtonOffice.Desk _FirstDesk;
        private ButtonOffice.Desk _SecondDesk;
        private ButtonOffice.Desk _ThirdDesk;
        private ButtonOffice.Desk _FourthDesk;
        private ButtonOffice.Lamp _FirstLamp;
        private ButtonOffice.Lamp _SecondLamp;
        private ButtonOffice.Lamp _ThirdLamp;

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
            _FirstDesk = new Desk();
            _FirstDesk.Office = this;
            _FirstDesk.SetHeight(ButtonOffice.Data.DeskHeight);
            _FirstDesk.SetWidth(ButtonOffice.Data.DeskWidth);
            _SecondDesk = new Desk();
            _SecondDesk.Office = this;
            _SecondDesk.SetHeight(ButtonOffice.Data.DeskHeight);
            _SecondDesk.SetWidth(ButtonOffice.Data.DeskWidth);
            _ThirdDesk = new Desk();
            _ThirdDesk.Office = this;
            _ThirdDesk.SetHeight(ButtonOffice.Data.DeskHeight);
            _ThirdDesk.SetWidth(ButtonOffice.Data.DeskWidth);
            _FourthDesk = new Desk();
            _FourthDesk.Office = this;
            _FourthDesk.SetHeight(ButtonOffice.Data.DeskHeight);
            _FourthDesk.SetWidth(ButtonOffice.Data.DeskWidth);
            _FirstLamp = new ButtonOffice.Lamp();
            _FirstLamp.SetHeight(ButtonOffice.Data.LampHeight);
            _FirstLamp.SetWidth(ButtonOffice.Data.LampWidth);
            _SecondLamp = new ButtonOffice.Lamp();
            _SecondLamp.SetHeight(ButtonOffice.Data.LampHeight);
            _SecondLamp.SetWidth(ButtonOffice.Data.LampWidth);
            _ThirdLamp = new ButtonOffice.Lamp();
            _ThirdLamp.SetHeight(ButtonOffice.Data.LampHeight);
            _ThirdLamp.SetWidth(ButtonOffice.Data.LampWidth);
        }

        public ButtonOffice.Desk GetFreeDesk()
        {
            if(_FirstDesk.Person == null)
            {
                return _FirstDesk;
            }
            else if(_SecondDesk.Person == null)
            {
                return _SecondDesk;
            }
            else if(_ThirdDesk.Person == null)
            {
                return _ThirdDesk;
            }
            else if(_FourthDesk.Person == null)
            {
                return _FourthDesk;
            }
            else
            {
                return null;
            }
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.RectangleF GetRectangle()
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

        public System.Boolean HasFreeDesk()
        {
            return (_FirstDesk.Person == null) || (_SecondDesk.Person == null) || (_ThirdDesk.Person == null) || (_FourthDesk.Person == null);
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
            _FirstDesk.SetX(X + ButtonOffice.Data.DeskOneX);
            _SecondDesk.SetX(X + ButtonOffice.Data.DeskTwoX);
            _ThirdDesk.SetX(X + ButtonOffice.Data.DeskThreeX);
            _FourthDesk.SetX(X + ButtonOffice.Data.DeskFourX);
            _FirstLamp.SetX(X + ButtonOffice.Data.LampOneX);
            _SecondLamp.SetX(X + ButtonOffice.Data.LampTwoX);
            _ThirdLamp.SetX(X + ButtonOffice.Data.LampThreeX);
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
            _FirstDesk.SetY(Y);
            _SecondDesk.SetY(Y);
            _ThirdDesk.SetY(Y);
            _FourthDesk.SetY(Y);
            _FirstLamp.SetY(Y + GetHeight() - _FirstLamp.GetHeight());
            _SecondLamp.SetY(Y + GetHeight() - _SecondLamp.GetHeight());
            _ThirdLamp.SetY(Y + GetHeight() - _ThirdLamp.GetHeight());
        }
    }
}

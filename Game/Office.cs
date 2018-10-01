using System;

namespace ButtonOffice
{
    public class Office : Building
    {
        private Cat _Cat;
        private Desk _FirstDesk;
        private Lamp _FirstLamp;
        private Desk _FourthDesk;
        private Desk _SecondDesk;
        private Lamp _SecondLamp;
        private Desk _ThirdDesk;
        private Lamp _ThirdLamp;

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

        public Desk FirstDesk => _FirstDesk;

        public Desk SecondDesk => _SecondDesk;

        public Desk ThirdDesk => _ThirdDesk;

        public Desk FourthDesk => _FourthDesk;

        public Lamp FirstLamp => _FirstLamp;

        public Lamp SecondLamp => _SecondLamp;

        public Lamp ThirdLamp => _ThirdLamp;

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
            _FirstLamp.Office = this;
            _SecondLamp = new Lamp();
            _SecondLamp.Office = this;
            _ThirdLamp = new Lamp();
            _ThirdLamp.Office = this;
        }

        protected override void _UpdateInterior()
        {
            _FirstDesk.SetLocation(Left + Data.DeskOneX, Floor);
            _FirstDesk.Computer.SetLocation(_FirstDesk.GetX() + (_FirstDesk.GetWidth() - _FirstDesk.Computer.GetWidth()) / 2.0f, _FirstDesk.GetY() +_FirstDesk.GetHeight() + 0.04f);
            _SecondDesk.SetLocation(Left + Data.DeskTwoX, Floor);
            _SecondDesk.Computer.SetLocation(_SecondDesk.GetX() + (_SecondDesk.GetWidth() - _SecondDesk.Computer.GetWidth()) / 2.0f, _SecondDesk.GetY() + _SecondDesk.GetHeight() + 0.04f);
            _ThirdDesk.SetLocation(Left + Data.DeskThreeX, Floor);
            _ThirdDesk.Computer.SetLocation(_ThirdDesk.GetX() + (_ThirdDesk.GetWidth() - _ThirdDesk.Computer.GetWidth()) / 2.0f, _ThirdDesk.GetY() + _ThirdDesk.GetHeight() + 0.04f);
            _FourthDesk.SetLocation(Left + Data.DeskFourX, Floor);
            _FourthDesk.Computer.SetLocation(_FourthDesk.GetX() + (_FourthDesk.GetWidth() - _FourthDesk.Computer.GetWidth()) / 2.0f, _FourthDesk.GetY() + _FourthDesk.GetHeight() + 0.04f);
            _FirstLamp.Left = Left + Data.LampOneX;
            _FirstLamp.Bottom = Floor + Height - _FirstLamp.Height;
            _SecondLamp.Left = Left + Data.LampTwoX;
            _SecondLamp.Bottom = Floor + Height - _SecondLamp.Height;
            _ThirdLamp.Left = Left + Data.LampThreeX;
            _ThirdLamp.Bottom = Floor + Height - _ThirdLamp.Height;
        }

        public override void Move(Game Game, Double DeltaGameMinutes)
        {
            _FirstLamp.Move(Game, DeltaGameMinutes);
            _SecondLamp.Move(Game, DeltaGameMinutes);
            _ThirdLamp.Move(Game, DeltaGameMinutes);
            _Cat?.Move(Game, DeltaGameMinutes);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("cat", _Cat);
            ObjectStore.Save("first-desk", _FirstDesk);
            ObjectStore.Save("first-lamp", _FirstLamp);
            ObjectStore.Save("fourth-desk", _FourthDesk);
            ObjectStore.Save("second-desk", _SecondDesk);
            ObjectStore.Save("second-lamp", _SecondLamp);
            ObjectStore.Save("third-desk", _ThirdDesk);
            ObjectStore.Save("third-lamp", _ThirdLamp);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Cat = ObjectStore.LoadCatProperty("cat");
            _FirstDesk = ObjectStore.LoadDeskProperty("first-desk");
            _FirstLamp = ObjectStore.LoadLampProperty("first-lamp");
            _FourthDesk = ObjectStore.LoadDeskProperty("fourth-desk");
            _SecondDesk = ObjectStore.LoadDeskProperty("second-desk");
            _SecondLamp = ObjectStore.LoadLampProperty("second-lamp");
            _ThirdDesk = ObjectStore.LoadDeskProperty("third-desk");
            _ThirdLamp = ObjectStore.LoadLampProperty("third-lamp");
        }
    }
}

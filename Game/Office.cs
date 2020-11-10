using System;

namespace ButtonOffice
{
    public class Office : Building
    {
        public Cat Cat
        {
            get;
            set;
        }

        public Desk FirstDesk
        {
            get;
            private set;
        }

        public Desk SecondDesk
        {
            get;
            private set;
        }

        public Desk ThirdDesk
        {
            get;
            private set;
        }

        public Desk FourthDesk
        {
            get;
            private set;
        }

        public Lamp FirstLamp
        {
            get;
            private set;
        }

        public Lamp SecondLamp
        {
            get;
            private set;
        }

        public Lamp ThirdLamp
        {
            get;
            private set;
        }

        public Office()
        {
            _BackgroundColor = Data.OfficeBackgroundColor;
            _BorderColor = Data.OfficeBorderColor;
            FirstDesk = new Desk();
            FirstDesk.Office = this;
            SecondDesk = new Desk();
            SecondDesk.Office = this;
            ThirdDesk = new Desk();
            ThirdDesk.Office = this;
            FourthDesk = new Desk();
            FourthDesk.Office = this;
            FirstLamp = new Lamp();
            FirstLamp.Office = this;
            SecondLamp = new Lamp();
            SecondLamp.Office = this;
            ThirdLamp = new Lamp();
            ThirdLamp.Office = this;
        }

        public override Boolean CanDestroy()
        {
            return (FirstDesk.IsFree() == true) && (SecondDesk.IsFree() == true) && (ThirdDesk.IsFree() == true) && (FourthDesk.IsFree() == true);
        }

        protected override void _UpdateInterior()
        {
            FirstDesk.SetLocation(Left + Data.DeskOneX, Floor);
            FirstDesk.Computer.SetLocation(FirstDesk.GetX() + (FirstDesk.GetWidth() - FirstDesk.Computer.GetWidth()) / 2.0f, FirstDesk.GetY() + FirstDesk.GetHeight() + 0.04f);
            SecondDesk.SetLocation(Left + Data.DeskTwoX, Floor);
            SecondDesk.Computer.SetLocation(SecondDesk.GetX() + (SecondDesk.GetWidth() - SecondDesk.Computer.GetWidth()) / 2.0f, SecondDesk.GetY() + SecondDesk.GetHeight() + 0.04f);
            ThirdDesk.SetLocation(Left + Data.DeskThreeX, Floor);
            ThirdDesk.Computer.SetLocation(ThirdDesk.GetX() + (ThirdDesk.GetWidth() - ThirdDesk.Computer.GetWidth()) / 2.0f, ThirdDesk.GetY() + ThirdDesk.GetHeight() + 0.04f);
            FourthDesk.SetLocation(Left + Data.DeskFourX, Floor);
            FourthDesk.Computer.SetLocation(FourthDesk.GetX() + (FourthDesk.GetWidth() - FourthDesk.Computer.GetWidth()) / 2.0f, FourthDesk.GetY() + FourthDesk.GetHeight() + 0.04f);
            FirstLamp.Left = Left + Data.LampOneX;
            FirstLamp.Bottom = Floor + Height - FirstLamp.Height;
            SecondLamp.Left = Left + Data.LampTwoX;
            SecondLamp.Bottom = Floor + Height - SecondLamp.Height;
            ThirdLamp.Left = Left + Data.LampThreeX;
            ThirdLamp.Bottom = Floor + Height - ThirdLamp.Height;
        }

        public override void Update(Game Game, Double DeltaGameMinutes)
        {
            FirstLamp.Update(Game, DeltaGameMinutes);
            SecondLamp.Update(Game, DeltaGameMinutes);
            ThirdLamp.Update(Game, DeltaGameMinutes);
            Cat?.Update(Game, DeltaGameMinutes);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("cat", Cat);
            ObjectStore.Save("first-desk", FirstDesk);
            ObjectStore.Save("first-lamp", FirstLamp);
            ObjectStore.Save("fourth-desk", FourthDesk);
            ObjectStore.Save("second-desk", SecondDesk);
            ObjectStore.Save("second-lamp", SecondLamp);
            ObjectStore.Save("third-desk", ThirdDesk);
            ObjectStore.Save("third-lamp", ThirdLamp);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            Cat = ObjectStore.LoadCatProperty("cat");
            FirstDesk = ObjectStore.LoadDeskProperty("first-desk");
            FirstLamp = ObjectStore.LoadLampProperty("first-lamp");
            FourthDesk = ObjectStore.LoadDeskProperty("fourth-desk");
            SecondDesk = ObjectStore.LoadDeskProperty("second-desk");
            SecondLamp = ObjectStore.LoadLampProperty("second-lamp");
            ThirdDesk = ObjectStore.LoadDeskProperty("third-desk");
            ThirdLamp = ObjectStore.LoadLampProperty("third-lamp");
        }
    }
}

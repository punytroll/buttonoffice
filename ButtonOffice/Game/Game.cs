namespace ButtonOffice
{
    internal class Game : ButtonOffice.PersistentObject
    {
        public delegate void MoneyChangeDelegate(System.UInt64 Cents, System.Drawing.PointF Location);
        public event MoneyChangeDelegate OnEarnMoney;
        public event MoneyChangeDelegate OnSpendMoney;

        private System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> _BrokenThings;
        private System.UInt32 _CatStock;
        private System.UInt64 _Cents;
        private System.Collections.Generic.List<System.Collections.BitArray> _FreeSpace;
        private System.Collections.Generic.List<System.Pair<System.Int32, System.Int32>> _BuildingMinimumMaximum;
        private System.UInt64 _Minutes;
        private System.UInt32 _NextCatAtNumberOfEmployees;
        private System.Collections.Generic.List<ButtonOffice.Office> _Offices;
        private System.Collections.Generic.List<ButtonOffice.Person> _Persons;
        private System.Single _SubMinute;

        public System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> BrokenThings
        {
            get
            {
                return _BrokenThings;
            }
        }

        public System.Collections.Generic.List<ButtonOffice.Office> Offices
        {
            get
            {
                return _Offices;
            }
        }

        public System.Collections.Generic.List<ButtonOffice.Person> Persons
        {
            get
            {
                return _Persons;
            }
        }

        public static ButtonOffice.Game CreateNew()
        {
            ButtonOffice.Game Result = new ButtonOffice.Game();

            Result._CatStock = 0;
            Result._Cents = ButtonOffice.Data.StartCents;
            for(System.Int32 Index = 0; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                Result._FreeSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, true));
            }
            for(System.Int32 Index = 0; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                Result._BuildingMinimumMaximum.Add(new System.Pair<System.Int32, System.Int32>(System.Int32.MaxValue, System.Int32.MinValue));
            }
            Result._Minutes = ButtonOffice.Data.StartMinutes;
            Result._NextCatAtNumberOfEmployees = 20;
            Result._SubMinute = 0.0f;

            return Result;
        }

        public static ButtonOffice.Game LoadFromFileName(System.String FileName)
        {
            ButtonOffice.GameLoader GameLoader = new GameLoader(FileName);
            ButtonOffice.Game Result = new ButtonOffice.Game();

            GameLoader.Load(Result);

            return Result;
        }

        private Game()
        {
            _BrokenThings = new System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>>();
            _FreeSpace = new System.Collections.Generic.List<System.Collections.BitArray>();
            _BuildingMinimumMaximum = new System.Collections.Generic.List<System.Pair<System.Int32, System.Int32>>();
            _Offices = new System.Collections.Generic.List<ButtonOffice.Office>();
            _Persons = new System.Collections.Generic.List<ButtonOffice.Person>();
        }

        public void Move(System.Single GameMinutes)
        {
            _SubMinute += GameMinutes;
            while(_SubMinute >= 1.0f)
            {
                _Minutes += 1;
                _SubMinute -= 1.0f;
            }
            foreach(ButtonOffice.Office Office in _Offices)
            {
                Office.Move(this, GameMinutes);
            }
            foreach(ButtonOffice.Person Person in _Persons)
            {
                Person.Move(this, GameMinutes);
            }
        }

        public System.UInt32 GetCatStock()
        {
            return _CatStock;
        }

        public void AddCents(System.UInt64 Cents)
        {
            _Cents += Cents;
        }

        public void SubtractCents(System.UInt64 Cents)
        {
            _Cents -= Cents;
        }

        public System.UInt64 GetDay()
        {
            return _Minutes / 1440;
        }

        public System.UInt64 GetTotalMinutes()
        {
            return _Minutes;
        }

        public System.UInt64 GetMinuteOfDay()
        {
            return _Minutes % 1440;
        }

        public System.UInt64 GetFirstMinuteOfDay(System.UInt64 Day)
        {
            return Day * 1440;
        }

        public System.UInt64 GetFirstMinuteOfToday()
        {
            return GetDay() * 1440;
        }

        public System.Boolean BuildOffice(System.Drawing.RectangleF Rectangle)
        {
            if((Rectangle.Y.GetFloored() >= 0.0f) && (Rectangle.X.GetFloored() >= 0.0f) && (Rectangle.X.GetFlooredAsInt32() < ButtonOffice.Data.WorldBlockWidth))
            {
                System.Boolean BuildAllowed = true;

                for(System.Int32 Column = 0; Column < Rectangle.Width; ++Column)
                {
                    BuildAllowed &= _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column];
                }
                if(Rectangle.Y.GetFlooredAsInt32() > 0)
                {
                    BuildAllowed &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].First <= Rectangle.X.GetFlooredAsInt32();
                    BuildAllowed &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].Second >= Rectangle.Right.GetFlooredAsInt32();
                }
                if((BuildAllowed == true) && (_Cents >= ButtonOffice.Data.OfficeBuildCost))
                {
                    _Cents -= ButtonOffice.Data.OfficeBuildCost;
                    for(System.Int32 Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
                    {
                        _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column] = false;
                    }
                    if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First > Rectangle.X.GetFlooredAsInt32())
                    {
                        _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First = Rectangle.X.GetFlooredAsInt32();
                    }
                    if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second < Rectangle.Right.GetFlooredAsInt32())
                    {
                        _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second = Rectangle.Right.GetFlooredAsInt32();
                    }

                    Office Office = new Office();

                    Office.SetRectangle(Rectangle);
                    Office.FirstDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.SecondDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.ThirdDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.FourthDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.FirstLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                    Office.SecondLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                    Office.ThirdLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                    _Offices.Add(Office);
                    FireSpendMoney(ButtonOffice.Data.OfficeBuildCost, Office.GetMidLocation());

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public System.Boolean HireWorker(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.WorkerHireCost)
            {
                ButtonOffice.Office Office = _GetOffice(Rectangle.Location);

                if(Office != null)
                {
                    ButtonOffice.Desk Desk = _GetDesk(Office, Rectangle.Location);

                    if((Desk != null) && (Desk.IsFree() == true))
                    {
                        _Cents -= ButtonOffice.Data.WorkerHireCost;

                        ButtonOffice.Worker Worker = new ButtonOffice.Worker();

                        Worker.AssignDesk(Desk);
                        _Persons.Add(Worker);
                        if(_Persons.Count == _NextCatAtNumberOfEmployees)
                        {
                            _NextCatAtNumberOfEmployees += 20;
                            _CatStock += 1;
                        }
                        FireSpendMoney(ButtonOffice.Data.WorkerHireCost, Desk.GetMidLocation());

                        return true;
                    }
                }
            }

            return false;
        }

        public System.Boolean HireITTech(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.ITTechHireCost)
            {
                ButtonOffice.Office Office = _GetOffice(Rectangle.Location);

                if(Office != null)
                {
                    ButtonOffice.Desk Desk = _GetDesk(Office, Rectangle.Location);

                    if((Desk != null) && (Desk.IsFree() == true))
                    {
                        _Cents -= ButtonOffice.Data.ITTechHireCost;

                        ButtonOffice.ITTech ITTech = new ButtonOffice.ITTech();

                        ITTech.AssignDesk(Desk);
                        _Persons.Add(ITTech);
                        if(_Persons.Count == _NextCatAtNumberOfEmployees)
                        {
                            _NextCatAtNumberOfEmployees += 20;
                            _CatStock += 1;
                        }
                        FireSpendMoney(ButtonOffice.Data.ITTechHireCost, Desk.GetMidLocation());

                        return true;
                    }
                }
            }

            return false;
        }

        public System.Boolean HireJanitor(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.JanitorHireCost)
            {
                ButtonOffice.Office Office = _GetOffice(Rectangle.Location);

                if(Office != null)
                {
                    ButtonOffice.Desk Desk = _GetDesk(Office, Rectangle.Location);

                    if((Desk != null) && (Desk.IsFree() == true))
                    {
                        _Cents -= ButtonOffice.Data.JanitorHireCost;

                        ButtonOffice.Janitor Janitor = new ButtonOffice.Janitor();

                        Janitor.AssignDesk(Desk);
                        _Persons.Add(Janitor);
                        if(_Persons.Count == _NextCatAtNumberOfEmployees)
                        {
                            _NextCatAtNumberOfEmployees += 20;
                            _CatStock += 1;
                        }
                        FireSpendMoney(ButtonOffice.Data.JanitorHireCost, Desk.GetMidLocation());

                        return true;
                    }
                }
            }

            return false;
        }

        public void FirePerson(ButtonOffice.Person Person)
        {
            Person.Fire();
            _Persons.Remove(Person);
            if(Person.Type == ButtonOffice.Type.ITTech)
            {
                ButtonOffice.ITTech ITTech = Person as ITTech;
                System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = ITTech.GetRepairingTarget();

                if(BrokenThing != null)
                {
                    _BrokenThings.Enqueue(BrokenThing);
                }
            }
        }

        public System.Boolean PlaceCat(System.Drawing.RectangleF Rectangle)
        {
            if(_CatStock > 0)
            {
                ButtonOffice.Office Office = _GetOffice(Rectangle.Location);

                if((Office != null) && (Office.Cat == null))
                {
                    ButtonOffice.Cat Cat = new ButtonOffice.Cat();

                    Cat.SetRectangle(Rectangle);
                    Cat.AssignOffice(Office);
                    _CatStock -= 1;

                    return true;
                }
            }

            return false;
        }

        private ButtonOffice.Office _GetOffice(System.Drawing.PointF GameCoordinates)
        {
            foreach(ButtonOffice.Office Office in _Offices)
            {
                if(Office.GetRectangle().Contains(GameCoordinates) == true)
                {
                    return Office;
                }
            }

            return null;
        }

        private ButtonOffice.Desk _GetDesk(ButtonOffice.Office Office, System.Drawing.PointF Location)
        {
            System.Diagnostics.Debug.Assert(Office != null);

            System.Single NearestDeskDistanceSquared = System.Single.MaxValue;
            ButtonOffice.Desk NearestDesk = null;
            System.Single DeskDistanceSquared;

            DeskDistanceSquared = Office.FirstDesk.GetMidLocation().GetDistanceSquared(Location);
            if(DeskDistanceSquared < NearestDeskDistanceSquared)
            {
                NearestDesk = Office.FirstDesk;
                NearestDeskDistanceSquared = DeskDistanceSquared;
            }
            DeskDistanceSquared = Office.SecondDesk.GetMidLocation().GetDistanceSquared(Location);
            if(DeskDistanceSquared < NearestDeskDistanceSquared)
            {
                NearestDesk = Office.SecondDesk;
                NearestDeskDistanceSquared = DeskDistanceSquared;
            }
            DeskDistanceSquared = Office.ThirdDesk.GetMidLocation().GetDistanceSquared(Location);
            if(DeskDistanceSquared < NearestDeskDistanceSquared)
            {
                NearestDesk = Office.ThirdDesk;
                NearestDeskDistanceSquared = DeskDistanceSquared;
            }
            DeskDistanceSquared = Office.FourthDesk.GetMidLocation().GetDistanceSquared(Location);
            if(DeskDistanceSquared < NearestDeskDistanceSquared)
            {
                NearestDesk = Office.FourthDesk;
                NearestDeskDistanceSquared = DeskDistanceSquared;
            }

            return NearestDesk;
        }

        public void FireEarnMoney(System.UInt64 Cents, System.Drawing.PointF Location)
        {
            if(OnEarnMoney != null)
            {
                OnEarnMoney(Cents, Location);
            }
        }

        public void FireSpendMoney(System.UInt64 Cents, System.Drawing.PointF Location)
        {
            if(OnSpendMoney != null)
            {
                OnSpendMoney(Cents, Location);
            }
        }

        public System.UInt64 GetCents()
        {
            return _Cents;
        }

        private System.UInt64 _GetEuros(System.UInt64 Cents)
        {
            return Cents / 100;
        }

        private System.UInt64 _GetCents(System.UInt64 Cents)
        {
            return Cents % 100;
        }

        public System.String GetMoneyString(System.UInt64 Cents)
        {
            return _GetEuros(Cents).ToString() + "." + _GetCents(Cents).ToString("00") + "€";
        }

        public System.Pair<System.Int32, System.Int32> GetBuildingMinimumMaximum(System.Int32 Row)
        {
            return _BuildingMinimumMaximum[Row];
        }

        public void SaveToFile(System.String FileName)
        {
            ButtonOffice.GameSaver SaveGameState = new ButtonOffice.GameSaver(FileName);

            SaveGameState.Save(this);
        }

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = GameSaver.CreateElement("game");
            System.Xml.XmlElement BrokenThingsListElement = GameSaver.CreateElement("broken-things");

            foreach(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing in _BrokenThings)
            {
                BrokenThingsListElement.AppendChild(GameSaver.CreateProperty("broken-thing", BrokenThing));
            }
            Result.AppendChild(BrokenThingsListElement);
            Result.AppendChild(GameSaver.CreateProperty("cat-stock", _CatStock));
            Result.AppendChild(GameSaver.CreateProperty("cents", _Cents));
            Result.AppendChild(GameSaver.CreateProperty("minutes", _Minutes));
            Result.AppendChild(GameSaver.CreateProperty("next-cat-at-number-of-employees", _NextCatAtNumberOfEmployees));

            System.Xml.XmlElement OfficeListElement = GameSaver.CreateElement("offices");

            foreach(ButtonOffice.Office Office in _Offices)
            {
                OfficeListElement.AppendChild(GameSaver.CreateProperty("office", Office));
            }
            Result.AppendChild(OfficeListElement);

            System.Xml.XmlElement PersonListElement = GameSaver.CreateElement("persons");

            foreach(ButtonOffice.Person Person in _Persons)
            {
                PersonListElement.AppendChild(GameSaver.CreateProperty("person", Person));
            }
            Result.AppendChild(PersonListElement);
            Result.AppendChild(GameSaver.CreateProperty("sub-minute", _SubMinute));
            Result.AppendChild(GameSaver.CreateProperty("world-width", ButtonOffice.Data.WorldBlockWidth));
            Result.AppendChild(GameSaver.CreateProperty("world-height", ButtonOffice.Data.WorldBlockHeight));

            return Result;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            foreach(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing in GameLoader.LoadBrokenThingList(Element, "broken-things", "broken-thing"))
            {
                _BrokenThings.Enqueue(BrokenThing);
            }
            _CatStock = GameLoader.LoadUInt32Property(Element, "cat-stock");
            _Cents = GameLoader.LoadUInt64Property(Element, "cents");
            _Minutes = GameLoader.LoadUInt64Property(Element, "minutes");
            _NextCatAtNumberOfEmployees = GameLoader.LoadUInt32Property(Element, "next-cat-at-number-of-employees");
            foreach(ButtonOffice.Office Office in GameLoader.LoadOfficeList(Element, "offices", "office"))
            {
                _Offices.Add(Office);
            }
            foreach(ButtonOffice.Person Person in GameLoader.LoadPersonList(Element, "persons", "person"))
            {
                _Persons.Add(Person);
            }
            _SubMinute = GameLoader.LoadSingleProperty(Element, "sub-minute");

            System.Int32 WorldWidth = GameLoader.LoadInt32Property(Element, "world-width");
            System.Int32 WorldHeight = GameLoader.LoadInt32Property(Element, "world-height");

            for(System.Int32 Index = 0; Index < WorldHeight; ++Index)
            {
                _FreeSpace.Add(new System.Collections.BitArray(WorldWidth, true));
            }
            for(System.Int32 Index = 0; Index < WorldHeight; ++Index)
            {
                _BuildingMinimumMaximum.Add(new System.Pair<System.Int32, System.Int32>(System.Int32.MaxValue, System.Int32.MinValue));
            }
            foreach(ButtonOffice.Office Office in _Offices)
            {
                System.Drawing.RectangleF Rectangle = Office.GetRectangle();

                for(System.Int32 Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
                {
                    _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column] = false;
                }
                if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First > Rectangle.X.GetFlooredAsInt32())
                {
                    _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First = Rectangle.X.GetFlooredAsInt32();
                }
                if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second < Rectangle.Right.GetFlooredAsInt32())
                {
                    _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second = Rectangle.Right.GetFlooredAsInt32();
                }
            }
        }
    }
}

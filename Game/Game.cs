namespace ButtonOffice
{
    public class Game : ButtonOffice.PersistentObject
    {
        public delegate void MoneyChangeDelegate(System.UInt64 Cents, System.Drawing.PointF Location);
        public event MoneyChangeDelegate OnEarnMoney;
        public event MoneyChangeDelegate OnSpendMoney;

        private readonly System.Collections.Generic.List<ButtonOffice.Accountant> _Accountants;
        private readonly System.Collections.Generic.List<ButtonOffice.Bathroom> _Bathrooms;
        private readonly System.Collections.Generic.List<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> _BrokenThings;
        private System.UInt32 _CatStock;
        private System.UInt64 _Cents;
        private readonly System.Collections.Generic.List<System.Collections.BitArray> _FreeSpace;
        private readonly System.Collections.Generic.List<System.Pair<System.Int32, System.Int32>> _BuildingMinimumMaximum;
        private System.UInt64 _Minutes;
        private System.UInt32 _NextCatAtNumberOfEmployees;
        private readonly System.Collections.Generic.List<ButtonOffice.Office> _Offices;
        private readonly System.Collections.Generic.List<ButtonOffice.Person> _Persons;
        private System.Single _SubMinute;

        public System.Collections.Generic.List<ButtonOffice.Bathroom> Bathrooms
        {
            get
            {
                return _Bathrooms;
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
            _Accountants = new System.Collections.Generic.List<ButtonOffice.Accountant>();
            _Bathrooms = new System.Collections.Generic.List<ButtonOffice.Bathroom>();
            _BrokenThings = new System.Collections.Generic.List<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>>();
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

        public System.UInt64 GetCurrentBonusPromille()
        {
            System.UInt64 Result = 1000;

            foreach(ButtonOffice.Accountant Accountant in _Accountants)
            {
                if((Accountant.GetAtDesk() == true) && (Accountant.GetDesk().GetComputer().IsBroken() == false))
                {
                    Result += Accountant.GetBonusPromille();
                }
            }

            return Result;
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
            if(_CanBuild(ButtonOffice.Data.OfficeBuildCost, Rectangle) == true)
            {
                _Build(ButtonOffice.Data.OfficeBuildCost, Rectangle);

                ButtonOffice.Office Office = new ButtonOffice.Office();

                Office.SetRectangle(Rectangle);
                _Offices.Add(Office);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean BuildBathroom(System.Drawing.RectangleF Rectangle)
        {
            if(_CanBuild(ButtonOffice.Data.BathroomBuildCost, Rectangle) == true)
            {
                _Build(ButtonOffice.Data.BathroomBuildCost, Rectangle);

                ButtonOffice.Bathroom Bathroom = new ButtonOffice.Bathroom();

                Bathroom.SetRectangle(Rectangle);
                _Bathrooms.Add(Bathroom);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean HireAccountant(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.AccountantHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    _Cents -= ButtonOffice.Data.AccountantHireCost;

                    ButtonOffice.Accountant Accountant = new ButtonOffice.Accountant();

                    Accountant.AssignDesk(Desk);
                    _Persons.Add(Accountant);
                    _Accountants.Add(Accountant);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }
                    FireSpendMoney(ButtonOffice.Data.AccountantHireCost, Desk.GetMidLocation());

                    return true;
                }
            }

            return false;
        }

        public System.Boolean HireWorker(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.WorkerHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

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

            return false;
        }

        public System.Boolean HireITTech(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.ITTechHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

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

            return false;
        }

        public System.Boolean HireJanitor(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= ButtonOffice.Data.JanitorHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

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

            return false;
        }

        public void FirePerson(ButtonOffice.Person Person)
        {
            Person.Fire();
            _Persons.Remove(Person);
            if(Person is ButtonOffice.ITTech)
            {
                ButtonOffice.ITTech ITTech = Person as ITTech;
                System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = ITTech.GetRepairingTarget();

                if(BrokenThing != null)
                {
                    _BrokenThings.Add(BrokenThing);
                }
            }
            else if(Person is ButtonOffice.Accountant)
            {
                _Accountants.Remove(Person as ButtonOffice.Accountant);
            }
        }

        public System.Boolean PlaceCat(System.Drawing.RectangleF Rectangle)
        {
            if(_CatStock > 0)
            {
                ButtonOffice.Office Office = GetOffice(Rectangle.Location);

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

        public ButtonOffice.Office GetOffice(System.Drawing.PointF Location)
        {
            foreach(ButtonOffice.Office Office in _Offices)
            {
                if(Office.GetRectangle().Contains(Location) == true)
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

        public void MovePerson(ButtonOffice.Person Person, ButtonOffice.Desk Desk)
        {
            System.Diagnostics.Debug.Assert(Person != null);
            System.Diagnostics.Debug.Assert(Desk != null);
            System.Diagnostics.Debug.Assert(Desk.IsFree() == true);

            Person.SetAtDesk(false);
            Person.AssignDesk(Desk);
        }

        public ButtonOffice.Desk GetDesk(System.Drawing.PointF Location)
        {
            ButtonOffice.Office Office = GetOffice(Location);

            if(Office == null)
            {
                return null;
            }

            ButtonOffice.Desk Desk = _GetDesk(Office, Location);

            return Desk;
        }

        public void EnqueueBrokenThing(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing)
        {
            _BrokenThings.Add(BrokenThing);
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> DequeueBrokenThing()
        {
            if(_BrokenThings.Count > 0)
            {
                return _BrokenThings.PopFirst();
            }
            else
            {
                return null;
            }
        }

        private void _Build(System.UInt64 Cost, System.Drawing.RectangleF Rectangle)
        {
            _Cents -= Cost;
            FireSpendMoney(Cost, Rectangle.GetMidPoint());
            _OccupyFreeSpace(Rectangle);
            _WidenBuilding(Rectangle);
        }

        private System.Boolean _CanBuild(System.UInt64 Cost, System.Drawing.RectangleF Rectangle)
        {
            return (_EnoughCents(Cost) == true) && (_InBuildableWorld(Rectangle) == true) && (_InFreeSpace(Rectangle) == true) && (_CompletelyOnTopOfBuilding(Rectangle) == true);
        }

        private System.Boolean _EnoughCents(System.UInt64 Cents)
        {
            return _Cents >= Cents;
        }

        private System.Boolean _InBuildableWorld(System.Drawing.RectangleF Rectangle)
        {
            return (Rectangle.Y >= 0.0f) && (Rectangle.X >= 0.0f) && (Rectangle.X + Rectangle.Width < ButtonOffice.Data.WorldBlockWidth.ToSingle());
        }

        private System.Boolean _InFreeSpace(System.Drawing.RectangleF Rectangle)
        {
            System.Boolean Result = true;

            for(System.Int32 Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
            {
                Result &= _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column];
            }

            return Result;
        }

        private System.Boolean _CompletelyOnTopOfBuilding(System.Drawing.RectangleF Rectangle)
        {
            System.Boolean Result = true;

            if(Rectangle.Y > 0.0f)
            {
                Result &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].First <= Rectangle.X.GetFlooredAsInt32();
                Result &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].Second >= Rectangle.Right.GetFlooredAsInt32();
            }

            return Result;
        }

        private void _OccupyFreeSpace(System.Drawing.RectangleF Rectangle)
        {
            for(System.Int32 Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
            {
                _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column] = false;
            }
        }

        private void _WidenBuilding(System.Drawing.RectangleF Rectangle)
        {
            if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First > Rectangle.X.GetFlooredAsInt32())
            {
                _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First = Rectangle.X.GetFlooredAsInt32();
            }
            if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second < Rectangle.Right.GetFlooredAsInt32())
            {
                _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second = Rectangle.Right.GetFlooredAsInt32();
            }
        }

        public virtual System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver, System.Xml.XmlElement Element)
        {
			System.Diagnostics.Debug.Assert(Element == null);
            Element = GameSaver.CreateElement("game");
			
            System.Xml.XmlElement AccountantListElement = GameSaver.CreateElement("accountants");

            foreach(ButtonOffice.Accountant Accountant in _Accountants)
            {
                AccountantListElement.AppendChild(GameSaver.CreateProperty("accountant", Accountant));
            }
            Element.AppendChild(AccountantListElement);

            System.Xml.XmlElement BathroomsListElement = GameSaver.CreateElement("bathrooms");

            foreach(ButtonOffice.Bathroom Bathroom in _Bathrooms)
            {
                BathroomsListElement.AppendChild(GameSaver.CreateProperty("bathroom", Bathroom));
            }
            Element.AppendChild(BathroomsListElement);

            System.Xml.XmlElement BrokenThingsListElement = GameSaver.CreateElement("broken-things");

            foreach(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing in _BrokenThings)
            {
                BrokenThingsListElement.AppendChild(GameSaver.CreateProperty("broken-thing", BrokenThing));
            }
            Element.AppendChild(BrokenThingsListElement);
            Element.AppendChild(GameSaver.CreateProperty("cat-stock", _CatStock));
            Element.AppendChild(GameSaver.CreateProperty("cents", _Cents));
            Element.AppendChild(GameSaver.CreateProperty("minutes", _Minutes));
            Element.AppendChild(GameSaver.CreateProperty("next-cat-at-number-of-employees", _NextCatAtNumberOfEmployees));

            System.Xml.XmlElement OfficeListElement = GameSaver.CreateElement("offices");

            foreach(ButtonOffice.Office Office in _Offices)
            {
                OfficeListElement.AppendChild(GameSaver.CreateProperty("office", Office));
            }
            Element.AppendChild(OfficeListElement);

            System.Xml.XmlElement PersonListElement = GameSaver.CreateElement("persons");

            foreach(ButtonOffice.Person Person in _Persons)
            {
                PersonListElement.AppendChild(GameSaver.CreateProperty("person", Person));
            }
            Element.AppendChild(PersonListElement);
            Element.AppendChild(GameSaver.CreateProperty("sub-minute", _SubMinute));
            Element.AppendChild(GameSaver.CreateProperty("world-width", ButtonOffice.Data.WorldBlockWidth));
            Element.AppendChild(GameSaver.CreateProperty("world-height", ButtonOffice.Data.WorldBlockHeight));

            return Element;
        }

        public virtual void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            foreach(ButtonOffice.Accountant Accountant in GameLoader.LoadAccountantList(Element, "accountants", "accountant"))
            {
                _Accountants.Add(Accountant);
            }
            foreach(ButtonOffice.Bathroom Bathroom in GameLoader.LoadBathroomList(Element, "bathrooms", "bathroom"))
            {
                _Bathrooms.Add(Bathroom);
            }
            foreach(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing in GameLoader.LoadBrokenThingList(Element, "broken-things", "broken-thing"))
            {
                _BrokenThings.Add(BrokenThing);
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

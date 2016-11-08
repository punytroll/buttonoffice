using System;

namespace ButtonOffice
{
    public class Game : PersistentObject
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
        private readonly System.Collections.Generic.List<Person> _Persons;
        private Single _SubMinute;

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

        public System.Collections.Generic.List<Person> Persons
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
            Result._Cents = Data.StartCents;
            for(System.Int32 Index = 0; Index < Data.WorldBlockHeight; ++Index)
            {
                Result._FreeSpace.Add(new System.Collections.BitArray(Data.WorldBlockWidth, true));
            }
            for(System.Int32 Index = 0; Index < Data.WorldBlockHeight; ++Index)
            {
                Result._BuildingMinimumMaximum.Add(new System.Pair<System.Int32, System.Int32>(System.Int32.MaxValue, System.Int32.MinValue));
            }
            Result._Minutes = Data.StartMinutes;
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
            _Persons = new System.Collections.Generic.List<Person>();
        }

        public void Move(Single GameMinutes)
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
            foreach(Person Person in _Persons)
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

        public Boolean BuildOffice(System.Drawing.RectangleF Rectangle)
        {
            if(_CanBuild(Data.OfficeBuildCost, Rectangle) == true)
            {
                _Build(Data.OfficeBuildCost, Rectangle);

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

        public Boolean BuildBathroom(System.Drawing.RectangleF Rectangle)
        {
            if(_CanBuild(Data.BathroomBuildCost, Rectangle) == true)
            {
                _Build(Data.BathroomBuildCost, Rectangle);

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

        public Boolean HireAccountant(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= Data.AccountantHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    _Cents -= Data.AccountantHireCost;

                    ButtonOffice.Accountant Accountant = new ButtonOffice.Accountant();

                    Accountant.AssignDesk(Desk);
                    _Persons.Add(Accountant);
                    _Accountants.Add(Accountant);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }
                    FireSpendMoney(Data.AccountantHireCost, Desk.GetMidLocation());

                    return true;
                }
            }

            return false;
        }

        public Boolean HireWorker(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= Data.WorkerHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    _Cents -= Data.WorkerHireCost;

                    ButtonOffice.Worker Worker = new ButtonOffice.Worker();

                    Worker.AssignDesk(Desk);
                    _Persons.Add(Worker);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }
                    FireSpendMoney(Data.WorkerHireCost, Desk.GetMidLocation());

                    return true;
                }
            }

            return false;
        }

        public Boolean HireITTech(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= Data.ITTechHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    _Cents -= Data.ITTechHireCost;

                    ButtonOffice.ITTech ITTech = new ButtonOffice.ITTech();

                    ITTech.AssignDesk(Desk);
                    _Persons.Add(ITTech);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }
                    FireSpendMoney(Data.ITTechHireCost, Desk.GetMidLocation());

                    return true;
                }
            }

            return false;
        }

        public Boolean HireJanitor(System.Drawing.RectangleF Rectangle)
        {
            if(_Cents >= Data.JanitorHireCost)
            {
                ButtonOffice.Desk Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    _Cents -= Data.JanitorHireCost;

                    ButtonOffice.Janitor Janitor = new ButtonOffice.Janitor();

                    Janitor.AssignDesk(Desk);
                    _Persons.Add(Janitor);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }
                    FireSpendMoney(Data.JanitorHireCost, Desk.GetMidLocation());

                    return true;
                }
            }

            return false;
        }

        public void FirePerson(Person Person)
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

        public Boolean PlaceCat(System.Drawing.RectangleF Rectangle)
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

            Single NearestDeskDistanceSquared = System.Single.MaxValue;
            ButtonOffice.Desk NearestDesk = null;
            Single DeskDistanceSquared;

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
            return _GetEuros(Cents) + "." + _GetCents(Cents).ToString("00") + "€";
        }

        public System.Pair<System.Int32, System.Int32> GetBuildingMinimumMaximum(System.Int32 Row)
        {
            return _BuildingMinimumMaximum[Row];
        }

        public void SaveToFile(String FileName)
        {
            var GameSaver = new GameSaver();

            GameSaver.Save(this);
            GameSaver.WriteToFile(FileName);
        }

        public void MovePerson(Person Person, ButtonOffice.Desk Desk)
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

        private Boolean _CanBuild(System.UInt64 Cost, System.Drawing.RectangleF Rectangle)
        {
            return (_EnoughCents(Cost) == true) && (_InBuildableWorld(Rectangle) == true) && (_InFreeSpace(Rectangle) == true) && (_CompletelyOnTopOfBuilding(Rectangle) == true);
        }

        private Boolean _EnoughCents(System.UInt64 Cents)
        {
            return _Cents >= Cents;
        }

        private Boolean _InBuildableWorld(System.Drawing.RectangleF Rectangle)
        {
            return (Rectangle.Y >= 0.0f) && (Rectangle.X >= 0.0f) && (Rectangle.X + Rectangle.Width < Data.WorldBlockWidth.ToSingle());
        }

        private Boolean _InFreeSpace(System.Drawing.RectangleF Rectangle)
        {
            Boolean Result = true;

            for(System.Int32 Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
            {
                Result &= _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column];
            }

            return Result;
        }

        private Boolean _CompletelyOnTopOfBuilding(System.Drawing.RectangleF Rectangle)
        {
            Boolean Result = true;

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

        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("accountants", _Accountants);
            ObjectStore.Save("bathrooms", _Bathrooms);
            ObjectStore.Save("broken-things", _BrokenThings);
            ObjectStore.Save("cat-stock", _CatStock);
            ObjectStore.Save("cents", _Cents);
            ObjectStore.Save("minutes", _Minutes);
            ObjectStore.Save("next-cat-at-number-of-employees", _NextCatAtNumberOfEmployees);
            ObjectStore.Save("offices", _Offices);
            ObjectStore.Save("persons", _Persons);
            ObjectStore.Save("sub-minute", _SubMinute);
            ObjectStore.Save("world-width", Data.WorldBlockWidth);
            ObjectStore.Save("world-height", Data.WorldBlockHeight);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            foreach(var Accountant in ObjectStore.LoadAccountants("accountants"))
            {
                _Accountants.Add(Accountant);
            }
            foreach(var Bathroom in ObjectStore.LoadBathrooms("bathrooms"))
            {
                _Bathrooms.Add(Bathroom);
            }
            foreach(var BrokenThing in ObjectStore.LoadBrokenThings("broken-things"))
            {
                _BrokenThings.Add(BrokenThing);
            }
            _CatStock = ObjectStore.LoadUInt32Property("cat-stock");
            _Cents = ObjectStore.LoadUInt64Property("cents");
            _Minutes = ObjectStore.LoadUInt64Property("minutes");
            _NextCatAtNumberOfEmployees = ObjectStore.LoadUInt32Property("next-cat-at-number-of-employees");
            foreach(ButtonOffice.Office Office in ObjectStore.LoadOffices("offices"))
            {
                _Offices.Add(Office);
            }
            foreach(Person Person in ObjectStore.LoadPersons("persons"))
            {
                _Persons.Add(Person);
            }
            _SubMinute = ObjectStore.LoadSingleProperty("sub-minute");

            System.Int32 WorldWidth = ObjectStore.LoadInt32Property("world-width");
            System.Int32 WorldHeight = ObjectStore.LoadInt32Property("world-height");

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

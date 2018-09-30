using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Game : PersistentObject
    {
        public delegate void MoneyChangeDelegate(UInt64 Cents, Vector2 Location);
        public event MoneyChangeDelegate OnEarnMoney;
        public event MoneyChangeDelegate OnSpendMoney;

        private readonly List<Accountant> _Accountants;
        private readonly List<PersistentObject> _BrokenThings;
        private readonly List<Pair<Int32, Int32>> _BuildingMinimumMaximum;
        private readonly List<Building> _Buildings;
        private UInt32 _CatStock;
        private UInt64 _Cents;
        private readonly List<BitArray> _FreeSpace;
        private Int32 _HighestFloor;
        private Int32 _LeftBorder;
        private Int32 _LowestFloor;
        private Double _Minutes;
        private UInt32 _NextCatAtNumberOfEmployees;
        private readonly List<Office> _Offices;
        private readonly List<Person> _Persons;
        private Int32 _RightBorder;
        private readonly Transportation.Transportation _Transportation;

        public List<Building> Buildings => _Buildings;

        public Int32 HighestFloor => _HighestFloor;

        public Int32 LeftBorder => _LeftBorder;

        public Int32 LowestFloor => _LowestFloor;

        public List<Office> Offices => _Offices;

        public List<Person> Persons => _Persons;

        public Int32 RightBorder => _RightBorder;

        internal Transportation.Transportation Transportation => _Transportation;

        public static Game CreateNew()
        {
            var Result = new Game();

            Result._CatStock = 0;
            Result._Cents = Data.NewGameCents;
            Result._HighestFloor = Data.NewGameHighestFloor;
            Result._LeftBorder = Data.NewGameLeftBorder;
            Result._LowestFloor = Data.NewGameLowestFloor;
            Result._Minutes = Data.NewGameMinutes;
            Result._NextCatAtNumberOfEmployees = 20;
            Result._RightBorder = Data.NewGameRightBorder;
            Result._InitializeFreeSpaceAndMinimumMaximum();

            return Result;
        }

        public static Game LoadFromFileName(String FileName)
        {
            var GameLoader = new GameLoader(FileName);
            var Result = new Game();

            GameLoader.Load(Result);

            return Result;
        }

        private Game()
        {
            _Accountants = new List<Accountant>();
            _BrokenThings = new List<PersistentObject>();
            _Buildings = new List<Building>();
            _FreeSpace = new List<BitArray>();
            _BuildingMinimumMaximum = new List<Pair<Int32, Int32>>();
            _Offices = new List<Office>();
            _Persons = new List<Person>();
            _Transportation = new Transportation.Transportation();
        }

        public void Move(Double DeltaGameMinutes)
        {
            _Minutes += DeltaGameMinutes;
            foreach(var Building in _Buildings)
            {
                Building.Move(this, DeltaGameMinutes);
            }
            foreach(var Person in _Persons)
            {
                Person.Move(this, DeltaGameMinutes);
            }
        }

        public UInt64 GetCurrentBonusPromille()
        {
            var Result = 1000UL;

            foreach(var Accountant in _Accountants)
            {
                if((Accountant.GetAtDesk() == true) && (Accountant.Desk.Computer.IsBroken() == false))
                {
                    Result += Accountant.GetBonusPromille();
                }
            }

            return Result;
        }

        public UInt32 GetCatStock()
        {
            return _CatStock;
        }

        public void EarnMoney(UInt64 Cents, Vector2 Location)
        {
            _Cents += Cents;
            OnEarnMoney?.Invoke(Cents, Location);
        }

        public void SpendMoney(UInt64 Cents, Vector2 Location)
        {
            _Cents -= Cents;
            OnSpendMoney?.Invoke(Cents, Location);
        }

        public UInt64 GetDay()
        {
            return Convert.ToUInt64(_Minutes / 1440.0);
        }

        public UInt64 GetTotalMinutes()
        {
            return Convert.ToUInt64(_Minutes);
        }

        public UInt64 GetMinuteOfDay()
        {
            return Convert.ToUInt64(_Minutes) % 1440;
        }

        public UInt64 GetFirstMinuteOfDay(UInt64 Day)
        {
            return Day * 1440;
        }

        public UInt64 GetFirstMinuteOfToday()
        {
            return GetDay() * 1440;
        }

        public Boolean BuildBathroom(RectangleF Rectangle)
        {
            if(CanBuild(Data.BathroomBuildCost, Rectangle.X, Rectangle.Width, Rectangle.Y, Rectangle.Height) == true)
            {
                var Bathroom = new Bathroom();

                Bathroom.Floor = Rectangle.Y;
                Bathroom.Height = Rectangle.Height;
                Bathroom.Left = Rectangle.X;
                Bathroom.Width = Rectangle.Width;
                _Build(Data.BathroomBuildCost, Bathroom);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuildOffice(RectangleF Rectangle)
        {
            if(CanBuild(Data.OfficeBuildCost, Rectangle.X, Rectangle.Width, Rectangle.Y, Rectangle.Height) == true)
            {
                var Office = new Office();

                Office.Floor = Rectangle.Y;
                Office.Height = Rectangle.Height;
                Office.Left = Rectangle.X;
                Office.Width = Rectangle.Width;
                _Build(Data.OfficeBuildCost, Office);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuildStairs(RectangleF Rectangle)
        {
            if(CanBuild(Data.StairsBuildCost, Rectangle.X, Rectangle.Width, Rectangle.Y, Rectangle.Height) == true)
            {
                var Stairs = new Stairs();

                Stairs.Floor = Rectangle.Y;
                Stairs.Height = Rectangle.Height;
                Stairs.Left = Rectangle.X;
                Stairs.Width = Rectangle.Width;
                _Build(Data.StairsBuildCost, Stairs);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean HireAccountant(RectangleF Rectangle)
        {
            if(_Cents >= Data.AccountantHireCost)
            {
                var Desk = GetDesk(Rectangle.GetMidPoint());

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.AccountantHireCost, Desk.GetMidLocation());

                    var Accountant = new Accountant();

                    Accountant.AssignDesk(Desk);
                    _AddPerson(Accountant);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }

                    return true;
                }
            }

            return false;
        }

        public Boolean HireWorker(RectangleF Rectangle)
        {
            if(_Cents >= Data.WorkerHireCost)
            {
                var Desk = GetDesk(Rectangle.GetMidPoint());

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.WorkerHireCost, Desk.GetMidLocation());

                    var Worker = new Worker();

                    Worker.AssignDesk(Desk);
                    _AddPerson(Worker);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }

                    return true;
                }
            }

            return false;
        }

        public Boolean HireITTech(RectangleF Rectangle)
        {
            if(_Cents >= Data.ITTechHireCost)
            {
                var Desk = GetDesk(Rectangle.GetMidPoint());

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.ITTechHireCost, Desk.GetMidLocation());

                    var ITTech = new ITTech();

                    ITTech.AssignDesk(Desk);
                    _AddPerson(ITTech);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }

                    return true;
                }
            }

            return false;
        }

        public Boolean HireJanitor(RectangleF Rectangle)
        {
            if(_Cents >= Data.JanitorHireCost)
            {
                var Desk = GetDesk(Rectangle.GetMidPoint());

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.JanitorHireCost, Desk.GetMidLocation());

                    var Janitor = new Janitor();

                    Janitor.AssignDesk(Desk);
                    _AddPerson(Janitor);
                    if(_Persons.Count == _NextCatAtNumberOfEmployees)
                    {
                        _NextCatAtNumberOfEmployees += 20;
                        _CatStock += 1;
                    }

                    return true;
                }
            }

            return false;
        }

        public void FirePerson(Person Person)
        {
            Person.Fire();
            _Persons.Remove(Person);
            if(Person is ITTech)
            {
                var ITTech = (ITTech)Person;
                var BrokenThing = ITTech.GetRepairingTarget();

                if(BrokenThing != null)
                {
                    _BrokenThings.Add(BrokenThing);
                }
            }
            else if(Person is Accountant)
            {
                _Accountants.Remove((Accountant)Person);
            }
        }

        public Boolean PlaceCat(RectangleF Rectangle)
        {
            if(_CatStock > 0)
            {
                var Office = GetOffice(Rectangle.GetMidPoint());

                if((Office != null) && (Office.Cat == null))
                {
                    var Cat = new Cat();

                    Cat.SetY(Office.Floor);
                    Cat.SetX(Rectangle.X + Rectangle.Width / 2.0f);
                    Cat.AssignOffice(Office);
                    _CatStock -= 1;

                    return true;
                }
            }

            return false;
        }

        public Office GetOffice(Vector2 Location)
        {
            foreach(var Office in _Offices)
            {
                if(Office.Contains(Location) == true)
                {
                    return Office;
                }
            }

            return null;
        }

        private Desk _GetDesk(Office Office, Vector2 Location)
        {
            Debug.Assert(Office != null);

            var NearestDeskDistanceSquared = Double.MaxValue;
            Desk NearestDesk = null;
            var DeskDistanceSquared = Office.FirstDesk.GetMidLocation().GetDistanceSquared(Location);

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
            }

            return NearestDesk;
        }

        public UInt64 GetCents()
        {
            return _Cents;
        }

        public String GetMoneyString(UInt64 Cents)
        {
            return (Cents / 100) + "." + (Cents % 100).ToString("00") + "€";
        }

        public Pair<Int32, Int32> GetBuildingMinimumMaximum(Int32 Row)
        {
            return _BuildingMinimumMaximum[Row];
        }

        public void SaveToFile(String FileName)
        {
            var GameSaver = new GameSaver();

            GameSaver.Save(this);
            GameSaver.WriteToFile(FileName);
        }

        public void MovePerson(Person Person, Desk Desk)
        {
            Debug.Assert(Person != null);
            Debug.Assert(Desk != null);
            Debug.Assert(Desk.IsFree() == true);

            Person.SetAtDesk(false);
            Person.AssignDesk(Desk);
        }

        public Desk GetDesk(Vector2 Location)
        {
            var Office = GetOffice(Location);

            if(Office == null)
            {
                return null;
            }

            var Desk = _GetDesk(Office, Location);

            return Desk;
        }

        public void EnqueueBrokenThing(PersistentObject BrokenThing)
        {
            _BrokenThings.Add(BrokenThing);
        }

        public PersistentObject DequeueBrokenThing()
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

        private void _Build(UInt64 Cost, Building Building)
        {
            SpendMoney(Cost, Building.GetMidLocation());
            _AddBuilding(Building);
        }

        public void UpdateBuilding(UInt64 Cost, Building Building)
        {
            SpendMoney(Cost, Building.GetMidLocation());
            _OccupyFreeSpace(Building.Left, Building.Width, Building.Floor, Building.Height);
            _WidenBuilding(Building.Left, Building.Width, Building.Floor, Building.Height);
        }

        public Boolean CanBuild(UInt64 Cost, Double Left, Double Width, Double Floor, Double Height)
        {
            return (_EnoughCents(Cost) == true) && (_InBuildableWorld(Left, Width, Floor, Height) == true) && (_InFreeSpace(Left, Width, Floor, Height) == true) && (_CompletelyOnTopOfBuilding(Left, Width, Floor, Height) == true);
        }

        private Boolean _EnoughCents(UInt64 Cents)
        {
            return _Cents >= Cents;
        }

        private Boolean _InBuildableWorld(Double Left, Double Width, Double Floor, Double Height)
        {
            return (Left >= _LeftBorder) && (Left + Width < _RightBorder) && (Floor >= _LowestFloor) && (Floor + Height < _HighestFloor);
        }

        private Boolean _InFreeSpace(Double Left, Double Width, Double Floor, Double Height)
        {
            var Result = true;

            for(var Column = 0; Column < Width.GetFlooredAsInt32(); ++Column)
            {
                Result &= _FreeSpace[Floor.GetFlooredAsInt32() - _LowestFloor][Left.GetFlooredAsInt32() + Column - _LeftBorder];
            }

            return Result;
        }

        private Boolean _CompletelyOnTopOfBuilding(Double Left, Double Width, Double Floor, Double Height)
        {
            var Result = true;

            if(Floor > 0.0)
            {
                Result &= _BuildingMinimumMaximum[Floor.GetFlooredAsInt32() - 1].First <= Left.GetFlooredAsInt32();
                Result &= _BuildingMinimumMaximum[Floor.GetFlooredAsInt32() - 1].Second >= (Left + Width).GetFlooredAsInt32();
            }

            return Result;
        }

        private void _InitializeFreeSpaceAndMinimumMaximum()
        {
            _FreeSpace.Clear();
            for(var Index = _LowestFloor; Index < _HighestFloor; ++Index)
            {
                _FreeSpace.Add(new BitArray(_RightBorder - _LeftBorder, true));
            }
            _BuildingMinimumMaximum.Clear();
            for(var Index = _LowestFloor; Index < _HighestFloor; ++Index)
            {
                _BuildingMinimumMaximum.Add(new Pair<Int32, Int32>(Int32.MaxValue, Int32.MinValue));
            }
        }

        private void _OccupyFreeSpace(Double Left, Double Width, Double Floor, Double Height)
        {
            for(var Row = 0; Row < Height.GetNearestInt32(); ++Row)
            {
                for(var Column = 0; Column < Width.GetNearestInt32(); ++Column)
                {
                    _FreeSpace[Floor.GetNearestInt32() + Row - _LowestFloor][Left.GetNearestInt32() + Column - _LeftBorder] = false;
                }
            }
        }

        private void _WidenBuilding(Double Left, Double Width, Double Floor, Double Height)
        {
            for(var Row = 0; Row < Height.GetNearestInt32(); ++Row)
            {
                if(_BuildingMinimumMaximum[Floor.GetNearestInt32() + Row].First > Left.GetNearestInt32())
                {
                    _BuildingMinimumMaximum[Floor.GetNearestInt32() + Row].First = Left.GetNearestInt32();
                }
                if(_BuildingMinimumMaximum[Floor.GetNearestInt32() + Row].Second < (Left + Width).GetNearestInt32())
                {
                    _BuildingMinimumMaximum[Floor.GetNearestInt32() + Row].Second = (Left + Width).GetNearestInt32();
                }
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("broken-things", _BrokenThings);
            ObjectStore.Save("buildings", _Buildings);
            ObjectStore.Save("cat-stock", _CatStock);
            ObjectStore.Save("cents", _Cents);
            ObjectStore.Save("highest-floor", _HighestFloor);
            ObjectStore.Save("left-border", _LeftBorder);
            ObjectStore.Save("lowest-floor", _LowestFloor);
            ObjectStore.Save("minutes", _Minutes);
            ObjectStore.Save("next-cat-at-number-of-employees", _NextCatAtNumberOfEmployees);
            ObjectStore.Save("persons", _Persons);
            ObjectStore.Save("right-border", _RightBorder);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            // read these at first, so we can initialize the free space and minimum/maximum per floor
            _HighestFloor = ObjectStore.LoadInt32Property("highest-floor");
            _LeftBorder = ObjectStore.LoadInt32Property("left-border");
            _LowestFloor = ObjectStore.LoadInt32Property("lowest-floor");
            _RightBorder = ObjectStore.LoadInt32Property("right-border");
            _InitializeFreeSpaceAndMinimumMaximum();
            foreach(var BrokenThing in ObjectStore.LoadObjects("broken-things"))
            {
                _BrokenThings.Add(BrokenThing);
            }
            foreach(var Building in ObjectStore.LoadBuildings("buildings"))
            {
                _AddBuilding(Building);
            }
            _CatStock = ObjectStore.LoadUInt32Property("cat-stock");
            _Cents = ObjectStore.LoadUInt64Property("cents");
            _Minutes = ObjectStore.LoadDoubleProperty("minutes");
            _NextCatAtNumberOfEmployees = ObjectStore.LoadUInt32Property("next-cat-at-number-of-employees");
            foreach(var Person in ObjectStore.LoadPersons("persons"))
            {
                _AddPerson(Person);
            }
        }

        private void _AddPerson(Person Person)
        {
            _Persons.Add(Person);
            if(Person is Accountant)
            {
                _Accountants.Add((Accountant)Person);
            }
        }

        private void _AddBuilding(Building Building)
        {
            _Buildings.Add(Building);
            if(Building is Office)
            {
                _Offices.Add((Office)Building);
            }
            else if(Building is Stairs)
            {
                var Stairs = (Stairs)Building;

                Stairs.UpdateTransportation(this);
            }
            _OccupyFreeSpace(Building.Left, Building.Width, Building.Floor, Building.Height);
            _WidenBuilding(Building.Left, Building.Width, Building.Floor, Building.Height);
        }
    }
}

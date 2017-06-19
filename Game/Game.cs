using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Game : PersistentObject
    {
        public delegate void MoneyChangeDelegate(UInt64 Cents, PointF Location);
        public event MoneyChangeDelegate OnEarnMoney;
        public event MoneyChangeDelegate OnSpendMoney;

        private readonly List<Accountant> _Accountants;
        private readonly List<Bathroom> _Bathrooms;
        private readonly List<Pair<Office, BrokenThing>> _BrokenThings;
        private readonly List<Building> _Buildings;
        private UInt32 _CatStock;
        private UInt64 _Cents;
        private readonly List<BitArray> _FreeSpace;
        private readonly List<Pair<Int32, Int32>> _BuildingMinimumMaximum;
        private UInt64 _Minutes;
        private UInt32 _NextCatAtNumberOfEmployees;
        private readonly List<Office> _Offices;
        private readonly List<Person> _Persons;
        private readonly List<Stairs> _Stairs;
        private Single _SubMinute;
        private Int32 _WorldBlockHeight;
        private Int32 _WorldBlockWidth;

        public List<Bathroom> Bathrooms => _Bathrooms;

        public List<Building> Buildings => _Buildings;

        public List<Office> Offices => _Offices;

        public List<Person> Persons => _Persons;

        public List<Stairs> Stairs => _Stairs;

        public Int32 WorldBlockHeight => _WorldBlockHeight;

        public Int32 WorldBlockWidth => _WorldBlockWidth;

        public static Game CreateNew()
        {
            var Result = new Game();

            Result._CatStock = 0;
            Result._Cents = Data.NewGameCents;
            Result._Minutes = Data.NewGameMinutes;
            Result._NextCatAtNumberOfEmployees = 20;
            Result._SubMinute = 0.0f;
            Result._WorldBlockHeight = Data.NewGameWorldBlockHeight;
            Result._WorldBlockWidth = Data.NewGameWorldBlockWidth;
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
            _Bathrooms = new List<Bathroom>();
            _BrokenThings = new List<Pair<Office, BrokenThing>>();
            _Buildings = new List<Building>();
            _FreeSpace = new List<BitArray>();
            _BuildingMinimumMaximum = new List<Pair<Int32, Int32>>();
            _Offices = new List<Office>();
            _Persons = new List<Person>();
            _Stairs = new List<Stairs>();
        }

        public void Move(Single GameMinutes)
        {
            _SubMinute += GameMinutes;
            while(_SubMinute >= 1.0f)
            {
                _Minutes += 1;
                _SubMinute -= 1.0f;
            }
            foreach(var Building in _Buildings)
            {
                Building.Move(this, GameMinutes);
            }
            foreach(var Person in _Persons)
            {
                Person.Move(this, GameMinutes);
            }
        }

        public UInt64 GetCurrentBonusPromille()
        {
            var Result = 1000UL;

            foreach(var Accountant in _Accountants)
            {
                if((Accountant.GetAtDesk() == true) && (Accountant.GetDesk().GetComputer().IsBroken() == false))
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

        public void EarnMoney(UInt64 Cents, PointF Location)
        {
            _Cents += Cents;
            OnEarnMoney?.Invoke(Cents, Location);
        }

        public void SpendMoney(UInt64 Cents, PointF Location)
        {
            _Cents -= Cents;
            OnSpendMoney?.Invoke(Cents, Location);
        }

        public UInt64 GetDay()
        {
            return _Minutes / 1440;
        }

        public UInt64 GetTotalMinutes()
        {
            return _Minutes;
        }

        public UInt64 GetMinuteOfDay()
        {
            return _Minutes % 1440;
        }

        public UInt64 GetFirstMinuteOfDay(UInt64 Day)
        {
            return Day * 1440;
        }

        public UInt64 GetFirstMinuteOfToday()
        {
            return GetDay() * 1440;
        }

        internal List<TransportationNode> GetTransportationPath(PointF FromLocation, PointF ToLocation)
        {
            var Result = new List<TransportationNode>();
            var CurrentFloor = FromLocation.Y.GetTruncatedAsInt32();
            var TargetFloor = ToLocation.Y.GetTruncatedAsInt32();
            var Upwards = TargetFloor - CurrentFloor > 0;

            while(CurrentFloor != TargetFloor)
            {
                var FoundStairs = false;

                foreach(var Stairs in _Stairs)
                {
                    var StairsFloors = Stairs.GetFloors();

                    if(StairsFloors.Contains(CurrentFloor) == true)
                    {
                        var StairsTargetFloor = CurrentFloor;

                        if(Upwards == true)
                        {
                            while((StairsTargetFloor < TargetFloor) && (StairsFloors.Contains(StairsTargetFloor + 1) == true))
                            {
                                StairsTargetFloor += 1;
                                FoundStairs = true;
                            }
                        }
                        else
                        {
                            while((StairsTargetFloor > TargetFloor) && (StairsFloors.Contains(StairsTargetFloor - 1) == true))
                            {
                                StairsTargetFloor -= 1;
                                FoundStairs = true;
                            }
                        }
                        if(FoundStairs == true)
                        {
                            var StairsTransportationNode = new TransportationNode();

                            StairsTransportationNode.SetCreateUseGoalFunction(Stairs.CreateUseGoal);
                            StairsTransportationNode.SetTargetFloor(StairsTargetFloor);
                            StairsTransportationNode.SetX(Stairs.GetX() + (Stairs.GetWidth() / 2.0f));
                            StairsTransportationNode.SetY(CurrentFloor);
                            Result.Add(StairsTransportationNode);
                            CurrentFloor = StairsTargetFloor;

                            break;
                        }
                    }
                }
            }
            if(CurrentFloor == TargetFloor)
            {
                var TransportationNode = new TransportationNode();

                TransportationNode.SetX(ToLocation.X);
                TransportationNode.SetY(ToLocation.Y);
                Result.Add(TransportationNode);

                return Result;
            }
            else
            {
                return null;
            }
        }

        public Boolean BuildBathroom(RectangleF Rectangle)
        {
            if(CanBuild(Data.BathroomBuildCost, Rectangle) == true)
            {
                var Bathroom = new Bathroom();

                Bathroom.SetRectangle(Rectangle);
                _Build(Data.BathroomBuildCost, Bathroom);
                _Bathrooms.Add(Bathroom);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuildOffice(RectangleF Rectangle)
        {
            if(CanBuild(Data.OfficeBuildCost, Rectangle) == true)
            {
                var Office = new Office();

                Office.SetRectangle(Rectangle);
                _Build(Data.OfficeBuildCost, Office);
                _Offices.Add(Office);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuildStairs(RectangleF Rectangle)
        {
            if(CanBuild(Data.StairsBuildCost, Rectangle) == true)
            {
                var Stairs = new Stairs();

                Stairs.SetRectangle(Rectangle);
                _Build(Data.StairsBuildCost, Stairs);
                _Stairs.Add(Stairs);

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
                var Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.AccountantHireCost, Desk.GetMidLocation());

                    var Accountant = new Accountant();

                    Accountant.AssignDesk(Desk);
                    _Persons.Add(Accountant);
                    _Accountants.Add(Accountant);
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
                var Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.WorkerHireCost, Desk.GetMidLocation());

                    var Worker = new Worker();

                    Worker.AssignDesk(Desk);
                    _Persons.Add(Worker);
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
                var Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.ITTechHireCost, Desk.GetMidLocation());

                    var ITTech = new ITTech();

                    ITTech.AssignDesk(Desk);
                    _Persons.Add(ITTech);
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
                var Desk = GetDesk(Rectangle.Location);

                if((Desk != null) && (Desk.IsFree() == true))
                {
                    SpendMoney(Data.JanitorHireCost, Desk.GetMidLocation());

                    var Janitor = new Janitor();

                    Janitor.AssignDesk(Desk);
                    _Persons.Add(Janitor);
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
                var Office = GetOffice(Rectangle.Location);

                if((Office != null) && (Office.Cat == null))
                {
                    var Cat = new Cat();

                    Cat.SetRectangle(Rectangle);
                    Cat.AssignOffice(Office);
                    _CatStock -= 1;

                    return true;
                }
            }

            return false;
        }

        public Office GetOffice(PointF Location)
        {
            foreach(var Office in _Offices)
            {
                if(Office.GetRectangle().Contains(Location) == true)
                {
                    return Office;
                }
            }

            return null;
        }

        private Desk _GetDesk(Office Office, PointF Location)
        {
            Debug.Assert(Office != null);

            var NearestDeskDistanceSquared = Single.MaxValue;
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

        private UInt64 _GetEuros(UInt64 Cents)
        {
            return Cents / 100;
        }

        private UInt64 _GetCents(UInt64 Cents)
        {
            return Cents % 100;
        }

        public String GetMoneyString(UInt64 Cents)
        {
            return _GetEuros(Cents) + "." + _GetCents(Cents).ToString("00") + "€";
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

        public Desk GetDesk(PointF Location)
        {
            var Office = GetOffice(Location);

            if(Office == null)
            {
                return null;
            }

            var Desk = _GetDesk(Office, Location);

            return Desk;
        }

        public void EnqueueBrokenThing(Pair<Office, BrokenThing> BrokenThing)
        {
            _BrokenThings.Add(BrokenThing);
        }

        public Pair<Office, BrokenThing> DequeueBrokenThing()
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
            _OccupyFreeSpace(Building.GetRectangle());
            _WidenBuilding(Building.GetRectangle());
            _Buildings.Add(Building);
        }

        public void UpdateBuilding(UInt64 Cost, Building Building)
        {
            SpendMoney(Cost, Building.GetMidLocation());
            _OccupyFreeSpace(Building.GetRectangle());
            _WidenBuilding(Building.GetRectangle());
        }

        public Boolean CanBuild(UInt64 Cost, RectangleF Rectangle)
        {
            return (_EnoughCents(Cost) == true) && (_InBuildableWorld(Rectangle) == true) && (_InFreeSpace(Rectangle) == true) && (_CompletelyOnTopOfBuilding(Rectangle) == true);
        }

        private Boolean _EnoughCents(UInt64 Cents)
        {
            return _Cents >= Cents;
        }

        private Boolean _InBuildableWorld(RectangleF Rectangle)
        {
            return (Rectangle.Y >= 0.0f) && (Rectangle.X >= 0.0f) && (Rectangle.X + Rectangle.Width < _WorldBlockWidth.ToSingle());
        }

        private Boolean _InFreeSpace(RectangleF Rectangle)
        {
            var Result = true;

            for(var Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
            {
                Result &= _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column];
            }

            return Result;
        }

        private Boolean _CompletelyOnTopOfBuilding(RectangleF Rectangle)
        {
            var Result = true;

            if(Rectangle.Y > 0.0f)
            {
                Result &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].First <= Rectangle.X.GetFlooredAsInt32();
                Result &= _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() - 1].Second >= Rectangle.Right.GetFlooredAsInt32();
            }

            return Result;
        }

        private void _InitializeFreeSpaceAndMinimumMaximum()
        {
            _FreeSpace.Clear();
            for(var Index = 0; Index < _WorldBlockHeight; ++Index)
            {
                _FreeSpace.Add(new BitArray(_WorldBlockWidth, true));
            }
            _BuildingMinimumMaximum.Clear();
            for(var Index = 0; Index < _WorldBlockHeight; ++Index)
            {
                _BuildingMinimumMaximum.Add(new Pair<Int32, Int32>(Int32.MaxValue, Int32.MinValue));
            }
        }

        private void _OccupyFreeSpace(RectangleF Rectangle)
        {
            for(var Row = 0; Row < Rectangle.Height.GetFlooredAsInt32(); ++Row)
            {
                for(var Column = 0; Column < Rectangle.Width.GetFlooredAsInt32(); ++Column)
                {
                    _FreeSpace[Rectangle.Y.GetFlooredAsInt32() + Row][Rectangle.X.GetFlooredAsInt32() + Column] = false;
                }
            }
        }

        private void _WidenBuilding(RectangleF Rectangle)
        {
            for(var Row = 0; Row < Rectangle.Height.GetFlooredAsInt32(); ++Row)
            {
                if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() + Row].First > Rectangle.X.GetFlooredAsInt32())
                {
                    _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() + Row].First = Rectangle.X.GetFlooredAsInt32();
                }
                if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() + Row].Second < Rectangle.Right.GetFlooredAsInt32())
                {
                    _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32() + Row].Second = Rectangle.Right.GetFlooredAsInt32();
                }
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("accountants", _Accountants);
            ObjectStore.Save("bathrooms", _Bathrooms);
            ObjectStore.Save("broken-things", _BrokenThings);
            ObjectStore.Save("buildings", _Buildings);
            ObjectStore.Save("cat-stock", _CatStock);
            ObjectStore.Save("cents", _Cents);
            ObjectStore.Save("minutes", _Minutes);
            ObjectStore.Save("next-cat-at-number-of-employees", _NextCatAtNumberOfEmployees);
            ObjectStore.Save("offices", _Offices);
            ObjectStore.Save("persons", _Persons);
            ObjectStore.Save("stairs", _Stairs);
            ObjectStore.Save("sub-minute", _SubMinute);
            ObjectStore.Save("world-width", _WorldBlockWidth);
            ObjectStore.Save("world-height", _WorldBlockHeight);
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
            foreach(var Building in ObjectStore.LoadBuildings("buildings"))
            {
                _Buildings.Add(Building);
            }
            _CatStock = ObjectStore.LoadUInt32Property("cat-stock");
            _Cents = ObjectStore.LoadUInt64Property("cents");
            _Minutes = ObjectStore.LoadUInt64Property("minutes");
            _NextCatAtNumberOfEmployees = ObjectStore.LoadUInt32Property("next-cat-at-number-of-employees");
            foreach(var Office in ObjectStore.LoadOffices("offices"))
            {
                _Offices.Add(Office);
            }
            foreach(var Person in ObjectStore.LoadPersons("persons"))
            {
                _Persons.Add(Person);
            }
            foreach(var Stairs in ObjectStore.LoadStairs("stairs"))
            {
                _Stairs.Add(Stairs);
            }
            _SubMinute = ObjectStore.LoadSingleProperty("sub-minute");
            _WorldBlockWidth = ObjectStore.LoadInt32Property("world-width");
            _WorldBlockHeight = ObjectStore.LoadInt32Property("world-height");
            _InitializeFreeSpaceAndMinimumMaximum();
            foreach(var Building in _Buildings)
            {
                _OccupyFreeSpace(Building.GetRectangle());
                _WidenBuilding(Building.GetRectangle());
            }
        }
    }
}

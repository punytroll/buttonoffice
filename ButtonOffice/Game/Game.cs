namespace ButtonOffice
{
    internal class Game
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
                    for(System.Int32 Column = 0; Column < Rectangle.Width; ++Column)
                    {
                        _FreeSpace[Rectangle.Y.GetFlooredAsInt32()][Rectangle.X.GetFlooredAsInt32() + Column] = false;
                    }
                    if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First > Rectangle.X)
                    {
                        _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].First = Rectangle.X.GetFlooredAsInt32();
                    }
                    if(_BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second < Rectangle.Right.GetFlooredAsInt32())
                    {
                        _BuildingMinimumMaximum[Rectangle.Y.GetFlooredAsInt32()].Second = Rectangle.Right.GetFlooredAsInt32();
                    }

                    Office Office = new Office();

                    Office.SetRectangle(Rectangle);
                    Office.FirstDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.SecondDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.ThirdDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                    Office.FourthDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
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
            System.IO.StreamWriter Writer = new System.IO.StreamWriter(FileName);
            System.Globalization.CultureInfo Format = System.Globalization.CultureInfo.InvariantCulture;

            Writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Writer.WriteLine();
            Writer.WriteLine("<button-office version=\"1.0\">");
            Writer.WriteLine("  <cat-stock type=\"System.UInt32\">" + _CatStock.ToString() + "</cat-stock>");
            Writer.WriteLine("  <cents type=\"System.UInt64\">" + _Cents.ToString() + "</cents>");
            Writer.WriteLine("  <minute type=\"System.UInt64\">" + _Minutes.ToString() + "</minutes>");
            Writer.WriteLine("  <next-cat-at-number-of-employees type=\"Ssytem.UInt32\">" + _NextCatAtNumberOfEmployees.ToString(Format) + "</next-cat-at-number-of-employees>");

            System.Collections.Generic.Dictionary<ButtonOffice.Person, System.UInt32> PersonMap = new System.Collections.Generic.Dictionary<ButtonOffice.Person, System.UInt32>();

            for(System.UInt32 Identifier = 0; Identifier < _Persons.Count; ++Identifier)
            {
                PersonMap.Add(_Persons[Identifier.ToInt32()], Identifier);
            }

            System.Collections.Generic.Dictionary<ButtonOffice.Office, System.UInt32> OfficeMap = new System.Collections.Generic.Dictionary<ButtonOffice.Office, System.UInt32>();

            for(System.UInt32 Identifier = 0; Identifier < _Offices.Count; ++Identifier)
            {
                OfficeMap.Add(_Offices[Identifier.ToInt32()], Identifier);
            }
            for(System.UInt32 Identifier = 0; Identifier < _Offices.Count; ++Identifier)
            {
                ButtonOffice.Office Office = _Offices[Identifier.ToInt32()];

                Writer.WriteLine("  <office>");
                Writer.WriteLine("    <background-color type=\"System.Drawing.Color\">");
                Writer.WriteLine("      <red type=\"System.Byte\">" + Office.BackgroundColor.R.ToString(Format) + "</red>");
                Writer.WriteLine("      <green type=\"System.Byte\">" + Office.BackgroundColor.G.ToString(Format) + "</green>");
                Writer.WriteLine("      <blue type=\"System.Byte\">" + Office.BackgroundColor.B.ToString(Format) + "</blue>");
                Writer.WriteLine("      <alpha type=\"System.Byte\">" + Office.BackgroundColor.A.ToString(Format) + "</alpha>");
                Writer.WriteLine("    </baclground-color>");
                Writer.WriteLine("    <border-color type=\"System.Drawing.Color\">");
                Writer.WriteLine("      <red type=\"System.Byte\">" + Office.BorderColor.R.ToString(Format) + "</red>");
                Writer.WriteLine("      <green type=\"System.Byte\">" + Office.BorderColor.G.ToString(Format) + "</green>");
                Writer.WriteLine("      <blue type=\"System.Byte\">" + Office.BorderColor.B.ToString(Format) + "</blue>");
                Writer.WriteLine("      <alpha type=\"System.Byte\">" + Office.BorderColor.A.ToString(Format) + "</alpha>");
                Writer.WriteLine("    </border-color>");
                Writer.WriteLine("    <identifier type=\"System.UInt32\">" + Identifier.ToString(Format) + "</identifier>");
                Writer.WriteLine("    <rectangle>");
                Writer.WriteLine("      <x type=\"System.Single\">" + Office.GetX().ToString(Format) + "</x>");
                Writer.WriteLine("      <y type=\"System.Single\">" + Office.GetY().ToString(Format) + "</y>");
                Writer.WriteLine("      <width type=\"System.Single\">" + Office.GetWidth().ToString(Format) + "</width>");
                Writer.WriteLine("      <height type=\"System.Single\">" + Office.GetHeight().ToString(Format) + "</height>");
                Writer.WriteLine("    </rectangle>");
                Writer.WriteLine("    <first-desk>");
                Writer.WriteLine("      <trash-level type=\"System.Single\">" + Office.FirstDesk.TrashLevel.ToString(Format) + "</trash-level>");
                Writer.WriteLine("      <minutes-until-computer-broken type=\"System.Single\">" + Office.FirstDesk.GetMinutesUntilComputerBroken().ToString(Format) + "</minutes-until-computer-broken>");
                if(Office.FirstDesk.Janitor != null)
                {
                    Writer.WriteLine("      <janitor-identifier type=\"System.UInt32\">" + PersonMap[Office.FirstDesk.Janitor].ToString(Format) + "</janitor-identifier>");
                }
                Writer.WriteLine("    </first-desk>");
                Writer.WriteLine("    <second-desk>");
                Writer.WriteLine("      <trash-level type=\"System.Single\">" + Office.SecondDesk.TrashLevel.ToString(Format) + "</trash-level>");
                Writer.WriteLine("      <minutes-until-computer-broken type=\"System.Single\">" + Office.SecondDesk.GetMinutesUntilComputerBroken().ToString(Format) + "</minutes-until-computer-broken>");
                if(Office.SecondDesk.Janitor != null)
                {
                    Writer.WriteLine("      <janitor-identifier type=\"System.UInt32\">" + PersonMap[Office.SecondDesk.Janitor].ToString(Format) + "</janitor-identifier>");
                }
                Writer.WriteLine("    </second-desk>");
                Writer.WriteLine("    <third-desk>");
                Writer.WriteLine("      <trash-level type=\"System.Single\">" + Office.ThirdDesk.TrashLevel.ToString(Format) + "</trash-level>");
                Writer.WriteLine("      <minutes-until-computer-broken type=\"System.Single\">" + Office.ThirdDesk.GetMinutesUntilComputerBroken().ToString(Format) + "</minutes-until-computer-broken>");
                if(Office.ThirdDesk.Janitor != null)
                {
                    Writer.WriteLine("      <janitor-identifier type=\"System.UInt32\">" + PersonMap[Office.ThirdDesk.Janitor].ToString(Format) + "</janitor-identifier>");
                }
                Writer.WriteLine("    </third-desk>");
                Writer.WriteLine("    <fourth-desk>");
                Writer.WriteLine("      <trash-level type=\"System.Single\">" + Office.FourthDesk.TrashLevel.ToString(Format) + "</trash-level>");
                Writer.WriteLine("      <minutes-until-computer-broken type=\"System.Single\">" + Office.FourthDesk.GetMinutesUntilComputerBroken().ToString(Format) + "</minutes-until-computer-broken>");
                if(Office.FourthDesk.Janitor != null)
                {
                    Writer.WriteLine("      <janitor-identifier type=\"System.UInt32\">" + PersonMap[Office.FourthDesk.Janitor].ToString(Format) + "</janitor-identifier>");
                }
                Writer.WriteLine("    </fourth-desk>");
                Writer.WriteLine("    <first-lamp>");
                Writer.WriteLine("      <minutes-until-broken type=\"System.Single\">" + Office.FirstLamp.GetMinutesUntilBroken().ToString(Format) + "</minutes-until-broken>");
                Writer.WriteLine("      <rectangle>");
                Writer.WriteLine("        <x type=\"System.Single\">" + Office.FirstLamp.GetX().ToString(Format) + "</x>");
                Writer.WriteLine("        <y type=\"System.Single\">" + Office.FirstLamp.GetY().ToString(Format) + "</y>");
                Writer.WriteLine("        <width type=\"System.Single\">" + Office.FirstLamp.GetWidth().ToString(Format) + "</width>");
                Writer.WriteLine("        <height type=\"System.Single\">" + Office.FirstLamp.GetHeight().ToString(Format) + "</height>");
                Writer.WriteLine("      </rectangle>");
                Writer.WriteLine("    </first-lamp>");
                Writer.WriteLine("    <second-lamp>");
                Writer.WriteLine("      <minutes-until-broken type=\"System.Single\">" + Office.SecondLamp.GetMinutesUntilBroken().ToString(Format) + "</minutes-until-broken>");
                Writer.WriteLine("      <rectangle>");
                Writer.WriteLine("        <x type=\"System.Single\">" + Office.SecondLamp.GetX().ToString(Format) + "</x>");
                Writer.WriteLine("        <y type=\"System.Single\">" + Office.SecondLamp.GetY().ToString(Format) + "</y>");
                Writer.WriteLine("        <width type=\"System.Single\">" + Office.SecondLamp.GetWidth().ToString(Format) + "</width>");
                Writer.WriteLine("        <height type=\"System.Single\">" + Office.SecondLamp.GetHeight().ToString(Format) + "</height>");
                Writer.WriteLine("      </rectangle>");
                Writer.WriteLine("    </second-lamp>");
                Writer.WriteLine("    <third-lamp>");
                Writer.WriteLine("      <minutes-until-broken type=\"System.Single\">" + Office.ThirdLamp.GetMinutesUntilBroken().ToString(Format) + "</minutes-until-broken>");
                Writer.WriteLine("      <rectangle>");
                Writer.WriteLine("        <x type=\"System.Single\">" + Office.ThirdLamp.GetX().ToString(Format) + "</x>");
                Writer.WriteLine("        <y type=\"System.Single\">" + Office.ThirdLamp.GetY().ToString(Format) + "</y>");
                Writer.WriteLine("        <width type=\"System.Single\">" + Office.ThirdLamp.GetWidth().ToString(Format) + "</width>");
                Writer.WriteLine("        <height type=\"System.Single\">" + Office.ThirdLamp.GetHeight().ToString(Format) + "</height>");
                Writer.WriteLine("      </rectangle>");
                Writer.WriteLine("    </third-lamp>");
                if(Office.Cat != null)
                {
                    Writer.WriteLine("  <cat>");
                    Writer.WriteLine("    <action-state type=\"ButtonOffice.ActionState\">" + Office.Cat.GetActionState().ToString() + "</action-state>");
                    Writer.WriteLine("    <background-color type=\"System.Drawing.Color\">");
                    Writer.WriteLine("      <red type=\"System.Byte\">" + Office.Cat.BackgroundColor.R.ToString(Format) + "</red>");
                    Writer.WriteLine("      <green type=\"System.Byte\">" + Office.Cat.BackgroundColor.G.ToString(Format) + "</green>");
                    Writer.WriteLine("      <blue type=\"System.Byte\">" + Office.Cat.BackgroundColor.B.ToString(Format) + "</blue>");
                    Writer.WriteLine("      <alpha type=\"System.Byte\">" + Office.Cat.BackgroundColor.A.ToString(Format) + "</alpha>");
                    Writer.WriteLine("    </baclground-color>");
                    Writer.WriteLine("    <border-color type=\"System.Drawing.Color\">");
                    Writer.WriteLine("      <red type=\"System.Byte\">" + Office.Cat.BorderColor.R.ToString(Format) + "</red>");
                    Writer.WriteLine("      <green type=\"System.Byte\">" + Office.Cat.BorderColor.G.ToString(Format) + "</green>");
                    Writer.WriteLine("      <blue type=\"System.Byte\">" + Office.Cat.BorderColor.B.ToString(Format) + "</blue>");
                    Writer.WriteLine("      <alpha type=\"System.Byte\">" + Office.Cat.BorderColor.A.ToString(Format) + "</alpha>");
                    Writer.WriteLine("    </border-color>");
                    Writer.WriteLine("    <rectangle>");
                    Writer.WriteLine("      <x type=\"System.Single\">" + Office.Cat.GetX().ToString(Format) + "</x>");
                    Writer.WriteLine("      <y type=\"System.Single\">" + Office.Cat.GetY().ToString(Format) + "</y>");
                    Writer.WriteLine("      <width type=\"System.Single\">" + Office.Cat.GetWidth().ToString(Format) + "</width>");
                    Writer.WriteLine("      <height type=\"System.Single\">" + Office.Cat.GetHeight().ToString(Format) + "</height>");
                    Writer.WriteLine("    </rectangle>");
                    Writer.WriteLine("  </cat>");
                }
                Writer.WriteLine("  </office>");
            }
            for(System.UInt32 Identifier = 0; Identifier < _Persons.Count; ++Identifier)
            {
                ButtonOffice.Person Person = _Persons[Identifier.ToInt32()];

                Writer.WriteLine("  <person>");
                Writer.WriteLine("    <type type=\"ButtonOffice.Type\">" + Person.Type.ToString() + "</type>");
                Writer.WriteLine("    <background-color type=\"System.Drawing.Color\">");
                Writer.WriteLine("      <red type=\"System.Byte\">" + Person.BackgroundColor.R.ToString(Format) + "</red>");
                Writer.WriteLine("      <green type=\"System.Byte\">" + Person.BackgroundColor.G.ToString(Format) + "</green>");
                Writer.WriteLine("      <blue type=\"System.Byte\">" + Person.BackgroundColor.B.ToString(Format) + "</blue>");
                Writer.WriteLine("      <alpha type=\"System.Byte\">" + Person.BackgroundColor.A.ToString(Format) + "</alpha>");
                Writer.WriteLine("    </baclground-color>");
                Writer.WriteLine("    <border-color type=\"System.Drawing.Color\">");
                Writer.WriteLine("      <red type=\"System.Byte\">" + Person.BorderColor.R.ToString(Format) + "</red>");
                Writer.WriteLine("      <green type=\"System.Byte\">" + Person.BorderColor.G.ToString(Format) + "</green>");
                Writer.WriteLine("      <blue type=\"System.Byte\">" + Person.BorderColor.B.ToString(Format) + "</blue>");
                Writer.WriteLine("      <alpha type=\"System.Byte\">" + Person.BorderColor.A.ToString(Format) + "</alpha>");
                Writer.WriteLine("    </border-color>");
                Writer.WriteLine("    <rectangle type=\"System.Drawing.RectangleF\">");
                Writer.WriteLine("      <x type=\"System.Single\">" + Person.GetX().ToString(Format) + "</x>");
                Writer.WriteLine("      <y type=\"System.Single\">" + Person.GetY().ToString(Format) + "</y>");
                Writer.WriteLine("      <width type=\"System.Single\">" + Person.GetWidth().ToString(Format) + "</width>");
                Writer.WriteLine("      <height type=\"System.Single\">" + Person.GetHeight().ToString(Format) + "</height>");
                Writer.WriteLine("    </rectangle>");
                Writer.WriteLine("  </person>");
            }
            foreach(System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing in _BrokenThings)
            {
                Writer.WriteLine("  <broken-thing>");
                Writer.WriteLine("    <office-identifier type=\"System.UInt32\">" + OfficeMap[BrokenThing.First].ToString(Format) + "</office-identifier>");
                Writer.WriteLine("    <broken-thing type=\"ButtonOffice.BrokenThing\">" + BrokenThing.Second.ToString() + "</Broken-thing>");
                Writer.WriteLine("  </broken-thing>");
            }
            Writer.WriteLine("</button-office>");
            Writer.Close();
        }
    }
}

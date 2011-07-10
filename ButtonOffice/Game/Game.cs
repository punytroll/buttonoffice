namespace ButtonOffice
{
    internal class Game
    {
        private System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> _BrokenThings;
        private System.UInt64 _Cents;
        private System.Collections.Generic.List<System.Collections.BitArray> _FreeSpace;
        private System.UInt64 _Minutes;
        private System.Collections.Generic.List<ButtonOffice.Office> _Offices;
        private System.Collections.Generic.List<ButtonOffice.Person> _Persons;
        private System.Single _SubMinute;
        private System.Collections.Generic.List<System.Collections.BitArray> _WalkSpace;

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

        public Game()
        {
            _BrokenThings = new System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>>();
            _Cents = ButtonOffice.Data.StartCents;
            _FreeSpace = new System.Collections.Generic.List<System.Collections.BitArray>();
            for(System.Int32 Index = 0; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                _FreeSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, true));
            }
            _Minutes = ButtonOffice.Data.StartMinutes;
            _Offices = new System.Collections.Generic.List<ButtonOffice.Office>();
            _Persons = new System.Collections.Generic.List<ButtonOffice.Person>();
            _SubMinute = 0.0f;
            _WalkSpace = new System.Collections.Generic.List<System.Collections.BitArray>();
            _WalkSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, true));
            for(System.Int32 Index = 1; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                _WalkSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, false));
            }
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

        public System.UInt64 GetEuros()
        {
            return _Cents / 100;
        }

        public System.UInt64 GetCents()
        {
            return _Cents % 100;
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
            System.Boolean BuildAllowed = true;

            for(System.Int32 Column = 0; Column < Rectangle.Width; ++Column)
            {
                BuildAllowed &= _FreeSpace[Rectangle.Y.GetIntegerAsInt32()][Rectangle.X.GetIntegerAsInt32() + Column];
                if(Rectangle.Y > 0)
                {
                    BuildAllowed &= !_FreeSpace[Rectangle.Y.GetIntegerAsInt32() - 1][Rectangle.X.GetIntegerAsInt32() + Column];
                }
            }
            if((BuildAllowed == true) && (_Cents >= ButtonOffice.Data.OfficeBuildCost))
            {
                _Cents -= ButtonOffice.Data.OfficeBuildCost;
                for(System.Int32 Column = 0; Column < Rectangle.Width; ++Column)
                {
                    _FreeSpace[Rectangle.Y.GetIntegerAsInt32()][Rectangle.X.GetIntegerAsInt32() + Column] = false;
                }
                for(System.Int32 Column = 0; Column < Rectangle.Width; ++Column)
                {
                    _WalkSpace[Rectangle.Y.GetIntegerAsInt32()][Rectangle.X.GetIntegerAsInt32() + Column] = true;
                }

                Office Office = new Office();

                Office.SetRectangle(Rectangle);

                System.Random Random = new System.Random();

                Office.FirstDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.SecondDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.ThirdDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.FourthDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.FirstLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                Office.SecondLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                Office.ThirdLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                _Offices.Add(Office);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean HireWorker(System.Drawing.RectangleF Rectangle)
        {
            Office Office = _GetOffice(Rectangle.Location);

            if((_Cents >= ButtonOffice.Data.WorkerHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
            {
                _Cents -= ButtonOffice.Data.WorkerHireCost;

                ButtonOffice.Worker Worker = new ButtonOffice.Worker();
                System.Random Random = new System.Random();

                Worker.ArrivesAtDayMinute = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;

                ButtonOffice.Desk Desk = Office.GetFreeDesk();

                Desk.Person = Worker;
                Worker.Desk = Desk;
                _Persons.Add(Worker);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean HireITTech(System.Drawing.RectangleF Rectangle)
        {
            Office Office = _GetOffice(Rectangle.Location);

            if((_Cents >= ButtonOffice.Data.ITTechHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
            {
                _Cents -= ButtonOffice.Data.ITTechHireCost;

                ButtonOffice.ITTech ITTech = new ButtonOffice.ITTech();
                System.Random Random = new System.Random();

                ITTech.ArrivesAtDayMinute = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;

                ButtonOffice.Desk Desk = Office.GetFreeDesk();

                Desk.Person = ITTech;
                ITTech.Desk = Desk;
                _Persons.Add(ITTech);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean HireJanitor(System.Drawing.RectangleF Rectangle)
        {
            Office Office = _GetOffice(Rectangle.Location);

            if((_Cents >= ButtonOffice.Data.JanitorHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
            {
                _Cents -= ButtonOffice.Data.JanitorHireCost;

                ButtonOffice.Janitor Janitor = new ButtonOffice.Janitor();
                System.Random Random = new System.Random();

                Janitor.ArrivesAtDayMinute = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.JanitorStartMinute, 300) % 1440;

                ButtonOffice.Desk Desk = Office.GetFreeDesk();

                Desk.Person = Janitor;
                Janitor.Desk = Desk;
                _Persons.Add(Janitor);

                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Boolean PlaceCat(System.Drawing.RectangleF Rectangle)
        {
            ButtonOffice.Office Office = _GetOffice(Rectangle.Location);

            if((Office != null) && (Office.Cat == null))
            {
                ButtonOffice.Cat Cat = new ButtonOffice.Cat();

                Cat.SetRectangle(Rectangle);
                Office.Cat = Cat;
                Cat.Office = Office;

                return true;
            }
            else
            {
                return false;
            }
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
    }
}

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
                if(Office.FirstLamp.IsBroken() == false)
                {
                    Office.FirstLamp.SetMinutesUntilBroken(Office.FirstLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.FirstLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.FirstLamp));
                    }
                }
                if(Office.SecondLamp.IsBroken() == false)
                {
                    Office.SecondLamp.SetMinutesUntilBroken(Office.SecondLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.SecondLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.SecondLamp));
                    }
                }
                if(Office.ThirdLamp.IsBroken() == false)
                {
                    Office.ThirdLamp.SetMinutesUntilBroken(Office.ThirdLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.ThirdLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.ThirdLamp));
                    }
                }
            }
            foreach(ButtonOffice.Person Person in _Persons)
            {
                switch(Person.Type)
                {
                case ButtonOffice.Type.ITTech:
                    {
                        _MoveITTech(Person as ButtonOffice.ITTech, GameMinutes);

                        break;
                    }
                case ButtonOffice.Type.Janitor:
                    {
                        _MoveJanitor(Person as ButtonOffice.Janitor, GameMinutes);

                        break;
                    }
                case ButtonOffice.Type.Worker:
                    {
                        _MoveWorker(Person as ButtonOffice.Worker, GameMinutes);

                        break;
                    }
                }
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

        public System.UInt64 GetDay()
        {
            return _Minutes / 1440;
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

                Office.FirstDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.SecondDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.ThirdDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.FourthDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                Office.FirstLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                Office.SecondLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                Office.ThirdLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
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

                Worker.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;
                Worker.WorkMinutes = ButtonOffice.Data.WorkerWorkMinutes;
                _PlanNextWorkDay(Worker);
                Worker.ActionState = ButtonOffice.ActionState.AtHome;
                Worker.AnimationState = ButtonOffice.AnimationState.Hidden;
                Worker.AnimationFraction = 0.0f;
                Worker.BackgroundColor = ButtonOffice.Data.WorkerBackgroundColor;
                Worker.BorderColor = ButtonOffice.Data.WorkerBorderColor;
                Worker.Wage = ButtonOffice.Data.WorkerWage;
                Worker.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                Worker.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.5f));
                if(new System.Random().NextDouble() < 0.5)
                {
                    Worker.LivingSide = ButtonOffice.LivingSide.Left;
                    Worker.SetLocation(-10.0f, 0.0f);
                }
                else
                {
                    Worker.LivingSide = ButtonOffice.LivingSide.Right;
                    Worker.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                }

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

                ITTech.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;
                ITTech.WorkMinutes = ButtonOffice.Data.ITTechWorkMinutes;
                _PlanNextWorkDay(ITTech);
                ITTech.ActionState = ButtonOffice.ActionState.AtHome;
                ITTech.AnimationState = ButtonOffice.AnimationState.Hidden;
                ITTech.AnimationFraction = 0.0f;
                ITTech.BackgroundColor = ButtonOffice.Data.ITTechBackgroundColor;
                ITTech.BorderColor = ButtonOffice.Data.ITTechBorderColor;
                ITTech.Wage = ButtonOffice.Data.ITTechWage;
                ITTech.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                ITTech.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.8f));
                if(new System.Random().NextDouble() < 0.5)
                {
                    ITTech.LivingSide = ButtonOffice.LivingSide.Left;
                    ITTech.SetLocation(-10.0f, 0.0f);
                }
                else
                {
                    ITTech.LivingSide = ButtonOffice.LivingSide.Right;
                    ITTech.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                }

                ButtonOffice.Desk Desk = Office.GetFreeDesk();

                Desk.Person = ITTech;
                ITTech.Desk = Desk;
                ITTech.SetX(Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f);
                ITTech.SetY(Desk.GetY());
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

                Janitor.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.JanitorStartMinute, 300) % 1440;
                Janitor.WorkMinutes = ButtonOffice.Data.JanitorWorkMinutes;
                _PlanNextWorkDay(Janitor);
                Janitor.ActionState = ButtonOffice.ActionState.AtHome;
                Janitor.AnimationState = ButtonOffice.AnimationState.Hidden;
                Janitor.AnimationFraction = 0.0f;
                Janitor.BackgroundColor = ButtonOffice.Data.JanitorBackgroundColor;
                Janitor.BorderColor = ButtonOffice.Data.JanitorBorderColor;
                Janitor.Wage = ButtonOffice.Data.JanitorWage;
                Janitor.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                Janitor.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.5f));
                if(new System.Random().NextDouble() < 0.5)
                {
                    Janitor.LivingSide = ButtonOffice.LivingSide.Left;
                    Janitor.SetLocation(-10.0f, 0.0f);
                }
                else
                {
                    Janitor.LivingSide = ButtonOffice.LivingSide.Right;
                    Janitor.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                }

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

        private void _MoveITTech(ButtonOffice.ITTech ITTech, System.Single GameMinutes)
        {
            switch(ITTech.ActionState)
            {
            case ButtonOffice.ActionState.Working:
                {
                    ITTech.ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > ITTech.ArrivesAtMinute)
                    {
                        ITTech.ActionState = ButtonOffice.ActionState.Arriving;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(ITTech.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            ITTech.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            ITTech.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        ITTech.WalkTo = new System.Drawing.PointF(ITTech.Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f, ITTech.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.Working;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Standing;
                        ITTech.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.ActionState = ButtonOffice.ActionState.AtHome;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(ITTech);
                    }

                    break;
                }
            case ButtonOffice.ActionState.WaitingForBrokenThings:
                {
                    if(_Minutes > ITTech.LeavesAtMinute)
                    {
                        if(ITTech.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            ITTech.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            ITTech.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        ITTech.ActionState = ButtonOffice.ActionState.Leaving;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        ITTech.AnimationFraction = 0.0f;
                        _Cents -= ITTech.Wage;
                    }
                    else
                    {
                        if(_BrokenThings.Count > 0)
                        {
                            System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = _BrokenThings.Dequeue();

                            switch(BrokenThing.Second)
                            {
                            case ButtonOffice.BrokenThing.FirstComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.FirstDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.SecondDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.ThirdDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FourthComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.FourthDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FirstLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampOneX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampTwoX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampThreeX, BrokenThing.First.GetY());

                                    break;
                                }
                            }
                            ITTech.SetRepairingTarget(BrokenThing.First, BrokenThing.Second);
                            ITTech.ActionState = ButtonOffice.ActionState.GoingToRepair;
                            ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                            ITTech.AnimationFraction = 0.0f;
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToRepair:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.Repairing;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Repairing;
                        ITTech.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Repairing:
                {
                    ITTech.AnimationFraction += ButtonOffice.Data.ITTechRepairSpeed * GameMinutes;
                    if(ITTech.AnimationFraction >= 1.0f)
                    {
                        System.Random Random = new System.Random();

                        switch(ITTech.GetBrokenThing())
                        {
                        case ButtonOffice.BrokenThing.FirstComputer:
                            {
                                ITTech.GetOffice().FirstDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondComputer:
                            {
                                ITTech.GetOffice().SecondDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdComputer:
                            {
                                ITTech.GetOffice().ThirdDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FourthComputer:
                            {
                                ITTech.GetOffice().FourthDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FirstLamp:
                            {
                                ITTech.GetOffice().FirstLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondLamp:
                            {
                                ITTech.GetOffice().SecondLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdLamp:
                            {
                                ITTech.GetOffice().ThirdLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        }
                        ITTech.DropRepairingTarget();
                        ITTech.ActionState = ButtonOffice.ActionState.GoingToDesk;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        ITTech.AnimationFraction = 0.0f;
                        ITTech.WalkTo = new System.Drawing.PointF(ITTech.Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f, ITTech.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToDesk:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Standing;
                        ITTech.AnimationFraction = 0.0f;
                        ITTech.Desk.TrashLevel += 1.0f;
                    }

                    break;
                }
            }
        }

        private void _MoveJanitor(ButtonOffice.Janitor Janitor, System.Single GameMinutes)
        {
            switch(Janitor.ActionState)
            {
            case ButtonOffice.ActionState.GoingToClean:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.Cleaning;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Cleaning;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Cleaning:
                {
                    ButtonOffice.Desk Desk = Janitor.PeekFirstCleaningTarget();

                    if(Desk.Janitor == null)
                    {
                        Desk.Janitor = Janitor;
                    }
                    if(Desk.Janitor == Janitor)
                    {
                        if(Desk.TrashLevel > 0.0f)
                        {
                            Janitor.AnimationFraction += ButtonOffice.Data.JanitorCleanSpeed * GameMinutes;
                            while(Janitor.AnimationFraction > 1.0f)
                            {
                                Desk.TrashLevel -= ButtonOffice.Data.JanitorCleanAmount;
                                Janitor.AnimationFraction -= 1.0f;
                            }
                        }
                        if(Desk.TrashLevel <= 0.0f)
                        {
                            Desk.Janitor = null;
                            Desk.TrashLevel = 0.0f;
                            Janitor.DropFirstCleaningTarget();
                            Janitor.ActionState = ButtonOffice.ActionState.PickTrash;
                            Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                            Janitor.AnimationFraction = 0.0f;
                        }
                    }
                    else
                    {
                        Janitor.DropFirstCleaningTarget();
                        Janitor.ActionState = ButtonOffice.ActionState.PickTrash;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToDesk:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.WaitingToGoHome;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.PickTrash:
                {
                    if(_Minutes > Janitor.LeavesAtMinute)
                    {
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.ActionState = ButtonOffice.ActionState.Leaving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                        Janitor.DropAllCleaningTargets();
                        _Cents -= Janitor.Wage;
                    }
                    else
                    {
                        if(Janitor.GetNumberOfCleaningTargets() > 0)
                        {
                            Janitor.WalkTo = Janitor.PeekFirstCleaningTarget().GetLocation();
                            Janitor.ActionState = ButtonOffice.ActionState.GoingToClean;
                        }
                        else
                        {
                            Janitor.WalkTo = Janitor.Desk.GetLocation();
                            Janitor.ActionState = ButtonOffice.ActionState.GoingToDesk;
                        }
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.WaitingToGoHome:
                {
                    if(_Minutes > Janitor.LeavesAtMinute)
                    {
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.ActionState = ButtonOffice.ActionState.Leaving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                        Janitor.DropAllCleaningTargets();
                        _Cents -= Janitor.Wage;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    foreach(ButtonOffice.Office Office in _Offices)
                    {
                        Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                        Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                        Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                        Janitor.EnqueueCleaningTarget(Office.FourthDesk);
                    }
                    Janitor.ActionState = ButtonOffice.ActionState.PickTrash;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > Janitor.ArrivesAtMinute)
                    {
                        Janitor.ActionState = ButtonOffice.ActionState.Arriving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.WalkTo = new System.Drawing.PointF(Janitor.Desk.GetX() + (ButtonOffice.Data.DeskWidth - Janitor.GetWidth()) / 2.0f, Janitor.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.Working;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.ActionState = ButtonOffice.ActionState.AtHome;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(Janitor);
                    }

                    break;
                }
            }
        }

        private void _MoveWorker(ButtonOffice.Worker Worker, System.Single GameMinutes)
        {
            switch(Worker.ActionState)
            {
            case ButtonOffice.ActionState.PushingButton:
                {
                    if(_Minutes > Worker.LeavesAtMinute)
                    {
                        if(Worker.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Worker.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Worker.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Worker.ActionState = ButtonOffice.ActionState.Leaving;
                        Worker.AnimationState = ButtonOffice.AnimationState.Walking;
                        Worker.AnimationFraction = 0.0f;
                        _Cents -= Worker.Wage;
                    }
                    else
                    {
                        if(Worker.Desk.IsComputerBroken() == false)
                        {
                            Worker.Desk.SetMinutesUntilComputerBroken(Worker.Desk.GetMinutesUntilComputerBroken() - GameMinutes);
                            if(Worker.Desk.IsComputerBroken() == true)
                            {
                                Worker.AnimationState = ButtonOffice.AnimationState.Standing;
                                Worker.AnimationFraction = 0.0f;
                                if(Worker.Desk == Worker.Desk.Office.FirstDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.FirstComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.SecondDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.SecondComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.ThirdDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.ThirdComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.FourthDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.FourthComputer));
                                }
                            }
                            else
                            {
                                Worker.AnimationFraction += ButtonOffice.Data.WorkerWorkSpeed * GameMinutes;
                                while(Worker.AnimationFraction >= 1.0f)
                                {
                                    Worker.AnimationFraction -= 1.0f;
                                    Worker.Desk.TrashLevel += 1.0f;
                                    _Cents += 100;
                                }
                            }
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    Worker.ActionState = ButtonOffice.ActionState.PushingButton;
                    Worker.AnimationState = ButtonOffice.AnimationState.PushingButton;
                    Worker.AnimationFraction = 0.0f;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > Worker.ArrivesAtMinute)
                    {
                        Worker.ActionState = ButtonOffice.ActionState.Arriving;
                        Worker.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(Worker.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Worker.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            Worker.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Worker.WalkTo = new System.Drawing.PointF(Worker.Desk.GetX() + (ButtonOffice.Data.DeskWidth - Worker.GetWidth()) / 2.0f, Worker.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = Worker.WalkTo.X - Worker.GetX();
                    System.Single DeltaY = Worker.WalkTo.Y - Worker.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Worker.SetLocation(Worker.GetX() + DeltaX, Worker.GetY() + DeltaY);
                    }
                    else
                    {
                        Worker.SetLocation(Worker.WalkTo);
                        Worker.ActionState = ButtonOffice.ActionState.Working;
                        Worker.AnimationState = ButtonOffice.AnimationState.Standing;
                        Worker.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = Worker.WalkTo.X - Worker.GetX();
                    System.Single DeltaY = Worker.WalkTo.Y - Worker.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Worker.SetLocation(Worker.GetX() + DeltaX, Worker.GetY() + DeltaY);
                    }
                    else
                    {
                        Worker.ActionState = ButtonOffice.ActionState.AtHome;
                        Worker.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(Worker);
                    }

                    break;
                }
            }
        }

        private void _PlanNextWorkDay(ButtonOffice.Person Person)
        {
            System.UInt64 MinuteOfDay = GetMinuteOfDay();

            Person.ArrivesAtMinute = GetFirstMinuteOfToday() + Person.ArrivesAtDayMinute;
            if(Person.ArrivesAtMinute + Person.WorkMinutes < _Minutes)
            {
                Person.ArrivesAtMinute += 1440;
            }
            Person.LeavesAtMinute = Person.ArrivesAtMinute + Person.WorkMinutes;
        }
    }
}

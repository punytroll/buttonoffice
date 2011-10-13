namespace ButtonOffice.Goals
{
    internal class AccountantThink : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new ButtonOffice.Goals.PlanNextWorkDay());
                AppendSubGoal(new ButtonOffice.Goals.WaitUntilTimeToArrive());
                AppendSubGoal(new ButtonOffice.Goals.GoToWork());
                AppendSubGoal(new ButtonOffice.Goals.Accounting());
                AppendSubGoal(new ButtonOffice.Goals.GoHome());
            }
        }
    }

    internal class Accounting : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetAnimationState(ButtonOffice.AnimationState.Accounting);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
            else
            {
                if(Person.GetDesk().GetComputer().IsBroken() == false)
                {
                    Person.GetDesk().GetComputer().SetMinutesUntilBroken(Person.GetDesk().GetComputer().GetMinutesUntilBroken() - DeltaMinutes);
                    if(Person.GetDesk().GetComputer().IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0f);
                        Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
                        Person.SetAnimationFraction(0.0f);
                        if(Person.GetDesk() == Person.GetDesk().Office.FirstDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.FirstComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.SecondDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.SecondComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.ThirdDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.ThirdComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.FourthDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.FourthComputer));
                        }
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + ButtonOffice.Data.AccountantWorkSpeed * DeltaMinutes);
                        while(Person.GetActionFraction() >= 1.0f)
                        {
                            Person.SetActionFraction(Person.GetActionFraction() - 1.0f);
                            Person.GetDesk().TrashLevel += 2.0f;
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + ButtonOffice.Data.AccountantWorkSpeed * DeltaMinutes);
                        while(Person.GetAnimationFraction() >= 1.0f)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
                        }
                    }
                }
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }
    }

    internal class CleanDesk : ButtonOffice.Goal
    {
        private ButtonOffice.Desk _CleaningTarget;
        private System.Single _StartTrashLevel;

        public CleanDesk()
        {
            _CleaningTarget = null;
            _StartTrashLevel = 0.0f;
        }

        public void SetCleaningTarget(ButtonOffice.Desk CleaningTarget)
        {
            _CleaningTarget = CleaningTarget;
        }

        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            ButtonOffice.Janitor Janitor = Person as ButtonOffice.Janitor;

            System.Diagnostics.Debug.Assert(_CleaningTarget != null);
            if((_CleaningTarget.GetJanitor() == null) && (_CleaningTarget.TrashLevel > 0.0f))
            {
                _CleaningTarget.SetJanitor(Janitor);
                _StartTrashLevel = _CleaningTarget.TrashLevel;
                Person.SetActionFraction(0.0f);
                Person.SetAnimationState(ButtonOffice.AnimationState.Cleaning);
                Person.SetAnimationFraction(0.0f);
            }
            else
            {
                Janitor.DequeueCleaningTarget();
                Finish(Game, Person);
            }
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            ButtonOffice.Janitor Janitor = Person as ButtonOffice.Janitor;

            if(_CleaningTarget.TrashLevel > 0.0f)
            {
                _CleaningTarget.TrashLevel -= ButtonOffice.Data.JanitorCleanAmount * ButtonOffice.Data.JanitorCleanSpeed * DeltaMinutes;
                if(_CleaningTarget.TrashLevel <= 0.0f)
                {
                    _CleaningTarget.TrashLevel = 0.0f;
                }
                Person.SetActionFraction(1.0f - _CleaningTarget.TrashLevel / _StartTrashLevel);
            }
            Person.SetAnimationFraction(Person.GetAnimationFraction() + ButtonOffice.Data.JanitorCleanSpeed * DeltaMinutes);
            if(((Person.GetAnimationFraction() > 1.0f) || (Person.GetAnimationFraction() == 0.0f)) && (_CleaningTarget.TrashLevel == 0.0f))
            {
                Janitor.DequeueCleaningTarget();
                Finish(Game, Person);
            }
            while(Person.GetAnimationFraction() > 1.0f)
            {
                Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            System.Diagnostics.Debug.Assert(_CleaningTarget != null);
            if(_CleaningTarget.GetJanitor() == Person)
            {
                _CleaningTarget.SetJanitor(null);
            }
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);

            Result.AppendChild(GameSaver.CreateProperty("cleaning-target", _CleaningTarget));
            Result.AppendChild(GameSaver.CreateProperty("start-trash-level", _StartTrashLevel));

            return Result;
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
            _CleaningTarget = GameLoader.LoadDeskProperty(Element, "cleaning-target");
            _StartTrashLevel = GameLoader.LoadSingleProperty(Element, "start-trash-level");
        }
    }

    internal class CleanDesks : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            ButtonOffice.Janitor Janitor = Person as ButtonOffice.Janitor;
            
            foreach(ButtonOffice.Office Office in Game.Offices)
            {
                Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                Janitor.EnqueueCleaningTarget(Office.FourthDesk);
            }
            Person.SetAtDesk(false);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    ButtonOffice.Janitor Janitor = Person as ButtonOffice.Janitor;
                    ButtonOffice.Desk CleaningTarget = Janitor.PeekCleaningTarget();

                    if(CleaningTarget != null)
                    {
                        ButtonOffice.Goals.WalkTo WalkTo = new ButtonOffice.Goals.WalkTo();

                        WalkTo.SetWalkTo(CleaningTarget.GetLocation());
                        AppendSubGoal(WalkTo);

                        ButtonOffice.Goals.CleanDesk CleanDesk = new ButtonOffice.Goals.CleanDesk();

                        CleanDesk.SetCleaningTarget(CleaningTarget);
                        AppendSubGoal(CleanDesk);
                    }
                    else
                    {
                        Finish(Game, Person);
                    }
                }
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            ButtonOffice.Janitor Janitor = Person as ButtonOffice.Janitor;

            Janitor.ClearCleaningTargets();
        }
    }

    internal class GoHome : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Game.SubtractCents(Person.GetWage());
            Game.FireSpendMoney(Person.GetWage(), Person.GetMidLocation());

            ButtonOffice.Goals.WalkTo WalkTo = new ButtonOffice.Goals.WalkTo();

            if(Person.GetLivingSide() == ButtonOffice.LivingSide.Left)
            {
                WalkTo.SetWalkTo(new System.Drawing.PointF(-10.0f, 0.0f));
            }
            else
            {
                WalkTo.SetWalkTo(new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f));
            }
            Person.SetAtDesk(false);
            AppendSubGoal(WalkTo);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Person.SetAnimationState(ButtonOffice.AnimationState.Hidden);
                Person.SetAnimationFraction(0.0f);
                Finish(Game, Person);
            }
        }
    }

    internal class GoToOwnDesk : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            ButtonOffice.Goals.WalkTo WalkTo = new ButtonOffice.Goals.WalkTo();

            WalkTo.SetWalkTo(new System.Drawing.PointF(Person.GetDesk().GetX() + (Person.GetDesk().GetWidth() - Person.GetWidth()) / 2.0f, Person.GetDesk().GetY()));
            AppendSubGoal(WalkTo);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Person.SetAtDesk(true);
                Finish(Game, Person);
            }
        }
    }

    internal class GoToWork : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
            if(Person.GetLivingSide() == ButtonOffice.LivingSide.Left)
            {
                Person.SetLocation(-10.0f, 0.0f);
            }
            else
            {
                Person.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
            }
            AppendSubGoal(new ButtonOffice.Goals.GoToOwnDesk());
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Person);
            }
        }
    }

    internal class ITTechThink : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new ButtonOffice.Goals.PlanNextWorkDay());
                AppendSubGoal(new ButtonOffice.Goals.WaitUntilTimeToArrive());
                AppendSubGoal(new ButtonOffice.Goals.GoToWork());
                AppendSubGoal(new ButtonOffice.Goals.StandByForRepairs());
                AppendSubGoal(new ButtonOffice.Goals.GoHome());
            }
        }
    }

    internal class JanitorThink : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new ButtonOffice.Goals.PlanNextWorkDay());
                AppendSubGoal(new ButtonOffice.Goals.WaitUntilTimeToArrive());
                AppendSubGoal(new ButtonOffice.Goals.GoToWork());
                AppendSubGoal(new ButtonOffice.Goals.CleanDesks());
                AppendSubGoal(new ButtonOffice.Goals.GoHome());
            }
        }
    }

    internal class PlanNextWorkDay : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            System.UInt64 ArrivesAtMinute = Game.GetFirstMinuteOfToday() + Person.GetArrivesAtMinuteOfDay();

            if(ArrivesAtMinute + Person.GetWorkMinutes() < Game.GetTotalMinutes())
            {
                ArrivesAtMinute += 1440;
            }
            Person.SetWorkDayMinutes(ArrivesAtMinute, ArrivesAtMinute + Person.GetWorkMinutes());
            Finish(Game, Person);
        }
    }

    internal class PushButtons : ButtonOffice.Goal
    {
        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetAnimationState(ButtonOffice.AnimationState.PushingButton);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
            else
            {
                if(Person.GetDesk().GetComputer().IsBroken() == false)
                {
                    Person.GetDesk().GetComputer().SetMinutesUntilBroken(Person.GetDesk().GetComputer().GetMinutesUntilBroken() - DeltaMinutes);
                    if(Person.GetDesk().GetComputer().IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0f);
                        Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
                        Person.SetAnimationFraction(0.0f);
                        if(Person.GetDesk() == Person.GetDesk().Office.FirstDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.FirstComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.SecondDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.SecondComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.ThirdDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.ThirdComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.FourthDesk)
                        {
                            Game.EnqueueBrokenThing(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Person.GetDesk().Office, ButtonOffice.BrokenThing.FourthComputer));
                        }
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + ButtonOffice.Data.WorkerWorkSpeed * DeltaMinutes);
                        while(Person.GetActionFraction() >= 1.0f)
                        {
                            System.UInt64 Revenue = 100L * Game.GetCurrentBonusPromille() / 1000L;

                            Person.SetActionFraction(Person.GetActionFraction() - 1.0f);
                            Person.GetDesk().TrashLevel += 1.0f;
                            Game.AddCents(Revenue);
                            Game.FireEarnMoney(Revenue, Person.GetMidLocation());
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + ButtonOffice.Data.WorkerWorkSpeed * DeltaMinutes);
                        while(Person.GetAnimationFraction() >= 1.0f)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
                        }
                    }
                }
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }
    }

    internal class Repair : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            ButtonOffice.ITTech ITTech = Person as ButtonOffice.ITTech;
            System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> RepairingTarget = ITTech.GetRepairingTarget();

            if((RepairingTarget.Second == ButtonOffice.BrokenThing.FirstComputer) || (RepairingTarget.Second == ButtonOffice.BrokenThing.SecondComputer) || (RepairingTarget.Second == ButtonOffice.BrokenThing.ThirdComputer) || (RepairingTarget.Second == ButtonOffice.BrokenThing.FourthComputer))
            {
                Person.SetActionFraction(Person.GetActionFraction() + ButtonOffice.Data.ITTechRepairComputerSpeed * DeltaMinutes);
            }
            else if((RepairingTarget.Second == ButtonOffice.BrokenThing.FirstLamp) || (RepairingTarget.Second == ButtonOffice.BrokenThing.SecondLamp) || (RepairingTarget.Second == ButtonOffice.BrokenThing.ThirdLamp))
            {
                Person.SetActionFraction(Person.GetActionFraction() + ButtonOffice.Data.ITTechRepairLampSpeed * DeltaMinutes);
            }
            if(Person.GetActionFraction() >= 1.0f)
            {
                Person.SetActionFraction(1.0f);
            }
            Person.SetAnimationFraction(Person.GetAnimationFraction() + ButtonOffice.Data.ITTechRepairSpeed * DeltaMinutes);
            if((Person.GetActionFraction() == 1.0f) && (Person.GetAnimationFraction() >= 1.0f))
            {
                switch(RepairingTarget.Second)
                {
                case ButtonOffice.BrokenThing.FirstComputer:
                    {
                        RepairingTarget.First.FirstDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case ButtonOffice.BrokenThing.SecondComputer:
                    {
                        RepairingTarget.First.SecondDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case ButtonOffice.BrokenThing.ThirdComputer:
                    {
                        RepairingTarget.First.ThirdDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case ButtonOffice.BrokenThing.FourthComputer:
                    {
                        RepairingTarget.First.FourthDesk.GetComputer().SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case ButtonOffice.BrokenThing.FirstLamp:
                    {
                        RepairingTarget.First.FirstLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                case ButtonOffice.BrokenThing.SecondLamp:
                    {
                        RepairingTarget.First.SecondLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                case ButtonOffice.BrokenThing.ThirdLamp:
                    {
                        RepairingTarget.First.ThirdLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                }
                ITTech.SetRepairingTarget(null);
                Person.GetDesk().TrashLevel += 1.0f;
                Finish(Game, Person);
            }
            while(Person.GetAnimationFraction() >= 1.0f)
            {
                Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
            }
        }
    }

    internal class StandByForRepairs : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = Game.DequeueBrokenThing();

                    if(BrokenThing != null)
                    {
                        ButtonOffice.ITTech ITTech = Person as ButtonOffice.ITTech;
                        ButtonOffice.Goals.WalkTo WalkTo = new ButtonOffice.Goals.WalkTo();

                        ITTech.SetRepairingTarget(BrokenThing);
                        switch(BrokenThing.Second)
                        {
                        case ButtonOffice.BrokenThing.FirstComputer:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.FirstDesk.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondComputer:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.SecondDesk.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdComputer:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.ThirdDesk.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FourthComputer:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.FourthDesk.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FirstLamp:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.FirstLamp.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondLamp:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.SecondLamp.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdLamp:
                            {
                                WalkTo.SetWalkTo(new System.Drawing.PointF(BrokenThing.First.ThirdLamp.GetX(), BrokenThing.First.GetY()));

                                break;
                            }
                        }
                        AppendSubGoal(WalkTo);
                        AppendSubGoal(new ButtonOffice.Goals.Repair());
                        AppendSubGoal(new ButtonOffice.Goals.GoToOwnDesk());
                        Person.SetAtDesk(false);
                    }
                }
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            ButtonOffice.ITTech ITTech = Person as ButtonOffice.ITTech;

            if(ITTech.GetRepairingTarget() != null)
            {
                Game.EnqueueBrokenThing(ITTech.GetRepairingTarget());
            }
        }
    }

    internal class Wait : ButtonOffice.Goal
    {
    }

    internal class WaitUntilTimeToArrive : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetArrivesAtMinute())
            {
                Finish(Game, Person);
            }
        }
    }

    internal class WaitUntilTimeToLeave : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
        }
    }

    internal class WalkTo : ButtonOffice.Goal
    {
        private System.Drawing.PointF _WalkTo;

        public void SetWalkTo(System.Drawing.PointF WalkTo)
        {
            _WalkTo = WalkTo;
        }

        protected override void _OnInitialize(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(ButtonOffice.AnimationState.Walking);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            System.Single DeltaX = _WalkTo.X - Person.GetX();
            System.Single DeltaY = _WalkTo.Y - Person.GetY();
            System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

            if(Norm > 0.1)
            {
                DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * DeltaMinutes;
                DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * DeltaMinutes;
                Person.SetLocation(Person.GetX() + DeltaX, Person.GetY() + DeltaY);
            }
            else
            {
                Person.SetLocation(_WalkTo);
                Finish(Game, Person);
            }
        }

        protected override void _OnTerminate(ButtonOffice.Game Game, ButtonOffice.Person Person)
        {
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(ButtonOffice.AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);

            Result.AppendChild(GameSaver.CreateProperty("walk-to", _WalkTo));

            return Result;
        }

        public override void Load(ButtonOffice.GameLoader GameLoader, System.Xml.XmlElement Element)
        {
            base.Load(GameLoader, Element);
            _WalkTo = GameLoader.LoadPointProperty(Element, "walk-to");
        }
    }

    internal class WorkerThink : ButtonOffice.Goal
    {
        protected override void _OnExecute(ButtonOffice.Game Game, ButtonOffice.Person Person, System.Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new ButtonOffice.Goals.PlanNextWorkDay());
                AppendSubGoal(new ButtonOffice.Goals.WaitUntilTimeToArrive());
                AppendSubGoal(new ButtonOffice.Goals.GoToWork());
                AppendSubGoal(new ButtonOffice.Goals.PushButtons());
                AppendSubGoal(new ButtonOffice.Goals.GoHome());
            }
        }
    }
}

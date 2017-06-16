﻿using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice.Goals
{
    internal class AccountantThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new Accounting());
                AppendSubGoal(new GoHome());
            }
        }
    }

    internal class Accounting : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Accounting);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(Person.GetDesk().GetComputer().IsBroken() == false)
                {
                    Person.GetDesk().GetComputer().SetMinutesUntilBroken(Person.GetDesk().GetComputer().GetMinutesUntilBroken() - DeltaMinutes);
                    if(Person.GetDesk().GetComputer().IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0f);
                        Person.SetAnimationState(AnimationState.Standing);
                        Person.SetAnimationFraction(0.0f);
                        if(Person.GetDesk() == Person.GetDesk().Office.FirstDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.FirstComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.SecondDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.SecondComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.ThirdDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.ThirdComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.FourthDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.FourthComputer));
                        }
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + Data.AccountantWorkSpeed * DeltaMinutes);
                        while(Person.GetActionFraction() >= 1.0f)
                        {
                            Person.SetActionFraction(Person.GetActionFraction() - 1.0f);
                            Person.GetDesk().TrashLevel += 2.0f;
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.AccountantWorkSpeed * DeltaMinutes);
                        while(Person.GetAnimationFraction() >= 1.0f)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
                        }
                    }
                }
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }
    }

    internal class CatThink : Goal
    {
        private ActionState _ActionState;
        private Single _MinutesToActionStateChange;

        public CatThink()
        {
            _ActionState = ActionState.Stay;
            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(10.0f, 15.0f);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Cat = Actor as Cat;

            Debug.Assert(Cat != null);
            switch(_ActionState)
            {
            case ActionState.Stay:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(RandomNumberGenerator.GetBoolean() == true)
                        {
                            _ActionState = ActionState.WalkLeft;
                        }
                        else
                        {
                            _ActionState = ActionState.WalkRight;
                        }
                        _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(20.0f, 20.0f);
                    }
                    _MinutesToActionStateChange -= DeltaMinutes;

                    break;
                }
            case ActionState.WalkLeft:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.8) == true)
                        {
                            _ActionState = ActionState.Stay;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ActionState.WalkRight;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(10.0f, 8.0f);
                        }
                    }
                    Cat.SetX(Cat.GetX() - DeltaMinutes * Data.CatWalkSpeed);
                    if(Cat.GetX() <= Cat.Office.GetX())
                    {
                        _ActionState = ActionState.WalkRight;
                    }
                    _MinutesToActionStateChange -= DeltaMinutes;

                    break;
                }
            case ActionState.WalkRight:
                {

                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.8) == true)
                        {
                            _ActionState = ActionState.Stay;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ActionState.WalkLeft;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(10.0f, 8.0f);
                        }
                    }
                    Cat.SetX(Cat.GetX() + DeltaMinutes * Data.CatWalkSpeed);
                    if(Cat.GetRight() >= Cat.Office.GetRight())
                    {
                        _ActionState = ActionState.WalkLeft;
                    }
                    _MinutesToActionStateChange -= DeltaMinutes;

                    break;
                }
            }
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _ActionState = ObjectStore.LoadActionStateProperty("action-state");
            _MinutesToActionStateChange = ObjectStore.LoadSingleProperty("minutes-to-action-state-change");
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("action-state", _ActionState);
            ObjectStore.Save("minutes-to-action-state-change", _MinutesToActionStateChange);
        }
    }

    internal class CleanDesk : Goal
    {
        private Desk _CleaningTarget;
        private Single _StartTrashLevel;

        public CleanDesk()
        {
            _CleaningTarget = null;
            _StartTrashLevel = 0.0f;
        }

        public void SetCleaningTarget(Desk CleaningTarget)
        {
            _CleaningTarget = CleaningTarget;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            Debug.Assert(_CleaningTarget != null);
            if((_CleaningTarget.GetJanitor() == null) && (_CleaningTarget.TrashLevel > 0.0f))
            {
                _CleaningTarget.SetJanitor(Janitor);
                _StartTrashLevel = _CleaningTarget.TrashLevel;
                Janitor.SetActionFraction(0.0f);
                Janitor.SetAnimationState(AnimationState.Cleaning);
                Janitor.SetAnimationFraction(0.0f);
            }
            else
            {
                Janitor.DequeueCleaningTarget();
                Abort(Game, Actor);
            }
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            if(_CleaningTarget.TrashLevel > 0.0f)
            {
                _CleaningTarget.TrashLevel -= Data.JanitorCleanAmount * Data.JanitorCleanSpeed * DeltaMinutes;
                if(_CleaningTarget.TrashLevel <= 0.0f)
                {
                    _CleaningTarget.TrashLevel = 0.0f;
                }
                Janitor.SetActionFraction(1.0f - _CleaningTarget.TrashLevel / _StartTrashLevel);
            }
            Janitor.SetAnimationFraction(Janitor.GetAnimationFraction() + Data.JanitorCleanSpeed * DeltaMinutes);
            if(((Janitor.GetAnimationFraction() > 1.0f) || (Janitor.GetAnimationFraction() == 0.0f)) && (_CleaningTarget.TrashLevel == 0.0f))
            {
                Janitor.DequeueCleaningTarget();
                Finish(Game, Actor);
            }
            while(Janitor.GetAnimationFraction() > 1.0f)
            {
                Janitor.SetAnimationFraction(Janitor.GetAnimationFraction() - 1.0f);
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            Debug.Assert(_CleaningTarget != null);
            if(_CleaningTarget.GetJanitor() == Janitor)
            {
                _CleaningTarget.SetJanitor(null);
            }
            Janitor.SetActionFraction(0.0f);
            Janitor.SetAnimationState(AnimationState.Standing);
            Janitor.SetAnimationFraction(0.0f);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("cleaning-target", _CleaningTarget);
            ObjectStore.Save("start-trash-level", _StartTrashLevel);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _CleaningTarget = ObjectStore.LoadDeskProperty("cleaning-target");
            _StartTrashLevel = ObjectStore.LoadSingleProperty("start-trash-level");
        }
    }

    internal class CleanDesks : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            foreach(var Office in Game.Offices.GetShuffledCopy())
            {
                Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                Janitor.EnqueueCleaningTarget(Office.FourthDesk);
            }
            Janitor.SetAtDesk(false);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            if(Game.GetTotalMinutes() > Janitor.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    var CleaningTarget = Janitor.PeekCleaningTarget();

                    if(CleaningTarget != null)
                    {
                        var WalkToLocation = new WalkToLocation();

                        WalkToLocation.SetLocation(new PointF(CleaningTarget.GetX() + CleaningTarget.GetWidth() / 2.0f, CleaningTarget.GetY()));
                        AppendSubGoal(WalkToLocation);

                        var CleanDesk = new CleanDesk();

                        CleanDesk.SetCleaningTarget(CleaningTarget);
                        AppendSubGoal(CleanDesk);
                    }
                    else
                    {
                        Finish(Game, Actor);
                    }
                }
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            Janitor.ClearCleaningTargets();
        }
    }

    internal class GoHome : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Game.SpendMoney(Person.GetWage(), Person.GetMidLocation());

            var WalkToLocation = new WalkToLocation();

            if(Person.GetLivingSide() == LivingSide.Left)
            {
                WalkToLocation.SetLocation(new PointF(-10.0f, 0.0f));
            }
            else
            {
                WalkToLocation.SetLocation(new PointF(Game.WorldBlockWidth + 10.0f, 0.0f));
            }
            Person.SetAtDesk(false);
            AppendSubGoal(WalkToLocation);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;

                Debug.Assert(Person != null);
                Person.SetAnimationState(AnimationState.Hidden);
                Person.SetAnimationFraction(0.0f);
                Finish(Game, Actor);
            }
        }
    }

    internal class GoToOwnDesk : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var Desk = Person.GetDesk();

            Debug.Assert(Desk != null);

            var WalkToDesk = new WalkToDesk();

            WalkToDesk.SetDesk(Desk);
            AppendSubGoal(WalkToDesk);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;

                Debug.Assert(Person != null);
                Person.SetAtDesk(true);
                Finish(Game, Actor);
            }
        }
    }

    internal class GoToWork : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
            if(Person.GetLivingSide() == LivingSide.Left)
            {
                Person.SetLocation(-10.0f, 0.0f);
            }
            else
            {
                Person.SetLocation(Game.WorldBlockWidth + 10.0f, 0.0f);
            }
            AppendSubGoal(new GoToOwnDesk());
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
        }
    }

    internal class ITTechThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new StandByForRepairs());
                AppendSubGoal(new GoHome());
            }
        }
    }

    internal class JanitorThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new CleanDesks());
                AppendSubGoal(new GoHome());
            }
        }
    }

    internal class PlanNextWorkDay : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var ArrivesAtMinute = Game.GetFirstMinuteOfToday() + Person.GetArrivesAtMinuteOfDay();

            if(ArrivesAtMinute + Person.GetWorkMinutes() < Game.GetTotalMinutes())
            {
                ArrivesAtMinute += 1440;
            }
            Person.SetWorkDayMinutes(ArrivesAtMinute, ArrivesAtMinute + Person.GetWorkMinutes());
            Finish(Game, Actor);
        }
    }

    internal class PushButtons : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.PushingButton);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(Person.GetDesk().GetComputer().IsBroken() == false)
                {
                    Person.GetDesk().GetComputer().SetMinutesUntilBroken(Person.GetDesk().GetComputer().GetMinutesUntilBroken() - DeltaMinutes);
                    if(Person.GetDesk().GetComputer().IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0f);
                        Person.SetAnimationState(AnimationState.Standing);
                        Person.SetAnimationFraction(0.0f);
                        if(Person.GetDesk() == Person.GetDesk().Office.FirstDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.FirstComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.SecondDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.SecondComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.ThirdDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.ThirdComputer));
                        }
                        else if(Person.GetDesk() == Person.GetDesk().Office.FourthDesk)
                        {
                            Game.EnqueueBrokenThing(new Pair<Office, BrokenThing>(Person.GetDesk().Office, BrokenThing.FourthComputer));
                        }
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + Data.WorkerWorkSpeed * DeltaMinutes);
                        while(Person.GetActionFraction() >= 1.0f)
                        {
                            var Revenue = 100L * Game.GetCurrentBonusPromille() / 1000L;

                            Person.SetActionFraction(Person.GetActionFraction() - 1.0f);
                            Person.GetDesk().TrashLevel += 1.0f;
                            Game.EarnMoney(Revenue, Person.GetMidLocation());
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.WorkerWorkSpeed * DeltaMinutes);
                        while(Person.GetAnimationFraction() >= 1.0f)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0f);
                        }
                    }
                }
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }
    }

    internal class Repair : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var ITTech = Actor as ITTech;

            Debug.Assert(ITTech != null);

            var RepairingTarget = ITTech.GetRepairingTarget();

            if((RepairingTarget.Second == BrokenThing.FirstComputer) || (RepairingTarget.Second == BrokenThing.SecondComputer) || (RepairingTarget.Second == BrokenThing.ThirdComputer) || (RepairingTarget.Second == BrokenThing.FourthComputer))
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairComputerSpeed * DeltaMinutes);
            }
            else if((RepairingTarget.Second == BrokenThing.FirstLamp) || (RepairingTarget.Second == BrokenThing.SecondLamp) || (RepairingTarget.Second == BrokenThing.ThirdLamp))
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairLampSpeed * DeltaMinutes);
            }
            if(ITTech.GetActionFraction() >= 1.0f)
            {
                ITTech.SetActionFraction(1.0f);
            }
            ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() + Data.ITTechRepairSpeed * DeltaMinutes);
            if((ITTech.GetActionFraction() == 1.0f) && (ITTech.GetAnimationFraction() >= 1.0f))
            {
                switch(RepairingTarget.Second)
                {
                case BrokenThing.FirstComputer:
                    {
                        RepairingTarget.First.FirstDesk.GetComputer().SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case BrokenThing.SecondComputer:
                    {
                        RepairingTarget.First.SecondDesk.GetComputer().SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case BrokenThing.ThirdComputer:
                    {
                        RepairingTarget.First.ThirdDesk.GetComputer().SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case BrokenThing.FourthComputer:
                    {
                        RepairingTarget.First.FourthDesk.GetComputer().SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenComputer));

                        break;
                    }
                case BrokenThing.FirstLamp:
                    {
                        RepairingTarget.First.FirstLamp.SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                case BrokenThing.SecondLamp:
                    {
                        RepairingTarget.First.SecondLamp.SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                case BrokenThing.ThirdLamp:
                    {
                        RepairingTarget.First.ThirdLamp.SetMinutesUntilBroken(RandomNumberGenerator.GetSingleFromExponentialDistribution(Data.MeanMinutesToBrokenLamp));

                        break;
                    }
                }
                ITTech.SetRepairingTarget(null);
                ITTech.GetDesk().TrashLevel += 1.0f;
                Finish(Game, Actor);
            }
            while(ITTech.GetAnimationFraction() >= 1.0f)
            {
                ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() - 1.0f);
            }
        }
    }

    internal class StandByForRepairs : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var ITTech = Actor as ITTech;

            Debug.Assert(ITTech != null);
            if(Game.GetTotalMinutes() > ITTech.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(HasSubGoals() == false)
                {
                    var BrokenThing = Game.DequeueBrokenThing();

                    if(BrokenThing != null)
                    {
                        ITTech.SetRepairingTarget(BrokenThing);
                        switch(BrokenThing.Second)
                        {
                        case ButtonOffice.BrokenThing.FirstComputer:
                            {
                                var WalkToDesk = new WalkToDesk();

                                WalkToDesk.SetDesk(BrokenThing.First.FirstDesk);
                                AppendSubGoal(WalkToDesk);

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondComputer:
                            {
                                var WalkToDesk = new WalkToDesk();

                                WalkToDesk.SetDesk(BrokenThing.First.SecondDesk);
                                AppendSubGoal(WalkToDesk);

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdComputer:
                            {
                                var WalkToDesk = new WalkToDesk();

                                WalkToDesk.SetDesk(BrokenThing.First.ThirdDesk);
                                AppendSubGoal(WalkToDesk);

                                break;
                            }
                        case ButtonOffice.BrokenThing.FourthComputer:
                            {
                                var WalkToDesk = new WalkToDesk();

                                WalkToDesk.SetDesk(BrokenThing.First.FourthDesk);
                                AppendSubGoal(WalkToDesk);

                                break;
                            }
                        case ButtonOffice.BrokenThing.FirstLamp:
                            {
                                var WalkToLocation = new WalkToLocation();

                                WalkToLocation.SetLocation(new PointF(BrokenThing.First.FirstLamp.GetX() + BrokenThing.First.FirstLamp.GetWidth() / 2.0f, BrokenThing.First.GetY()));
                                AppendSubGoal(WalkToLocation);

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondLamp:
                            {
                                var WalkToLocation = new WalkToLocation();

                                WalkToLocation.SetLocation(new PointF(BrokenThing.First.SecondLamp.GetX() + BrokenThing.First.SecondLamp.GetWidth() / 2.0f, BrokenThing.First.GetY()));
                                AppendSubGoal(WalkToLocation);

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdLamp:
                            {
                                var WalkToLocation = new WalkToLocation();

                                WalkToLocation.SetLocation(new PointF(BrokenThing.First.ThirdLamp.GetX() + BrokenThing.First.ThirdLamp.GetWidth() / 2.0f, BrokenThing.First.GetY()));
                                AppendSubGoal(WalkToLocation);

                                break;
                            }
                        }
                        AppendSubGoal(new Repair());
                        AppendSubGoal(new GoToOwnDesk());
                        ITTech.SetAtDesk(false);
                    }
                }
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var ITTech = Actor as ITTech;

            Debug.Assert(ITTech != null);
            if(ITTech.GetRepairingTarget() != null)
            {
                Game.EnqueueBrokenThing(ITTech.GetRepairingTarget());
            }
        }
    }

    internal class Wait : Goal
    {
    }

    internal class WaitUntilTimeToArrive : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetArrivesAtMinute())
            {
                Finish(Game, Actor);
            }
        }
    }

    internal class WaitUntilTimeToLeave : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
        }
    }

    internal class WalkOnSameFloor : Goal
    {
        private Single _X;

        public void SetX(Single X)
        {
            _X = X;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Walking);
            Person.SetAnimationFraction(0.0f);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var DeltaX = _X - Person.GetX();

            if(Math.Abs(DeltaX) > 0.1)
            {
                if(DeltaX > 0.0f)
                {
                    DeltaX = Data.PersonSpeed * DeltaMinutes;
                }
                else
                {
                    DeltaX = -Data.PersonSpeed * DeltaMinutes;
                }
                Person.SetLocation(Person.GetX() + DeltaX, Person.GetY());
            }
            else
            {
                Person.SetLocation(_X, Person.GetY());
                Finish(Game, Person);
            }
        }

        protected override void _OnTerminate(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            Person.SetActionFraction(0.0f);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0f);
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("x", _X);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _X = ObjectStore.LoadSingleProperty("x");
        }
    }

    internal class WalkToLocation : Goal
    {
        private PointF _Location;

        public WalkToLocation()
        {
        }

        public void SetLocation(PointF Location)
        {
            _Location = Location;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);

            var TransportationPath = Game.GetTransportationPath(new PointF(Person.GetX() + Person.GetWidth() / 2.0f, Person.GetY()), _Location);

            if(TransportationPath != null)
            {
                foreach(var TransportationNode in TransportationPath)
                {
                    var WalkOnSameFloor = new WalkOnSameFloor();

                    WalkOnSameFloor.SetX(TransportationNode.GetX() - Person.GetWidth() / 2.0f);
                    AppendSubGoal(WalkOnSameFloor);

                    var CreateUseGoalFunction = TransportationNode.GetCreateUseGoalFunction();

                    if(CreateUseGoalFunction != null)
                    {
                        var UseGoal = CreateUseGoalFunction();

                        AppendSubGoal(UseGoal);
                    }
                }
            }
            else
            {
                Abort(Game, Person);
            }
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("location", _Location);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Location = ObjectStore.LoadPointProperty("location");
        }
    }

    internal class WalkToDesk : Goal
    {
        private Desk _Desk;

        public WalkToDesk()
        {
            _Desk = null;
        }

        public void SetDesk(Desk Desk)
        {
            _Desk = Desk;
        }

        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            Debug.Assert(_Desk != null);

            var WalkToLocation = new WalkToLocation();

            WalkToLocation.SetLocation(new PointF(_Desk.GetX() + _Desk.GetWidth() / 2.0f, _Desk.GetY()));
            AppendSubGoal(WalkToLocation);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("desk", _Desk);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _Desk = ObjectStore.LoadDeskProperty("desk");
        }
    }

    internal class WorkerThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Single DeltaMinutes)
        {
            if(HasSubGoals() == false)
            {
                AppendSubGoal(new PlanNextWorkDay());
                AppendSubGoal(new WaitUntilTimeToArrive());
                AppendSubGoal(new GoToWork());
                AppendSubGoal(new PushButtons());
                AppendSubGoal(new GoHome());
            }
        }
    }
}

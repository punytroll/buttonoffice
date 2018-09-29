using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace ButtonOffice.Goals
{
    internal class AccountantThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
            Person.SetAnimationFraction(0.0);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(Person.Desk.Computer.IsBroken() == false)
                {
                    Person.Desk.Computer.Use(DeltaGameMinutes);
                    if(Person.Desk.Computer.IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0);
                        Person.SetAnimationState(AnimationState.Standing);
                        Person.SetAnimationFraction(0.0);
                        Game.EnqueueBrokenThing(Person.Desk.Computer);
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + Data.AccountantWorkSpeed * DeltaGameMinutes);
                        while(Person.GetActionFraction() >= 1.0)
                        {
                            Person.SetActionFraction(Person.GetActionFraction() - 1.0);
                            Person.Desk.TrashLevel += 2.0;
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.AccountantWorkSpeed * DeltaGameMinutes);
                        while(Person.GetAnimationFraction() >= 1.0)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0);
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
            Person.SetAnimationFraction(0.0);
        }
    }

    internal class CatThink : Goal
    {
        private ActionState _ActionState;
        private Double _MinutesToActionStateChange;

        public CatThink()
        {
            _ActionState = ActionState.Stay;
            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(10.0, 15.0);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Cat = Actor as Cat;

            Debug.Assert(Cat != null);
            switch(_ActionState)
            {
            case ActionState.Stay:
                {
                    if(_MinutesToActionStateChange < 0.0)
                    {
                        if(RandomNumberGenerator.GetBoolean() == true)
                        {
                            _ActionState = ActionState.WalkLeft;
                        }
                        else
                        {
                            _ActionState = ActionState.WalkRight;
                        }
                        _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(20.0, 20.0);
                    }
                    _MinutesToActionStateChange -= DeltaGameMinutes;

                    break;
                }
            case ActionState.WalkLeft:
                {
                    if(_MinutesToActionStateChange < 0.0)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.8) == true)
                        {
                            _ActionState = ActionState.Stay;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(30.0, 30.0);
                        }
                        else
                        {
                            _ActionState = ActionState.WalkRight;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(10.0, 8.0);
                        }
                    }
                    Cat.SetX(Cat.GetX() - DeltaGameMinutes * Data.CatWalkSpeed);
                    if(Cat.GetLeft() <= Cat.Office.GetX())
                    {
                        _ActionState = ActionState.WalkRight;
                    }
                    _MinutesToActionStateChange -= DeltaGameMinutes;

                    break;
                }
            case ActionState.WalkRight:
                {

                    if(_MinutesToActionStateChange < 0.0)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.8) == true)
                        {
                            _ActionState = ActionState.Stay;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(30.0, 30.0);
                        }
                        else
                        {
                            _ActionState = ActionState.WalkLeft;
                            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(10.0, 8.0);
                        }
                    }
                    Cat.SetX(Cat.GetX() + DeltaGameMinutes * Data.CatWalkSpeed);
                    if(Cat.GetRight() >= Cat.Office.GetRight())
                    {
                        _ActionState = ActionState.WalkLeft;
                    }
                    _MinutesToActionStateChange -= DeltaGameMinutes;

                    break;
                }
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("action-state", _ActionState);
            ObjectStore.Save("minutes-to-action-state-change", _MinutesToActionStateChange);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _ActionState = ObjectStore.LoadActionStateProperty("action-state");
            _MinutesToActionStateChange = ObjectStore.LoadDoubleProperty("minutes-to-action-state-change");
        }
    }

    internal class CleanDesk : Goal
    {
        private Desk _CleaningTarget;
        private Double _StartTrashLevel;

        public CleanDesk()
        {
            _CleaningTarget = null;
            _StartTrashLevel = 0.0;
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
            if((_CleaningTarget.GetJanitor() == null) && (_CleaningTarget.TrashLevel > 0.0))
            {
                _CleaningTarget.SetJanitor(Janitor);
                _StartTrashLevel = _CleaningTarget.TrashLevel;
                Janitor.SetActionFraction(0.0);
                Janitor.SetAnimationState(AnimationState.Cleaning);
                Janitor.SetAnimationFraction(0.0);
            }
            else
            {
                Janitor.DequeueCleaningTarget();
                Abort(Game, Actor);
            }
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            if(_CleaningTarget.TrashLevel > 0.0)
            {
                _CleaningTarget.TrashLevel -= Data.JanitorCleanAmount * Data.JanitorCleanSpeed * DeltaGameMinutes;
                if(_CleaningTarget.TrashLevel <= 0.0)
                {
                    _CleaningTarget.TrashLevel = 0.0;
                }
                Janitor.SetActionFraction(1.0 - _CleaningTarget.TrashLevel / _StartTrashLevel);
            }
            Janitor.SetAnimationFraction(Janitor.GetAnimationFraction() + Data.JanitorCleanSpeed * DeltaGameMinutes);
            if(((Janitor.GetAnimationFraction() > 1.0) || (Janitor.GetAnimationFraction() == 0.0)) && (_CleaningTarget.TrashLevel == 0.0))
            {
                Janitor.DequeueCleaningTarget();
                Finish(Game, Actor);
            }
            while(Janitor.GetAnimationFraction() > 1.0)
            {
                Janitor.SetAnimationFraction(Janitor.GetAnimationFraction() - 1.0);
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
            Janitor.SetActionFraction(0.0);
            Janitor.SetAnimationState(AnimationState.Standing);
            Janitor.SetAnimationFraction(0.0);
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
            _StartTrashLevel = ObjectStore.LoadDoubleProperty("start-trash-level");
        }
    }

    internal class CleanDesks : Goal
    {
        protected override void _OnInitialize(Game Game, PersistentObject Actor)
        {
            var Janitor = Actor as Janitor;

            Debug.Assert(Janitor != null);
            foreach(var Office in Game.Offices.OrderBy(delegate(Office Office)
                                                       {
                                                           var Result = Office.GetY() - Janitor.Desk.GetY();

                                                           if(Result < 0.0)
                                                           {
                                                               Result = (Game.HighestFloor - Game.LowestFloor) - Result;
                                                           }

                                                           return Result;
                                                       }).ThenBy((Office) => Office.GetX()))
            {
                Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                Janitor.EnqueueCleaningTarget(Office.FourthDesk);
            }
            Janitor.SetAtDesk(false);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
                        var WalkToLocation = new TravelToLocation();

                        WalkToLocation.SetLocation(new Vector2(CleaningTarget.GetX() + CleaningTarget.GetWidth() / 2.0, CleaningTarget.GetY()));
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

            var WalkToLocation = new TravelToLocation();

            if(Person.GetLivingSide() == LivingSide.Left)
            {
                WalkToLocation.SetLocation(new Vector2(Game.LeftBorder - 10.0, 0.0));
            }
            else
            {
                WalkToLocation.SetLocation(new Vector2(Game.RightBorder + 10.0, 0.0));
            }
            Person.SetAtDesk(false);
            AppendSubGoal(WalkToLocation);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                var Person = Actor as Person;

                Debug.Assert(Person != null);
                Person.SetAnimationState(AnimationState.Hidden);
                Person.SetAnimationFraction(0.0);
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

            var Desk = Person.Desk;

            Debug.Assert(Desk != null);

            var WalkToDesk = new WalkToDesk();

            WalkToDesk.SetDesk(Desk);
            AppendSubGoal(WalkToDesk);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
            Person.SetActionFraction(0.0);
            Person.SetAnimationState(AnimationState.Standing);
            Person.SetAnimationFraction(0.0);
            if(Person.GetLivingSide() == LivingSide.Left)
            {
                Person.SetX(Game.LeftBorder - 10.0);
            }
            else
            {
                Person.SetX(Game.RightBorder + 10.0);
            }
            Person.SetY(0.0);
            AppendSubGoal(new GoToOwnDesk());
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                Finish(Game, Actor);
            }
        }
    }

    internal class ITTechThink : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
            Person.SetAnimationFraction(0.0);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Actor);
            }
            else
            {
                if(Person.Desk.Computer.IsBroken() == false)
                {
                    Person.Desk.Computer.Use(DeltaGameMinutes);
                    if(Person.Desk.Computer.IsBroken() == true)
                    {
                        Person.SetActionFraction(0.0);
                        Person.SetAnimationState(AnimationState.Standing);
                        Person.SetAnimationFraction(0.0);
                        Game.EnqueueBrokenThing(Person.Desk.Computer);
                    }
                    else
                    {
                        Person.SetActionFraction(Person.GetActionFraction() + Data.WorkerWorkSpeed * DeltaGameMinutes);
                        while(Person.GetActionFraction() >= 1.0)
                        {
                            var Revenue = 100L * Game.GetCurrentBonusPromille() / 1000L;

                            Person.SetActionFraction(Person.GetActionFraction() - 1.0);
                            Person.Desk.TrashLevel += 1.0;
                            Game.EarnMoney(Revenue, Person.GetMidLocation());
                        }
                        Person.SetAnimationFraction(Person.GetAnimationFraction() + Data.WorkerWorkSpeed * DeltaGameMinutes);
                        while(Person.GetAnimationFraction() >= 1.0)
                        {
                            Person.SetAnimationFraction(Person.GetAnimationFraction() - 1.0);
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
            Person.SetAnimationFraction(0.0);
        }
    }

    internal class Repair : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var ITTech = Actor as ITTech;

            Debug.Assert(ITTech != null);

            var RepairingTarget = ITTech.GetRepairingTarget();

            if(RepairingTarget is Computer)
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairComputerSpeed * DeltaGameMinutes);
            }
            else if(RepairingTarget is Lamp)
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairLampSpeed * DeltaGameMinutes);
            }
            if(ITTech.GetActionFraction() >= 1.0)
            {
                ITTech.SetActionFraction(1.0);
            }
            ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() + Data.ITTechRepairSpeed * DeltaGameMinutes);
            if((ITTech.GetActionFraction() == 1.0) && (ITTech.GetAnimationFraction() >= 1.0))
            {
                if(RepairingTarget is Computer)
                {
                    ((Computer)RepairingTarget).SetRepaired();
                }
                else if(RepairingTarget is Lamp)
                {
                    ((Lamp)RepairingTarget).SetRepaired();
                }
                ITTech.SetRepairingTarget(null);
                ITTech.Desk.TrashLevel += 1.0;
                Finish(Game, Actor);
            }
            while(ITTech.GetAnimationFraction() >= 1.0)
            {
                ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() - 1.0);
            }
        }
    }

    internal class StandByForRepairs : Goal
    {
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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

                        var FindPathToLocation = new TravelToLocation();

                        if(BrokenThing is Computer)
                        {
                            var Computer = (Computer)BrokenThing;

                            FindPathToLocation.SetLocation(new Vector2(Computer.GetX() + Computer.GetWidth() / 2.0, Computer.GetY().GetFloored()));
                        }
                        else if(BrokenThing is Lamp)
                        {
                            var Lamp = (Lamp)BrokenThing;

                            FindPathToLocation.SetLocation(new Vector2(Lamp.GetX() + Lamp.GetWidth() / 2.0, Lamp.GetY().GetFloored()));
                        }
                        AppendSubGoal(FindPathToLocation);
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
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
        {
            var Person = Actor as Person;

            Debug.Assert(Person != null);
            if(Game.GetTotalMinutes() > Person.GetLeavesAtMinute())
            {
                Finish(Game, Person);
            }
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

            var WalkToLocation = new TravelToLocation();

            WalkToLocation.SetLocation(new Vector2(_Desk.GetX() + _Desk.GetWidth() / 2.0, _Desk.GetY()));
            AppendSubGoal(WalkToLocation);
        }

        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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
        protected override void _OnExecute(Game Game, PersistentObject Actor, Double DeltaGameMinutes)
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

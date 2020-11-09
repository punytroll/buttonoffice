using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
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
        
        protected override void _OnInitialize(Game Game, Actor Actor)
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
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
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
        
        protected override void _OnTerminate(Game Game, Actor Actor)
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
}

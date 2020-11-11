using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
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
        
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Debug.Assert(_Desk != null);
            
            var WalkToLocation = new TravelToLocation();
            
            WalkToLocation.SetLocation(new Vector2(_Desk.GetX() + _Desk.GetWidth() / 2.0, _Desk.GetY()));
            AppendSubGoal(WalkToLocation);
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            if(HasSubGoals() == false)
            {
                Succeed();
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
}

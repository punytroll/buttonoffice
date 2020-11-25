using ButtonOffice.AI.Goals;
using System;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class Repeat : Goal
    {
        public UInt32? Count
        {
            get;
            set;
        }
        
        public Goal Behavior
        {
            get;
            set;
        }
        
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("Repeat.Initialize");
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("Repeat.Execute");
            
            var ExecuteResult = _Execute(Behavior, Game, Actor, DeltaGameMinutes);
            
            if(ExecuteResult == GoalState.Failed)
            {
                Fail();
            }
            else if(Count != null)
            {
                Count -= 1;
                if(Count == 0)
                {
                    Succeed();
                }
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("Repeat.Terminate");
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("has-count", Count.HasValue);
            ObjectStore.Save("count", (Count.HasValue == true) ? (Count.Value) : (0));
            ObjectStore.Save("behavior", Behavior);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            
            var HasCount = ObjectStore.LoadBooleanProperty("has-count");
            
            if(HasCount == true)
            {
                Count = ObjectStore.LoadUInt32Property("count");
            }
            Behavior = ObjectStore.LoadObjectProperty<Goal>("behavior");
        }
    }
}

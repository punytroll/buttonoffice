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
        
        public Repeat()
        {
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
    }
}

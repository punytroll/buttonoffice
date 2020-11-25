using ButtonOffice.AI.Goals;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class Sequence : Goal
    {
        public List<Goal> Behaviors
        {
            get;
            set;
        }
        
        private Int32 _BehaviorIndex;
        
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("Sequence.Initialize");
            _BehaviorIndex = 0;
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine($"Sequence.Execute (Count={Behaviors.Count}, Index={_BehaviorIndex})");
            if(_BehaviorIndex == Behaviors.Count)
            {
                Succeed();
            }
            else
            {
                var CurrentBehaviorState = _Execute(Behaviors[_BehaviorIndex], Game, Actor, DeltaGameMinutes);
                
                switch(CurrentBehaviorState)
                {
                case GoalState.Failed:
                    {
                        Fail();
                        
                        break;
                    }
                case GoalState.Succeeded:
                    {
                        _BehaviorIndex += 1;
                        if(_BehaviorIndex == Behaviors.Count)
                        {
                            Succeed();
                        }
                        
                        break;
                    }
                }
            }
        }
        
        protected override void _OnTerminate(Game Game, Actor Actor)
        {
            Console.WriteLine("Sequence.Terminate");
        }
        
        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("behaviors", Behaviors);
            ObjectStore.Save("behavior-index", _BehaviorIndex);
        }
        
        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            Behaviors = ObjectStore.LoadListProperty<Goal>("behaviors");
            _BehaviorIndex = ObjectStore.LoadInt32Property("behavior-index");
        }
    }
}

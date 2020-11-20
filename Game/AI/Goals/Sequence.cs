using ButtonOffice.AI.Goals;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal class Sequence : Goal, IEnumerable<Goal>
    {
        public List<Goal> Behaviors
        {
            get;
            set;
        }
        
        private IEnumerator<Goal> _CurrentBehavior;
        
        public Sequence()
        {
        }
        
        protected override void _OnInitialize(Game Game, Actor Actor)
        {
            Console.WriteLine("Sequence.Initialize");
            _CurrentBehavior = Behaviors.GetEnumerator();
            _CurrentBehavior.MoveNext();
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            Console.WriteLine("Sequence.Execute");
            if(_CurrentBehavior.Current == null)
            {
                Succeed();
            }
            else
            {
                var CurrentBehaviorState = _Execute(_CurrentBehavior.Current, Game, Actor, DeltaGameMinutes);
                
                switch(CurrentBehaviorState)
                {
                case GoalState.Failed:
                    {
                        Fail();
                        
                        break;
                    }
                case GoalState.Succeeded:
                    {
                        _CurrentBehavior.MoveNext();
                        if(_CurrentBehavior.Current == null)
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
        
        IEnumerator<Goal> IEnumerable<Goal>.GetEnumerator()
        {
            return Behaviors.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Behaviors.GetEnumerator();
        }
    }
}

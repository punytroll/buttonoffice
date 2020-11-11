using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class CatThink : Goal
    {
        private ActionState _ActionState;
        private Double _MinutesToActionStateChange;
        
        public CatThink()
        {
            _ActionState = ActionState.Stay;
            _MinutesToActionStateChange = RandomNumberGenerator.GetDouble(10.0, 15.0);
        }
        
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
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
                    if(Cat.GetLeft() <= Cat.Office.Left)
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
                    if(Cat.GetRight() >= Cat.Office.Right)
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
}

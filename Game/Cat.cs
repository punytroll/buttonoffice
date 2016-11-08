using System;
using System.Diagnostics;
using System.Drawing;

namespace ButtonOffice
{
    public class Cat : PersistentObject
    {
        private ActionState _ActionState;
        private Color _BackgroundColor;
        private Color _BorderColor;
        private Office _Office;
        private RectangleF _Rectangle;
        private Single _MinutesToActionStateChange;

        public Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
        }

        public Cat()
        {
            _ActionState = ActionState.Stay;
            _MinutesToActionStateChange = RandomNumberGenerator.GetSingle(10.0f, 15.0f);
            _BackgroundColor = Data.CatBackgroundColor;
            _BorderColor = Data.CatBorderColor;
        }

        public void AssignOffice(Office Office)
        {
            Debug.Assert(Office != null);
            if(_Office != null)
            {
                Debug.Assert(_Office.Cat == this);
                _Office.Cat = null;
            }
            _Office = Office;
            _Office.Cat = this;
        }

        public Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public Single GetX()
        {
            return _Rectangle.X;
        }

        public Single GetY()
        {
            return _Rectangle.Y;
        }

        public void SetHeight(Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(Single X, Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetRectangle(RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
        }

        public void SetWidth(Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(Single Y)
        {
            _Rectangle.Y = Y;
        }

        public void Move(Game Game, Single GameMinutes)
        {
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
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            case ActionState.WalkLeft:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.25) == true)
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
                    _Rectangle.X -= GameMinutes * Data.CatWalkSpeed;
                    if(_Rectangle.X <= _Office.GetX())
                    {
                        _ActionState = ActionState.WalkRight;
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            case ActionState.WalkRight:
                {

                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(RandomNumberGenerator.GetBoolean(0.25) == true)
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
                    _Rectangle.X += GameMinutes * Data.CatWalkSpeed;
                    if(_Rectangle.Right >= _Office.GetRight())
                    {
                        _ActionState = ActionState.WalkLeft;
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            }
        }

        public override void Save(SaveObjectStore ObjectStore)
        {
            base.Save(ObjectStore);
            ObjectStore.Save("action-state", _ActionState);
            ObjectStore.Save("background-color", _BackgroundColor);
            ObjectStore.Save("border-color", _BorderColor);
            ObjectStore.Save("minutes-until-action-state-changes", _MinutesToActionStateChange);
            ObjectStore.Save("office", _Office);
            ObjectStore.Save("rectangle", _Rectangle);
        }

        public override void Load(LoadObjectStore ObjectStore)
        {
            base.Load(ObjectStore);
            _ActionState = ObjectStore.LoadActionStateProperty("action-state");
            _BackgroundColor = ObjectStore.LoadColorProperty("background-color");
            _BorderColor = ObjectStore.LoadColorProperty("border-color");
            _MinutesToActionStateChange = ObjectStore.LoadSingleProperty("minutes-until-action-state-changes");
            _Office = ObjectStore.LoadOfficeProperty("office");
            _Rectangle = ObjectStore.LoadRectangleProperty("rectangle");
        }
    }
}

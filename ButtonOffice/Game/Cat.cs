﻿namespace ButtonOffice
{
    internal class Cat : ButtonOffice.IPersistentObject
    {
        private ButtonOffice.ActionState _ActionState;
        private System.Drawing.Color _BackgroundColor;
        private System.Drawing.Color _BorderColor;
        private ButtonOffice.Office _Office;
        private System.Drawing.RectangleF _Rectangle;
        private System.Single _MinutesToActionStateChange;

        public System.Drawing.Color BackgroundColor
        {
            get
            {
                return _BackgroundColor;
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
        }

        public Cat()
        {
            _ActionState = ButtonOffice.ActionState.Stay;
            _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(10.0f, 15.0f);
            _BackgroundColor = ButtonOffice.Data.CatBackgroundColor;
            _BorderColor = ButtonOffice.Data.CatBorderColor;
        }

        public void AssignOffice(ButtonOffice.Office Office)
        {
            System.Diagnostics.Debug.Assert(Office != null);
            if(_Office != null)
            {
                System.Diagnostics.Debug.Assert(_Office.Cat == this);
                _Office.Cat = null;
            }
            _Office = Office;
            _Office.Cat = this;
        }

        public System.Single GetHeight()
        {
            return _Rectangle.Height;
        }

        public System.Drawing.PointF GetLocation()
        {
            return _Rectangle.Location;
        }

        public System.Drawing.RectangleF GetRectangle()
        {
            return _Rectangle;
        }

        public System.Single GetWidth()
        {
            return _Rectangle.Width;
        }

        public System.Single GetX()
        {
            return _Rectangle.X;
        }

        public System.Single GetY()
        {
            return _Rectangle.Y;
        }

        public void SetHeight(System.Single Height)
        {
            _Rectangle.Height = Height;
        }

        public void SetLocation(System.Single X, System.Single Y)
        {
            _Rectangle.X = X;
            _Rectangle.Y = Y;
        }

        public void SetRectangle(System.Drawing.RectangleF Rectangle)
        {
            _Rectangle = Rectangle;
        }

        public void SetWidth(System.Single Width)
        {
            _Rectangle.Width = Width;
        }

        public void SetX(System.Single X)
        {
            _Rectangle.X = X;
        }

        public void SetY(System.Single Y)
        {
            _Rectangle.Y = Y;
        }

        public void Move(ButtonOffice.Game Game, System.Single GameMinutes)
        {
            switch(_ActionState)
            {
            case ButtonOffice.ActionState.Stay:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(ButtonOffice.RandomNumberGenerator.GetBoolean() == true)
                        {
                            _ActionState = ButtonOffice.ActionState.WalkLeft;
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkRight;
                        }
                        _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(20.0f, 20.0f);
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            case ButtonOffice.ActionState.WalkLeft:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(ButtonOffice.RandomNumberGenerator.GetBoolean(0.25) == true)
                        {
                            _ActionState = ButtonOffice.ActionState.Stay;
                            _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkRight;
                            _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(10.0f, 8.0f);
                        }
                    }
                    _Rectangle.X -= GameMinutes * ButtonOffice.Data.CatWalkSpeed;
                    if(_Rectangle.X <= _Office.GetX())
                    {
                        _ActionState = ButtonOffice.ActionState.WalkRight;
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            case ButtonOffice.ActionState.WalkRight:
                {

                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(ButtonOffice.RandomNumberGenerator.GetBoolean(0.25) == true)
                        {
                            _ActionState = ButtonOffice.ActionState.Stay;
                            _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkLeft;
                            _MinutesToActionStateChange = ButtonOffice.RandomNumberGenerator.GetSingle(10.0f, 8.0f);
                        }
                    }
                    _Rectangle.X += GameMinutes * ButtonOffice.Data.CatWalkSpeed;
                    if(_Rectangle.Right >= _Office.GetRight())
                    {
                        _ActionState = ButtonOffice.ActionState.WalkLeft;
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            }
        }

        public virtual System.Xml.XmlElement Save(ButtonOffice.SaveGameProcessor SaveGameProcessor)
        {
            // save referenced objects
            SaveGameProcessor.Save(_Office);

            // save own properties
            System.Xml.XmlElement Result = SaveGameProcessor.CreateElement("cat");

            Result.AppendChild(SaveGameProcessor.CreateProperty("action-state", _ActionState));
            Result.AppendChild(SaveGameProcessor.CreateProperty("background-color", _BackgroundColor));
            Result.AppendChild(SaveGameProcessor.CreateProperty("border-color", _BorderColor));
            Result.AppendChild(SaveGameProcessor.CreateProperty("minutes-until-action-state-changes", _MinutesToActionStateChange));
            Result.AppendChild(SaveGameProcessor.CreateProperty("office-identifier", _Office));
            Result.AppendChild(SaveGameProcessor.CreateProperty("rectangle", _Rectangle));

            return Result;
        }
    }
}

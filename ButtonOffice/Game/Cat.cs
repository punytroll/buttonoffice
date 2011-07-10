﻿namespace ButtonOffice
{
    internal class Cat
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
            set
            {
                _BackgroundColor = value;
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
            }
        }

        public ButtonOffice.Office Office
        {
            get
            {
                return _Office;
            }
            set
            {
                _Office = value;
            }
        }

        public Cat()
        {
            _ActionState = ButtonOffice.ActionState.Stay;
            _MinutesToActionStateChange = new System.Random().NextSingle(10.0f, 15.0f);
            _BackgroundColor = ButtonOffice.Data.CatBackgroundColor;
            _BorderColor = ButtonOffice.Data.CatBorderColor;
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
                        if(new System.Random().NextSingle() <= 0.5f)
                        {
                            _ActionState = ButtonOffice.ActionState.WalkLeft;
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkRight;
                        }
                        _MinutesToActionStateChange = new System.Random().NextSingle(20.0f, 20.0f);
                    }
                    _MinutesToActionStateChange -= GameMinutes;

                    break;
                }
            case ButtonOffice.ActionState.WalkLeft:
                {
                    if(_MinutesToActionStateChange < 0.0f)
                    {
                        if(new System.Random().NextSingle() <= 0.25f)
                        {
                            _ActionState = ButtonOffice.ActionState.Stay;
                            _MinutesToActionStateChange = new System.Random().NextSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkRight;
                            _MinutesToActionStateChange = new System.Random().NextSingle(10.0f, 8.0f);
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
                        if(new System.Random().NextSingle() <= 0.25f)
                        {
                            _ActionState = ButtonOffice.ActionState.Stay;
                            _MinutesToActionStateChange = new System.Random().NextSingle(30.0f, 30.0f);
                        }
                        else
                        {
                            _ActionState = ButtonOffice.ActionState.WalkLeft;
                            _MinutesToActionStateChange = new System.Random().NextSingle(10.0f, 8.0f);
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
    }
}

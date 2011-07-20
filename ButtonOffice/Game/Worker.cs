﻿namespace ButtonOffice
{
    internal class Worker : ButtonOffice.Person
    {
        public Worker() :
            base(ButtonOffice.Type.Worker)
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.WorkerBackgroundColor;
            _BorderColor = ButtonOffice.Data.WorkerBorderColor;
            _Wage = ButtonOffice.Data.WorkerWage;
            _WorkMinutes = ButtonOffice.Data.WorkerWorkMinutes;
        }

        public override void Move(ButtonOffice.Game Game, System.Single GameMinutes)
        {
            switch(_ActionState)
            {
            case ButtonOffice.ActionState.New:
                {
                    _PlanNextWorkDay(Game);
                    _ActionState = ButtonOffice.ActionState.AtHome;

                    break;
                }
            case ButtonOffice.ActionState.PushingButton:
                {
                    if(Game.GetTotalMinutes() > _LeavesAtMinute)
                    {
                        if(_LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            _WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            _WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        _ActionState = ButtonOffice.ActionState.Leaving;
                        _ActionFraction = 0.0f;
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        _AnimationFraction = 0.0f;
                        Game.SubtractCents(_Wage);
                    }
                    else
                    {
                        if(_Desk.IsComputerBroken() == false)
                        {
                            _Desk.SetMinutesUntilComputerBroken(_Desk.GetMinutesUntilComputerBroken() - GameMinutes);
                            if(_Desk.IsComputerBroken() == true)
                            {
                                _ActionFraction = 0.0f;
                                _AnimationState = ButtonOffice.AnimationState.Standing;
                                _AnimationFraction = 0.0f;
                                if(_Desk == _Desk.Office.FirstDesk)
                                {
                                    Game.BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(_Desk.Office, ButtonOffice.BrokenThing.FirstComputer));
                                }
                                else if(_Desk == _Desk.Office.SecondDesk)
                                {
                                    Game.BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(_Desk.Office, ButtonOffice.BrokenThing.SecondComputer));
                                }
                                else if(_Desk == _Desk.Office.ThirdDesk)
                                {
                                    Game.BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(_Desk.Office, ButtonOffice.BrokenThing.ThirdComputer));
                                }
                                else if(_Desk == _Desk.Office.FourthDesk)
                                {
                                    Game.BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(_Desk.Office, ButtonOffice.BrokenThing.FourthComputer));
                                }
                            }
                            else
                            {
                                _ActionFraction += ButtonOffice.Data.WorkerWorkSpeed * GameMinutes;
                                while(_ActionFraction >= 1.0f)
                                {
                                    _ActionFraction -= 1.0f;
                                    _Desk.TrashLevel += 1.0f;
                                    Game.AddCents(100);
                                    Game.FireEarnMoney(100, GetMidLocation());
                                }
                                _AnimationFraction += ButtonOffice.Data.WorkerWorkSpeed * GameMinutes;
                                while(_AnimationFraction >= 1.0f)
                                {
                                    _AnimationFraction -= 1.0f;
                                }
                            }
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    _ActionState = ButtonOffice.ActionState.PushingButton;
                    _ActionFraction = 0.0f;
                    _AnimationState = ButtonOffice.AnimationState.PushingButton;
                    _AnimationFraction = 0.0f;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(Game.GetTotalMinutes() > _ArrivesAtMinute)
                    {
                        _ActionState = ButtonOffice.ActionState.Arriving;
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        if(_LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        _WalkTo = new System.Drawing.PointF(_Desk.GetX() + (_Desk.GetWidth() - GetWidth()) / 2.0f, _Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = _WalkTo.X - GetX();
                    System.Single DeltaY = _WalkTo.Y - GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        SetLocation(GetX() + DeltaX, GetY() + DeltaY);
                    }
                    else
                    {
                        SetLocation(_WalkTo);
                        _ActionState = ButtonOffice.ActionState.Working;
                        _AnimationState = ButtonOffice.AnimationState.Standing;
                        _AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = _WalkTo.X - GetX();
                    System.Single DeltaY = _WalkTo.Y - GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        SetLocation(GetX() + DeltaX, GetY() + DeltaY);
                    }
                    else
                    {
                        _ActionState = ButtonOffice.ActionState.AtHome;
                        _AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(Game);
                    }

                    break;
                }
            }
        }

        public override System.Xml.XmlElement Save(ButtonOffice.GameSaver GameSaver)
        {
            System.Xml.XmlElement Result = base.Save(GameSaver);
            System.Xml.XmlElement Element = GameSaver.CreateElement("worker");

            Result.AppendChild(Element);

            return Result;
        }
    }
}

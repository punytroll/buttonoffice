namespace ButtonOffice
{
    internal class ITTech : ButtonOffice.Person
    {
        private System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> _RepairingTarget;

        public ITTech() :
            base(ButtonOffice.Type.ITTech)
        {
            _ArrivesAtDayMinute = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.ITTechBackgroundColor;
            _BorderColor = ButtonOffice.Data.ITTechBorderColor;
            _RepairingTarget = null;
            _Wage = ButtonOffice.Data.ITTechWage;
            _WorkMinutes = ButtonOffice.Data.ITTechWorkMinutes;
        }

        public ButtonOffice.Office GetOffice()
        {
            System.Diagnostics.Debug.Assert(_RepairingTarget != null);

            return _RepairingTarget.First;
        }

        public System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> GetRepairingTarget()
        {
            return _RepairingTarget;
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
            case ButtonOffice.ActionState.Working:
                {
                    _ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(Game.GetTotalMinutes() > _ArrivesAtMinute)
                    {
                        _ActionState = ButtonOffice.ActionState.Arriving;
                        _AnimationFraction = 0.0f;
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        if(_LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        _WalkTo = new System.Drawing.PointF(_Desk.GetX() + (ButtonOffice.Data.DeskWidth - GetWidth()) / 2.0f, _Desk.GetY());
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
            case ButtonOffice.ActionState.WaitingForBrokenThings:
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
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        _AnimationFraction = 0.0f;
                        Game.SubtractCents(_Wage);
                    }
                    else
                    {
                        if(Game.BrokenThings.Count > 0)
                        {
                            System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = Game.BrokenThings.Dequeue();

                            switch(BrokenThing.Second)
                            {
                            case ButtonOffice.BrokenThing.FirstComputer:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.FirstDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondComputer:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.SecondDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdComputer:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.ThirdDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FourthComputer:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.FourthDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FirstLamp:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampOneX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondLamp:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampTwoX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdLamp:
                                {
                                    _WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampThreeX, BrokenThing.First.GetY());

                                    break;
                                }
                            }
                            _RepairingTarget = BrokenThing;
                            _ActionState = ButtonOffice.ActionState.GoingToRepair;
                            _AnimationState = ButtonOffice.AnimationState.Walking;
                            _AnimationFraction = 0.0f;
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToRepair:
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
                        _ActionState = ButtonOffice.ActionState.Repairing;
                        _AnimationState = ButtonOffice.AnimationState.Repairing;
                        _AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Repairing:
                {
                    _AnimationFraction += ButtonOffice.Data.ITTechRepairSpeed * GameMinutes;
                    if(_AnimationFraction >= 1.0f)
                    {
                        switch(_RepairingTarget.Second)
                        {
                        case ButtonOffice.BrokenThing.FirstComputer:
                            {
                                _RepairingTarget.First.FirstDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondComputer:
                            {
                                _RepairingTarget.First.SecondDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdComputer:
                            {
                                _RepairingTarget.First.ThirdDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FourthComputer:
                            {
                                _RepairingTarget.First.FourthDesk.SetMinutesUntilComputerBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FirstLamp:
                            {
                                _RepairingTarget.First.FirstLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondLamp:
                            {
                                _RepairingTarget.First.SecondLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdLamp:
                            {
                                _RepairingTarget.First.ThirdLamp.SetMinutesUntilBroken(ButtonOffice.RandomNumberGenerator.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        }
                        _RepairingTarget = null;
                        _ActionState = ButtonOffice.ActionState.GoingToDesk;
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        _AnimationFraction = 0.0f;
                        _WalkTo = new System.Drawing.PointF(_Desk.GetX() + (ButtonOffice.Data.DeskWidth - GetWidth()) / 2.0f, _Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToDesk:
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
                        _ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;
                        _AnimationState = ButtonOffice.AnimationState.Standing;
                        _AnimationFraction = 0.0f;
                        _Desk.TrashLevel += 1.0f;
                    }

                    break;
                }
            }
        }
    }
}

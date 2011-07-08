namespace ButtonOffice
{
    internal class Janitor : ButtonOffice.Person
    {
        private System.Collections.Generic.Queue<ButtonOffice.Desk> _CleaningTargets;

        public Janitor() :
            base(ButtonOffice.Type.Janitor)
        {
            _CleaningTargets = new System.Collections.Generic.Queue<ButtonOffice.Desk>();
            _BackgroundColor = ButtonOffice.Data.JanitorBackgroundColor;
            _BorderColor = ButtonOffice.Data.JanitorBorderColor;
            _Wage = ButtonOffice.Data.JanitorWage;
            _WorkMinutes = ButtonOffice.Data.JanitorWorkMinutes;
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
            case ButtonOffice.ActionState.GoingToClean:
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
                        _ActionState = ButtonOffice.ActionState.Cleaning;
                        _AnimationState = ButtonOffice.AnimationState.Cleaning;
                        _AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Cleaning:
                {
                    ButtonOffice.Desk Desk = _CleaningTargets.Peek();

                    if(Desk.Janitor == null)
                    {
                        Desk.Janitor = this;
                    }
                    if(Desk.Janitor == this)
                    {
                        if(Desk.TrashLevel > 0.0f)
                        {
                            _AnimationFraction += ButtonOffice.Data.JanitorCleanSpeed * GameMinutes;
                            while(_AnimationFraction > 1.0f)
                            {
                                Desk.TrashLevel -= ButtonOffice.Data.JanitorCleanAmount;
                                _AnimationFraction -= 1.0f;
                            }
                        }
                        if(Desk.TrashLevel <= 0.0f)
                        {
                            Desk.Janitor = null;
                            Desk.TrashLevel = 0.0f;
                            _CleaningTargets.Dequeue();
                            _ActionState = ButtonOffice.ActionState.PickTrash;
                            _AnimationState = ButtonOffice.AnimationState.Standing;
                            _AnimationFraction = 0.0f;
                        }
                    }
                    else
                    {
                        _CleaningTargets.Dequeue();
                        _ActionState = ButtonOffice.ActionState.PickTrash;
                        _AnimationState = ButtonOffice.AnimationState.Standing;
                        _AnimationFraction = 0.0f;
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
                        _ActionState = ButtonOffice.ActionState.WaitingToGoHome;
                        _AnimationState = ButtonOffice.AnimationState.Standing;
                        _AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.PickTrash:
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
                        _CleaningTargets.Clear();
                        Game.SubtractCents(_Wage);
                    }
                    else
                    {
                        if(_CleaningTargets.Count > 0)
                        {
                            _WalkTo = _CleaningTargets.Peek().GetLocation();
                            _ActionState = ButtonOffice.ActionState.GoingToClean;
                        }
                        else
                        {
                            _WalkTo = _Desk.GetLocation();
                            _ActionState = ButtonOffice.ActionState.GoingToDesk;
                        }
                        _AnimationState = ButtonOffice.AnimationState.Walking;
                        _AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.WaitingToGoHome:
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
                        _CleaningTargets.Clear();
                        Game.SubtractCents(_Wage);
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    foreach(ButtonOffice.Office Office in Game.Offices)
                    {
                        _CleaningTargets.Enqueue(Office.FirstDesk);
                        _CleaningTargets.Enqueue(Office.SecondDesk);
                        _CleaningTargets.Enqueue(Office.ThirdDesk);
                        _CleaningTargets.Enqueue(Office.FourthDesk);
                    }
                    _ActionState = ButtonOffice.ActionState.PickTrash;

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
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ButtonOffice
{
    internal partial class MainWindow
    {
        private PointF _CameraVelocity;
        private List<FloatingText> _FloatingTexts;
        private Person _MovePerson;
        private Game _Game;
        private List<ToolStripButton> _ToolButtons;
        private Point? _DragPoint;
        private Point _DrawingOffset;
        private EntityPrototype _EntityPrototype;
        private DateTime _LastTick;
        private Person _SelectedPerson;
        private Office _SelectedOffice;
        private Single _Zoom;
    
        public MainWindow()
        {
            InitializeComponent();
            _ToolButtons = new List<ToolStripButton>();
            _ToolButtons.Add(_HireITTechButton);
            _ToolButtons.Add(_HireJanitorButton);
            _ToolButtons.Add(_HireWorkerButton);
            _ToolButtons.Add(_BuildOfficeButton);
            _ToolButtons.Add(_BuildBathroomButton);
            _ToolButtons.Add(_HireAccountantButton);
            _ToolButtons.Add(_PlaceCatButton);
            _FloatingTexts = new List<FloatingText>();
            _Game = Game.CreateNew();
            _OnNewGame();
            _DrawingBoard.MouseWheel += delegate(Object Sender, MouseEventArgs EventArguments)
                                        {
                                            if(EventArguments.Delta > 0)
                                            {
                                                _Zoom *= 1.2f;
                                                _DrawingOffset.X = ((_DrawingOffset.X - EventArguments.X).ToSingle() * 1.2f).GetFlooredAsInt32() + EventArguments.X;
                                                _DrawingOffset.Y = ((_DrawingOffset.Y - (_DrawingBoard.Height - EventArguments.Y)).ToSingle() * 1.2f).GetFlooredAsInt32() + (_DrawingBoard.Height - EventArguments.Y);
                                            }
                                            else
                                            {
                                                _Zoom /= 1.2f;
                                                _DrawingOffset.X = ((_DrawingOffset.X - EventArguments.X).ToSingle() / 1.2f).GetFlooredAsInt32() + EventArguments.X;
                                                _DrawingOffset.Y = ((_DrawingOffset.Y - (_DrawingBoard.Height - EventArguments.Y)).ToSingle() / 1.2f).GetFlooredAsInt32() + (_DrawingBoard.Height - EventArguments.Y);
                                            }
                                        };
        }

        private void _UncheckAllToolButtons()
        {
            foreach(var Button in _ToolButtons)
            {
                Button.Checked = false;
            }
        }

        private void _ToggleOneToolButton(ToolStripButton ToggleButton)
        {
            if(ToggleButton.Checked == true)
            {
                ToggleButton.Checked = false;
            }
            else
            {
                // uncheck all other buttons (if necessary)
                foreach(var Button in _ToolButtons)
                {
                    if(Button != ToggleButton)
                    {
                        if(Button.Checked == true)
                        {
                            Button.Checked = false;
                        }
                    }
                }
                if((_MoveButton != null) && (_MoveButton.Checked == true))
                {
                    _MoveButton.Checked = false;
                }
                // check the new button
                ToggleButton.Checked = true;
            }
        }

        private void _OnToolButtonClicked(Object Sender, EventArgs EventArguments)
        {
            _ToggleOneToolButton(Sender as ToolStripButton);
        }

        private void _OnBuildOfficeButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_BuildOfficeButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Office);
                _EntityPrototype.BackgroundColor = Data.OfficeBackgroundColor;
                _EntityPrototype.BorderColor = Data.OfficeBorderColor;
                _EntityPrototype.SetHeight(Data.OfficeBlockHeight);
                _EntityPrototype.SetWidth(Data.OfficeBlockWidth);
            }
        }

        private void _OnBuildBathroomButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
             _EntityPrototype = null;
            if(_BuildBathroomButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Bathroom);
                _EntityPrototype.BackgroundColor = Data.BathroomBackgroundColor;
                _EntityPrototype.BorderColor = Data.BathroomBorderColor;
                _EntityPrototype.SetHeight(Data.BathroomBlockHeight);
                _EntityPrototype.SetWidth(Data.BathroomBlockWidth);
            }
        }

        private void _OnHireITTechButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireITTechButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.ITTech);
                _EntityPrototype.BackgroundColor = Data.ITTechBackgroundColor;
                _EntityPrototype.BorderColor = Data.ITTechBorderColor;
                _EntityPrototype.SetHeight(Data.PersonHeight);
                _EntityPrototype.SetWidth(Data.PersonWidth);
            }
        }

        private void _OnHireJanitorButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireJanitorButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Janitor);
                _EntityPrototype.BackgroundColor = Data.JanitorBackgroundColor;
                _EntityPrototype.BorderColor = Data.JanitorBorderColor;
                _EntityPrototype.SetHeight(Data.PersonHeight);
                _EntityPrototype.SetWidth(Data.PersonWidth);
            }
        }

        private void _OnHireWorkerButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireWorkerButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Worker);
                _EntityPrototype.BackgroundColor = Data.WorkerBackgroundColor;
                _EntityPrototype.BorderColor = Data.WorkerBorderColor;
                _EntityPrototype.SetHeight(Data.PersonHeight);
                _EntityPrototype.SetWidth(Data.PersonWidth);
            }
        }

        private void _OnHireAccountantButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireAccountantButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Accountant);
                _EntityPrototype.BackgroundColor = Data.AccountantBackgroundColor;
                _EntityPrototype.BorderColor = Data.AccountantBorderColor;
                _EntityPrototype.SetHeight(Data.PersonHeight);
                _EntityPrototype.SetWidth(Data.PersonWidth);
            }
        }

        private void _OnPlaceCatButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_PlaceCatButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(Type.Cat);
                _EntityPrototype.BackgroundColor = Data.CatBackgroundColor;
                _EntityPrototype.BorderColor = Data.CatBorderColor;
                _EntityPrototype.SetHeight(Data.CatHeight);
                _EntityPrototype.SetWidth(Data.CatWidth);
            }
        }

        private void _OnDrawingBoardMouseMoved(Object Sender, MouseEventArgs EventArguments)
        {
            if(_DragPoint.HasValue == true)
            {
                _DrawingOffset.X -= _DragPoint.Value.X - EventArguments.X;
                _DrawingOffset.Y += _DragPoint.Value.Y - EventArguments.Y;
                _DragPoint = EventArguments.Location;
            }
            else
            {
                if(_EntityPrototype != null)
                {
                    _EntityPrototype.SetLocationFromGamingLocation(_GetGamingLocation(EventArguments.Location).GetFloored());
                    _DrawingBoard.Invalidate();
                }
            }

            var GamingLocation = _GetGamingLocation(EventArguments.Location);

            _PositionLabel.Text = "Location: " + GamingLocation.X.GetFlooredAsInt32() + " / " + GamingLocation.Y.GetFlooredAsInt32();
        }

        private void _OnDrawingBoardMouseDown(Object Sender, MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == MouseButtons.Right)
            {
                _DragPoint = EventArguments.Location;
            }
            else if(EventArguments.Button == MouseButtons.Left)
            {
                if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
                {
                    if(_EntityPrototype.Type == Type.Office)
                    {
                        if(_Game.BuildOffice(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _BuildOfficeButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.Bathroom)
                    {
                        if(_Game.BuildBathroom(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _BuildBathroomButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.Worker)
                    {
                        if(_Game.HireWorker(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _HireWorkerButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.ITTech)
                    {
                        if(_Game.HireITTech(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _HireITTechButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.Janitor)
                    {
                        if(_Game.HireJanitor(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _HireJanitorButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.Accountant)
                    {
                        if(_Game.HireAccountant(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _HireAccountantButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == Type.Cat)
                    {
                        if(_Game.PlaceCat(_EntityPrototype.Rectangle) == true)
                        {
                            if(ModifierKeys != Keys.Shift)
                            {
                                _PlaceCatButton.Checked = false;
                            }
                        }
                    }
                }
                else if(_MovePerson != null)
                {
                    var GamingLocation = _GetGamingLocation(EventArguments.Location);
                    var Desk = _Game.GetDesk(GamingLocation);

                    if((Desk != null) && (Desk.IsFree() == true))
                    {
                        _Game.MovePerson(_MovePerson, Desk);
                        _MoveButton.Checked = false;
                    }
                }
                else
                {
                    var GamingLocation = _GetGamingLocation(EventArguments.Location);
                    var Selected = false;

                    _MainSplitContainer.Panel2.Controls.Clear();
                    _SelectedOffice = null;
                    _SelectedPerson = null;
                    if(Selected == false)
                    {
                        foreach(var Person in _Game.Persons)
                        {
                            if(Person.GetRectangle().Contains(GamingLocation) == true)
                            {
                                _SelectedPerson = Person;
                                Selected = true;

                                var TypeLabel = new Label();

                                TypeLabel.Location = new Point(10, 20);
                                TypeLabel.Size = new Size(100, 20);
                                TypeLabel.Text = Person.GetType().Name;
                                _MainSplitContainer.Panel2.Controls.Add(TypeLabel);

                                var NameCaptionLabel = new Label();

                                NameCaptionLabel.Location = new Point(10, 40);
                                NameCaptionLabel.Size = new Size(100, 20);
                                NameCaptionLabel.Text = "Name:";
                                _MainSplitContainer.Panel2.Controls.Add(NameCaptionLabel);

                                var NameLabel = new Label();

                                NameLabel.Location = new Point(110, 40);
                                NameLabel.Size = new Size(100, 20);
                                NameLabel.Text = Person.Name;
                                _MainSplitContainer.Panel2.Controls.Add(NameLabel);

                                var FireButton = new Button();

                                FireButton.Location = new Point(10, 80);
                                FireButton.Size = new Size(100, 20);
                                FireButton.Text = "Fire";
                                FireButton.Click += delegate
                                                    {
                                                        _Game.FirePerson(Person);
                                                        _MainSplitContainer.Panel2Collapsed = true;
                                                        _MainSplitContainer.Panel2.Controls.Clear();
                                                    };
                                _MainSplitContainer.Panel2.Controls.Add(FireButton);
                                _MoveButton = new CheckBox();
                                _MoveButton.TextAlign = ContentAlignment.MiddleCenter;
                                _MoveButton.Location = new Point(10, 120);
                                _MoveButton.Size = new Size(100, 20);
                                _MoveButton.Text = "Move";
                                _MoveButton.Appearance = Appearance.Button;
                                _MoveButton.Checked = false;
                                _MoveButton.CheckedChanged += delegate
                                                              {
                                    if(_MoveButton.Checked == true)
                                    {
                                        _UncheckAllToolButtons();
                                        _MovePerson = Person;
                                    }
                                    else
                                    {
                                        _MovePerson = null;
                                    }
                                };
                                _MainSplitContainer.Panel2.Controls.Add(_MoveButton);

                                break;
                            }
                        }
                    }
                    if(Selected == false)
                    {
                        foreach(var Office in _Game.Offices)
                        {
                            if(Office.GetRectangle().Contains(GamingLocation) == true)
                            {
                                _SelectedOffice = Office;
                                Selected = true;

                                var NameCaptionLabel = new Label();

                                NameCaptionLabel.Location = new Point(10, 20);
                                NameCaptionLabel.Size = new Size(100, 20);
                                NameCaptionLabel.Text = "Office";
                                _MainSplitContainer.Panel2.Controls.Add(NameCaptionLabel);

                                break;
                            }
                        }
                    }
                    _MainSplitContainer.Panel2Collapsed = !Selected;
                }
            }
        }

        private void _OnDrawingBoardMouseUp(Object Sender, MouseEventArgs EventArguments)
        {
            _DragPoint = new Point?();
        }

        private Color _MixToWhite(Color Color, Single Fraction)
        {
            return Color.FromArgb((Color.R + (255 - Color.R) * Fraction).GetTruncatedAsInt32(), (Color.G + (255 - Color.G) * Fraction).GetTruncatedAsInt32(), (Color.B + (255 - Color.B) * Fraction).GetTruncatedAsInt32());
        }

        private void _OnDrawingBoardPaint(Object Sender, PaintEventArgs EventArguments)
        {
            _DrawingOffset.X += _CameraVelocity.X.GetFlooredAsInt32();
            _DrawingOffset.Y += _CameraVelocity.Y.GetFlooredAsInt32();
            for(var Row = 0; Row < Data.WorldBlockHeight; ++Row)
            {
                var BuildingMinimumMaximum = _Game.GetBuildingMinimumMaximum(Row);

                if(BuildingMinimumMaximum.Second.ToInt64() - BuildingMinimumMaximum.First.ToInt64() > 0)
                {
                    _DrawRectangle(EventArguments.Graphics, new RectangleF(BuildingMinimumMaximum.First.ToSingle(), Row.ToSingle(), (BuildingMinimumMaximum.Second - BuildingMinimumMaximum.First).ToSingle(), 1.0f), Data.BuildingBackgroundColor, Data.BuildingBorderColor);
                }
                else
                {
                    break;
                }
            }
            EventArguments.Graphics.FillRectangle(new SolidBrush(Data.GroundColor), 0, _GetDrawingY(0), _DrawingBoard.Width, _DrawingBoard.Height);
            foreach(var Office in _Game.Offices)
            {
                _DrawRectangle(EventArguments.Graphics, Office.GetRectangle(), Office.BackgroundColor, Office.BorderColor);

                Color LampColor;

                LampColor = Color.Yellow;
                if(Office.FirstLamp.IsBroken() == true)
                {
                    LampColor = Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstLamp.GetRectangle(), LampColor, Color.Black);
                LampColor = Color.Yellow;
                if(Office.SecondLamp.IsBroken() == true)
                {
                    LampColor = Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondLamp.GetRectangle(), LampColor, Color.Black);
                LampColor = Color.Yellow;
                if(Office.ThirdLamp.IsBroken() == true)
                {
                    LampColor = Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdLamp.GetRectangle(), LampColor, Color.Black);
            }
            foreach(var Bathroom in _Game.Bathrooms)
            {
                _DrawRectangle(EventArguments.Graphics, Bathroom.GetRectangle(), Bathroom.GetBackgroundColor(), Bathroom.GetBorderColor());
            }
            foreach(var Person in _Game.Persons)
            {
                if(Person.IsHidden() == false)
                {
                    _DrawRectangle(EventArguments.Graphics, Person.GetRectangle(), _MixToWhite(Person.BackgroundColor, Person.GetActionFraction()), _MixToWhite(Person.BorderColor, Person.GetAnimationFraction()));
                }
            }
            foreach(var Office in _Game.Offices)
            {
                Color PersonAtDeskColor;
                Color PersonColor;
                Color ComputerColor;

                // first desk
                PersonAtDeskColor = Color.White;
                PersonColor = Color.White;
                ComputerColor = Data.ComputerBackgroundColor;
                if(Office.FirstDesk.IsFree() == false)
                {
                    PersonColor = Office.FirstDesk.GetPerson().BackgroundColor;
                    if(Office.FirstDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.FirstDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.FirstDesk.GetY() + (Office.FirstDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - Data.ComputerWidth) / 2.0f, Office.FirstDesk.GetY() + Office.FirstDesk.GetHeight() + 0.04f, Data.ComputerWidth, Data.ComputerHeight, ComputerColor, Data.ComputerBorderColor);
                // second desk
                PersonAtDeskColor = Color.White;
                PersonColor = Color.White;
                ComputerColor = Data.ComputerBackgroundColor;
                if(Office.SecondDesk.IsFree() == false)
                {
                    PersonColor = Office.SecondDesk.GetPerson().BackgroundColor;
                    if(Office.SecondDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.SecondDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.SecondDesk.GetY() + (Office.SecondDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - Data.ComputerWidth) / 2.0f, Office.SecondDesk.GetY() + Office.SecondDesk.GetHeight() + 0.04f, Data.ComputerWidth, Data.ComputerHeight, ComputerColor, Data.ComputerBorderColor);
                // third desk
                PersonAtDeskColor = Color.White;
                PersonColor = Color.White;
                ComputerColor = Data.ComputerBackgroundColor;
                if(Office.ThirdDesk.IsFree() == false)
                {
                    PersonColor = Office.ThirdDesk.GetPerson().BackgroundColor;
                    if(Office.ThirdDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.ThirdDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.ThirdDesk.GetY() + (Office.ThirdDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - Data.ComputerWidth) / 2.0f, Office.ThirdDesk.GetY() + Office.ThirdDesk.GetHeight() + 0.04f, Data.ComputerWidth, Data.ComputerHeight, ComputerColor, Data.ComputerBorderColor);
                // fourth desk
                PersonAtDeskColor = Color.White;
                PersonColor = Color.White;
                ComputerColor = Data.ComputerBackgroundColor;
                if(Office.FourthDesk.IsFree() == false)
                {
                    PersonColor = Office.FourthDesk.GetPerson().BackgroundColor;
                    if(Office.FourthDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.FourthDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.FourthDesk.GetY() + (Office.FourthDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - Data.ComputerWidth) / 2.0f, Office.FourthDesk.GetY() + Office.FourthDesk.GetHeight() + 0.04f, Data.ComputerWidth, Data.ComputerHeight, ComputerColor, Data.ComputerBorderColor);
                // cat
                if(Office.Cat != null)
                {
                    _DrawEllipse(EventArguments.Graphics, Office.Cat.GetRectangle(), Office.Cat.BackgroundColor, Office.Cat.BorderColor);
                }
            }

            var Font = new Font("Arial", 16.0f);
            var Format = new StringFormat();

            Format.Alignment = StringAlignment.Center;
            foreach(var FloatingText in _FloatingTexts)
            {
                EventArguments.Graphics.DrawString(FloatingText.Text, Font, new SolidBrush(FloatingText.Color), _MovePointByOffset(_GetDrawingLocation(FloatingText.Origin), FloatingText.Offset), Format);
            }
            if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
            {
                if(_EntityPrototype.Type == Type.Cat)
                {
                    _DrawEllipse(EventArguments.Graphics, _EntityPrototype.Rectangle, Color.FromArgb(150, _EntityPrototype.BackgroundColor), Color.FromArgb(150, _EntityPrototype.BorderColor));
                }
                else
                {
                    _DrawRectangle(EventArguments.Graphics, _EntityPrototype.Rectangle, Color.FromArgb(150, _EntityPrototype.BackgroundColor), Color.FromArgb(150, _EntityPrototype.BorderColor));
                }
            }
        }

        private void _DrawEllipse(Graphics Graphics, RectangleF GameRectangle, Color BackgroundColor, Color BorderColor)
        {
            var BackgroundRectangle = _GetDrawingRectangle(GameRectangle);

            Graphics.FillEllipse(new SolidBrush(BackgroundColor), BackgroundRectangle);

            var ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawEllipse(new Pen(BorderColor), ForegroundRectangle);
        }

        private void _DrawRectangle(Graphics Graphics, RectangleF GameRectangle, Color BackgroundColor, Color BorderColor)
        {
            var BackgroundRectangle = _GetDrawingRectangle(GameRectangle);

            Graphics.FillRectangle(new SolidBrush(BackgroundColor), BackgroundRectangle);

            var ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawRectangle(new Pen(BorderColor), ForegroundRectangle);
        }

        private void _DrawRectangle(Graphics Graphics, Single GameX, Single GameY, Single GameWidth, Single GameHeight, Color BackgroundColor, Color BorderColor)
        {
            var BackgroundRectangle = _GetDrawingRectangle(GameX, GameY, GameWidth, GameHeight);

            Graphics.FillRectangle(new SolidBrush(BackgroundColor), BackgroundRectangle);

            var ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawRectangle(new Pen(BorderColor), ForegroundRectangle);
        }

        #region Coordinate system transformations: Game -> Draw
        private Int32 _GetDrawingHeight(Single GamingHeight)
        {
            return (GamingHeight * Data.BlockHeight.ToSingle() * _Zoom).GetFlooredAsInt32();
        }

        private Int32 _GetDrawingWidth(Single GamingWidth)
        {
            return (GamingWidth * Data.BlockWidth.ToSingle() * _Zoom).GetFlooredAsInt32();
        }

        private Int32 _GetDrawingY(Single GamingY)
        {
            return (_DrawingBoard.Height.ToSingle() - (GamingY * Data.BlockHeight.ToSingle() * _Zoom) - _DrawingOffset.Y.ToSingle()).GetFlooredAsInt32();
        }

        private Int32 _GetDrawingX(Single GamingX)
        {
            return (GamingX * Data.BlockWidth.ToSingle() * _Zoom + _DrawingOffset.X.ToSingle()).GetFlooredAsInt32();
        }

        private Rectangle _GetDrawingRectangle(RectangleF GamingRectangle)
        {
            var DrawingPoint = _GetDrawingLocation(GamingRectangle.Location);
            var DrawingSize = _GetDrawingSize(GamingRectangle.Size);

            return new Rectangle(DrawingPoint.X, DrawingPoint.Y - DrawingSize.Height, DrawingSize.Width, DrawingSize.Height);
        }

        private Rectangle _GetDrawingRectangle(Single GamingX, Single GamingY, Single GamingWidth, Single GamingHeight)
        {
            var DrawingHeight = _GetDrawingHeight(GamingHeight);

            return new Rectangle(_GetDrawingX(GamingX), _GetDrawingY(GamingY) - DrawingHeight, _GetDrawingWidth(GamingWidth), DrawingHeight);
        }

        private Size _GetDrawingSize(SizeF GamingSize)
        {
            return new Size(_GetDrawingWidth(GamingSize.Width), _GetDrawingHeight(GamingSize.Height));
        }

        private Point _GetDrawingLocation(PointF GamingLocation)
        {
            return new Point(_GetDrawingX(GamingLocation.X), _GetDrawingY(GamingLocation.Y));
        }
        #endregion

        #region Coordinate system transformations: Draw -> Game
        private Single _GetGamingX(Int32 DrawingX)
        {
            return (DrawingX - _DrawingOffset.X).ToSingle() / Data.BlockWidth.ToSingle() / _Zoom;
        }

        private Single _GetGamingY(Int32 DrawingY)
        {
            return (_DrawingBoard.Height - DrawingY - _DrawingOffset.Y).ToSingle() / Data.BlockHeight.ToSingle() / _Zoom;
        }

        private PointF _GetGamingLocation(Point DrawingLocation)
        {
            return new PointF(_GetGamingX(DrawingLocation.X), _GetGamingY(DrawingLocation.Y));
        }
        #endregion

        #region Coordinate system helper functions
        private PointF _MovePointByOffset(PointF Point, PointF Offset)
        {
            return new PointF(Point.X + Offset.X, Point.Y + Offset.Y);
        }
        #endregion

        private void _OnMainWindowResized(Object Sender, EventArgs EventArguments)
        {
            _DrawingBoard.Invalidate();
        }

        private void _OnTimerTicked(Object Sender, EventArgs EventArguments)
        {
            var Now = DateTime.Now;
            var Seconds = (Now - _LastTick).TotalSeconds.ToSingle();

            if((Seconds > 0.0f) && (Seconds < 0.05f))
            {
                _Game.Move(Data.GameMinutesPerSecond * Seconds);

                var Index = 0;

                while(Index < _FloatingTexts.Count)
                {
                    _FloatingTexts[Index].SetTimeout(_FloatingTexts[Index].Timeout - Seconds);
                    if(_FloatingTexts[Index].Timeout > 0.0f)
                    {
                        _FloatingTexts[Index].SetOffset(new PointF(_FloatingTexts[Index].Offset.X, _FloatingTexts[Index].Offset.Y - Seconds * Data.FloatingTextSpeed));
                        ++Index;
                    }
                    else
                    {
                        _FloatingTexts.RemoveAt(Index);
                    }
                }
                _TimeLabel.Text = "Day && Time: " + new TimeSpan(_Game.GetDay().ToInt32(), 0, _Game.GetMinuteOfDay().ToInt32(), 0);
                _MoneyLabel.Text = "Money: " + _Game.GetMoneyString(_Game.GetCents());
                _EmployeesLabel.Text = "Employees: " + _Game.Persons.Count;
                if(_Game.GetCatStock() > 0)
                {
                    if(_PlaceCatButton.Enabled == false)
                    {
                        _PlaceCatButton.Enabled = true;
                    }
                    if(_Game.GetCatStock() == 1)
                    {
                        _PlaceCatButton.Text = "Cat (1)";
                    }
                    else
                    {
                        _PlaceCatButton.Text = "Cat (" + _Game.GetCatStock() + ")";
                    }
                }
                else
                {
                    _PlaceCatButton.Text = "Cat";
                    if(_PlaceCatButton.Enabled == true)
                    {
                        _PlaceCatButton.Enabled = false;
                    }
                }
                _DrawingBoard.Invalidate();
            }
            _LastTick = Now;
        }

        private void _OnMainWindowLoaded(Object Sender, EventArgs EventArguments)
        {
            _DrawingBoard.BackColor = Data.BackgroundColor;
            _StartGame();
        }

        private void _DrawingBoardKeyDown(Object Sender, KeyEventArgs EventArguments)
        {
            if(EventArguments.KeyCode == Keys.W)
            {
                _CameraVelocity.Y = -10;
            }
            else if(EventArguments.KeyCode == Keys.A)
            {
                _CameraVelocity.X = 10;
            }
            else if(EventArguments.KeyCode == Keys.S)
            {
                _CameraVelocity.Y = 10;
            }
            else if(EventArguments.KeyCode == Keys.D)
            {
                _CameraVelocity.X = -10;
            }
        }

        private void _DrawingBoardKeyUp(Object Sender, KeyEventArgs EventArguments)
        {
            if(EventArguments.KeyCode == Keys.W)
            {
                _CameraVelocity.Y = 0;
            }
            else if(EventArguments.KeyCode == Keys.A)
            {
                _CameraVelocity.X = 0;
            }
            else if(EventArguments.KeyCode == Keys.S)
            {
                _CameraVelocity.Y = 0;
            }
            else if(EventArguments.KeyCode == Keys.D)
            {
                _CameraVelocity.X = 0;
            }
        }

        private void _StopGame()
        {
            _Timer.Stop();
        }

        private void _StartGame()
        {
            _Timer.Start();
        }

        private void _OnNewGameButtonClicked(Object Sender, EventArgs EventArguments)
        {
            _StopGame();
            _Game = Game.CreateNew();
            _OnNewGame();
            _StartGame();
        }

        private void _OnSaveGameButtonClicked(Object Sender, EventArgs EventArguments)
        {
            _StopGame();

            var SaveFileDialog = new SaveFileDialog();

            if(SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _Game.SaveToFile(SaveFileDialog.FileName);
            }
            _StartGame();
        }

        private void _OnLoadGameButtonClicked(Object Sender, EventArgs EventArguments)
        {
            _StopGame();

            var OpenFileDialog = new OpenFileDialog();

            if(OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                var OldGame = _Game;

                try
                {
                    _Game = Game.LoadFromFileName(OpenFileDialog.FileName);
                    _OnNewGame();
                }
                catch(GameLoadException Exception)
                {
                    MessageBox.Show(Exception.Message, "Error while loading save game file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _Game = OldGame;
                }
            }
            _StartGame();
        }

        private void _OnQuitApplicationButtonClicked(Object Sender, EventArgs EventArguments)
        {
            _StopGame();
            Close();
        }

        private void _OnNewGame()
        {
            // uncheck all buttons
            _UncheckAllToolButtons();
            _CameraVelocity = new PointF(0.0f, 0.0f);
            _FloatingTexts.Clear();
            _Game.OnEarnMoney += delegate(UInt64 Cents, PointF Location)
                                 {
                                     var FloatingText = new FloatingText();

                                     FloatingText.SetColor(Data.EarnMoneyFloatingTextColor);
                                     FloatingText.SetOffset(new PointF(0.0f, 0.0f));
                                     FloatingText.SetOrigin(Location);
                                     FloatingText.SetText(_Game.GetMoneyString(Cents));
                                     FloatingText.SetTimeout(1.2f);
                                     _FloatingTexts.Add(FloatingText);
                                 };
            _Game.OnSpendMoney += delegate(UInt64 Cents, PointF Location)
                                  {
                                      var FloatingText = new FloatingText();

                                      FloatingText.SetColor(Data.SpendMoneyFloatingTextColor);
                                      FloatingText.SetOffset(new PointF(0.0f, 0.0f));
                                      FloatingText.SetOrigin(Location);
                                      FloatingText.SetText(_Game.GetMoneyString(Cents));
                                      FloatingText.SetTimeout(1.2f);
                                      _FloatingTexts.Add(FloatingText);
                                  };
            _EntityPrototype = null;
            _DragPoint = new Point?();
            _DrawingOffset = new Point(-Data.WorldBlockWidth * Data.BlockWidth / 2, 2 * Data.BlockHeight);
            _LastTick = DateTime.MinValue;
            _MoveButton = null;
            _MovePerson = null;
            _Zoom = 1.0f;
        }
    }
}

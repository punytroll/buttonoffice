using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ButtonOffice
{
    internal partial class MainWindow
    {
        private Boolean _WPressed;
        private Boolean _APressed;
        private Boolean _SPressed;
        private Boolean _DPressed;
        private Vector2 _CameraPosition;
        private Vector2 _CameraVelocity;
        private readonly List<FloatingText> _FloatingTexts;
        private Person _MovePerson;
        private Game _Game;
        private readonly List<ToolStripButton> _ToolButtons;
        private Point? _DragPoint;
        private Boolean? _Dragged;
        private EntityPrototype _EntityPrototype;
        private DateTime _LastTick;
        private PersistentObject _SelectedObject;
        private Double _Zoom;

        public MainWindow()
        {
            InitializeComponent();
            _ToolButtons = new List<ToolStripButton>();
            _ToolButtons.Add(_BuildBathroomButton);
            _ToolButtons.Add(_BuildOfficeButton);
            _ToolButtons.Add(_BuildStairsButton);
            _ToolButtons.Add(_HireAccountantButton);
            _ToolButtons.Add(_HireITTechButton);
            _ToolButtons.Add(_HireJanitorButton);
            _ToolButtons.Add(_HireWorkerButton);
            _ToolButtons.Add(_PlaceCatButton);
            _FloatingTexts = new List<FloatingText>();
            _Game = Game.CreateNew();
            _OnNewGame();
            _DrawingBoard.MouseWheel += delegate(Object Sender, MouseEventArgs EventArguments)
                                        {
                                            var OldGamingLocation = _GetGamingLocation(EventArguments.Location);

                                            if(EventArguments.Delta > 0)
                                            {
                                                _Zoom *= 1.2;
                                            }
                                            else
                                            {
                                                _Zoom /= 1.2;
                                            }

                                            var NewGamingLocation = _GetGamingLocation(EventArguments.Location);

                                            _CameraPosition.X += OldGamingLocation.X - NewGamingLocation.X;
                                            _CameraPosition.Y += OldGamingLocation.Y - NewGamingLocation.Y;
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
            _UnselectObject();
            _ToggleOneToolButton(Sender as ToolStripButton);
        }

        private void _OnBuildBathroomButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
             _EntityPrototype = null;
            if(_BuildBathroomButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.BackgroundColor = Data.BathroomBackgroundColor;
                _EntityPrototype.BorderColor = Data.BathroomBorderColor;
                _EntityPrototype.SetGameFunction(_Game.BuildBathroom);
                _EntityPrototype.SetHeight(Data.BathroomBlockHeight);
                _EntityPrototype.SetWidth(Data.BathroomBlockWidth);
            }
        }

        private void _OnBuildOfficeButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_BuildOfficeButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.BackgroundColor = Data.OfficeBackgroundColor;
                _EntityPrototype.BorderColor = Data.OfficeBorderColor;
                _EntityPrototype.SetGameFunction(_Game.BuildOffice);
                _EntityPrototype.SetHeight(Data.OfficeBlockHeight);
                _EntityPrototype.SetWidth(Data.OfficeBlockWidth);
            }
        }

        private void _OnBuildStairsButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_BuildStairsButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.BackgroundColor = Data.StairsBackgroundColor;
                _EntityPrototype.BorderColor = Data.StairsBorderColor;
                _EntityPrototype.SetGameFunction(_Game.BuildStairs);
                _EntityPrototype.SetHeight(Data.StairsBlockHeight);
                _EntityPrototype.SetWidth(Data.StairsBlockWidth);
            }
        }

        private void _OnHireAccountantButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireAccountantButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.SnapToBlocksHorizontally = false;
                _EntityPrototype.BackgroundColor = Data.AccountantBackgroundColor;
                _EntityPrototype.BorderColor = Data.AccountantBorderColor;
                _EntityPrototype.SetGameFunction(_Game.HireAccountant);
                _EntityPrototype.SetHeight(Data.PersonHeightMean);
                _EntityPrototype.SetWidth(Data.PersonWidthMean);
            }
        }

        private void _OnHireITTechButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireITTechButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.SnapToBlocksHorizontally = false;
                _EntityPrototype.BackgroundColor = Data.ITTechBackgroundColor;
                _EntityPrototype.BorderColor = Data.ITTechBorderColor;
                _EntityPrototype.SetGameFunction(_Game.HireITTech);
                _EntityPrototype.SetHeight(Data.PersonHeightMean);
                _EntityPrototype.SetWidth(Data.PersonWidthMean);
            }
        }

        private void _OnHireJanitorButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireJanitorButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.SnapToBlocksHorizontally = false;
                _EntityPrototype.BackgroundColor = Data.JanitorBackgroundColor;
                _EntityPrototype.BorderColor = Data.JanitorBorderColor;
                _EntityPrototype.SetGameFunction(_Game.HireJanitor);
                _EntityPrototype.SetHeight(Data.PersonHeightMean);
                _EntityPrototype.SetWidth(Data.PersonWidthMean);
            }
        }

        private void _OnHireWorkerButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_HireWorkerButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.SnapToBlocksHorizontally = false;
                _EntityPrototype.BackgroundColor = Data.WorkerBackgroundColor;
                _EntityPrototype.BorderColor = Data.WorkerBorderColor;
                _EntityPrototype.SetGameFunction(_Game.HireWorker);
                _EntityPrototype.SetHeight(Data.PersonHeightMean);
                _EntityPrototype.SetWidth(Data.PersonWidthMean);
            }
        }

        private void _OnPlaceCatButtonCheckedChanged(Object Sender, EventArgs EventArguments)
        {
            _EntityPrototype = null;
            if(_PlaceCatButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype();
                _EntityPrototype.SnapToBlocksHorizontally = false;
                _EntityPrototype.BackgroundColor = Data.CatBackgroundColor;
                _EntityPrototype.BorderColor = Data.CatBorderColor;
                _EntityPrototype.DrawType = DrawType.Ellipse;
                _EntityPrototype.SetGameFunction(_Game.PlaceCat);
                _EntityPrototype.SetHeight(Data.CatHeight);
                _EntityPrototype.SetWidth(Data.CatWidth);
            }
        }

        private void _OnDrawingBoardMouseMoved(Object Sender, MouseEventArgs EventArguments)
        {
            if(_DragPoint != null)
            {
                Debug.Assert(_Dragged != null);
                _CameraPosition.X += _GetGamingWidth(_DragPoint.Value.X - EventArguments.X);
                _CameraPosition.Y += _GetGamingHeight(_DragPoint.Value.Y - EventArguments.Y);
                _DragPoint = EventArguments.Location;
                _Dragged = true;
            }
            else
            {
                if(_EntityPrototype != null)
                {
                    _EntityPrototype.SetLocationFromGamingLocation(_GetGamingLocation(EventArguments.Location));
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
                Debug.Assert(_DragPoint == null);
                Debug.Assert(_Dragged == null);
                _DragPoint = EventArguments.Location;
                _Dragged = false;
            }
            else if(EventArguments.Button == MouseButtons.Left)
            {
                if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
                {
                    if(_EntityPrototype.CallGameFunction() == true)
                    {
                        if(ModifierKeys != Keys.Shift)
                        {
                            _UncheckAllToolButtons();
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

                    _MainSplitContainer.Panel1.Controls.Clear();
                    _SelectedObject = null;
                    if(_SelectedObject == null)
                    {
                        foreach(var Person in _Game.Persons)
                        {
                            if(Person.Contains(GamingLocation) == true)
                            {
                                _SelectedObject = Person;

                                var TypeLabel = new Label();

                                TypeLabel.Location = new Point(10, 20);
                                TypeLabel.Size = new Size(100, 20);
                                TypeLabel.Text = Person.GetType().Name;
                                _MainSplitContainer.Panel1.Controls.Add(TypeLabel);

                                var NameCaptionLabel = new Label();

                                NameCaptionLabel.Location = new Point(10, 40);
                                NameCaptionLabel.Size = new Size(100, 20);
                                NameCaptionLabel.Text = "Name:";
                                _MainSplitContainer.Panel1.Controls.Add(NameCaptionLabel);

                                var NameLabel = new Label();

                                NameLabel.Location = new Point(110, 40);
                                NameLabel.Size = new Size(100, 20);
                                NameLabel.Text = Person.Name;
                                _MainSplitContainer.Panel1.Controls.Add(NameLabel);

                                var FireButton = new Button();

                                FireButton.Location = new Point(10, 80);
                                FireButton.Size = new Size(100, 20);
                                FireButton.Text = "Fire";
                                FireButton.Click += delegate
                                                    {
                                                        _Game.FirePerson(Person);
                                                        _MainSplitContainer.Panel1Collapsed = true;
                                                        _MainSplitContainer.Panel1.Controls.Clear();
                                                    };
                                _MainSplitContainer.Panel1.Controls.Add(FireButton);
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
                                _MainSplitContainer.Panel1.Controls.Add(_MoveButton);

                                break;
                            }
                        }
                    }
                    if(_SelectedObject == null)
                    {
                        foreach(var Building in _Game.Buildings)
                        {
                            if(Building.Contains(GamingLocation) == true)
                            {
                                _SelectedObject = Building;

                                var NameCaptionLabel = new Label();

                                NameCaptionLabel.Location = new Point(10, 20);
                                NameCaptionLabel.Size = new Size(100, 20);
                                NameCaptionLabel.Text = Building.GetType().Name;
                                _MainSplitContainer.Panel1.Controls.Add(NameCaptionLabel);

                                var DestroyButton = new Button();

                                DestroyButton.Location = new Point(10, 80);
                                DestroyButton.Size = new Size(120, 20);
                                DestroyButton.Text = "Destroy";
                                DestroyButton.Enabled = Building.CanDestroy();
                                DestroyButton.Click += delegate
                                                       {
                                                           _Game.Destroy(Building);
                                                           _MainSplitContainer.Panel1Collapsed = true;
                                                           _MainSplitContainer.Panel1.Controls.Clear();
                                                       };
                                _MainSplitContainer.Panel1.Controls.Add(DestroyButton);

                                var Stairs = Building as Stairs;

                                if(Stairs != null)
                                {
                                    var AddHighestFloorButton = new Button();

                                    AddHighestFloorButton.Location = new Point(10, 110);
                                    AddHighestFloorButton.Size = new Size(120, 20);
                                    AddHighestFloorButton.Text = "Add highest floor";
                                    AddHighestFloorButton.Click += delegate
                                                                   {
                                                                       Stairs.AddHighestFloor(_Game);
                                                                   };
                                    _MainSplitContainer.Panel1.Controls.Add(AddHighestFloorButton);

                                    var RemoveHighestFloorButton = new Button();

                                    RemoveHighestFloorButton.Location = new Point(10, 140);
                                    RemoveHighestFloorButton.Size = new Size(120, 20);
                                    RemoveHighestFloorButton.Text = "Remove highest floor";
                                    RemoveHighestFloorButton.Click += delegate
                                                                      {
                                                                          Stairs.RemoveHighestFloor(_Game);
                                                                      };
                                    _MainSplitContainer.Panel1.Controls.Add(RemoveHighestFloorButton);

                                    var AddLowestFloorButton = new Button();

                                    AddLowestFloorButton.Location = new Point(10, 170);
                                    AddLowestFloorButton.Size = new Size(120, 20);
                                    AddLowestFloorButton.Text = "Add lowest floor";
                                    AddLowestFloorButton.Click += delegate
                                                                  {
                                                                      Stairs.AddLowestFloor(_Game);
                                                                  };
                                    _MainSplitContainer.Panel1.Controls.Add(AddLowestFloorButton);

                                    var RemoveLowestFloorButton = new Button();

                                    RemoveLowestFloorButton.Location = new Point(10, 200);
                                    RemoveLowestFloorButton.Size = new Size(120, 20);
                                    RemoveLowestFloorButton.Text = "Remove lowest floor";
                                    RemoveLowestFloorButton.Click += delegate
                                                                     {
                                                                         Stairs.RemoveLowestFloor(_Game);
                                                                     };
                                    _MainSplitContainer.Panel1.Controls.Add(RemoveLowestFloorButton);
                                }

                                break;
                            }
                        }
                    }
                    _MainSplitContainer.Panel1Collapsed = _SelectedObject == null;
                }
            }
        }

        private void _OnDrawingBoardMouseUp(Object Sender, MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == MouseButtons.Right)
            {
                Debug.Assert(_DragPoint != null);
                Debug.Assert(_Dragged != null);
                if(_Dragged == false)
                {
                    if(_SelectedObject != null)
                    {
                        _UnselectObject();
                    }
                    else
                    {
                        _UncheckAllToolButtons();
                    }
                }
                _DragPoint = null;
                _Dragged = null;
            }
        }

        private Color _MixToWhite(Color Color, Double Fraction)
        {
            return Color.FromArgb((Color.R + (255 - Color.R) * Fraction).GetTruncatedAsInt32(), (Color.G + (255 - Color.G) * Fraction).GetTruncatedAsInt32(), (Color.B + (255 - Color.B) * Fraction).GetTruncatedAsInt32());
        }

        private void _OnDrawingBoardPaint(Object Sender, PaintEventArgs EventArguments)
        {
            for(var Row = _Game.LowestFloor; Row < _Game.HighestFloor; ++Row)
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
            EventArguments.Graphics.FillRectangle(new SolidBrush(Data.GroundColor), 0, _GetDrawingY(0).GetNearestInt32(), _DrawingBoard.Width, _DrawingBoard.Height);
            foreach(var Building in _Game.Buildings)
            {
                _DrawRectangle(EventArguments.Graphics, Building.GetVisualRectangle(), Building.BackgroundColor, Building.BorderColor);
                if(Building is Office == true)
                {
                    var Office = (Office)Building;

                    _DrawRectangle(EventArguments.Graphics, Office.FirstLamp.GetVisualRectangle(), Office.FirstLamp.BackgroundColor, Color.Black);
                    _DrawRectangle(EventArguments.Graphics, Office.SecondLamp.GetVisualRectangle(), Office.SecondLamp.BackgroundColor, Color.Black);
                    _DrawRectangle(EventArguments.Graphics, Office.ThirdLamp.GetVisualRectangle(), Office.ThirdLamp.BackgroundColor, Color.Black);
                }
            }
            foreach(var Person in _Game.Persons)
            {
                if(Person.IsHidden() == false)
                {
                    _DrawRectangle(EventArguments.Graphics, Person.GetVisualRectangle(), _MixToWhite(Person.BackgroundColor, Person.GetActionFraction()), _MixToWhite(Person.BorderColor, Person.GetAnimationFraction()));
                }
            }
            foreach(var Office in _Game.Offices)
            {
                // first desk
                var PersonAtDeskColor = Color.White;
                var PersonColor = Color.White;
                var ComputerColor = Data.ComputerBackgroundColor;

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
                if(Office.FirstDesk.Computer.IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.FirstDesk.GetY() + (Office.FirstDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.Computer.GetRectangle(), ComputerColor, Data.ComputerBorderColor);
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
                if(Office.SecondDesk.Computer.IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.SecondDesk.GetY() + (Office.SecondDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.Computer.GetRectangle(), ComputerColor, Data.ComputerBorderColor);
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
                if(Office.ThirdDesk.Computer.IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.ThirdDesk.GetY() + (Office.ThirdDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.Computer.GetRectangle(), ComputerColor, Data.ComputerBorderColor);
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
                if(Office.FourthDesk.Computer.IsBroken() == true)
                {
                    ComputerColor = Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetRectangle(), Data.DeskBackgroundColor, Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - Data.PersonTagWidth) / 2.0f, Office.FourthDesk.GetY() + (Office.FourthDesk.GetHeight() - Data.PersonTagHeight) / 2.0f, Data.PersonTagWidth, Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.Computer.GetRectangle(), ComputerColor, Data.ComputerBorderColor);
                // cat
                if(Office.Cat != null)
                {
                    _DrawEllipse(EventArguments.Graphics, Office.Cat.GetVisualRectangle(), Office.Cat.BackgroundColor, Office.Cat.BorderColor);
                }
            }
            using(var FloatingTextFont = new Font("Arial", 16.0f))
            {
                foreach(var FloatingText in _FloatingTexts)
                {
                    _DrawFloatingText(EventArguments.Graphics, FloatingTextFont, FloatingText);
                }
            }
            if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
            {
                if(_EntityPrototype.DrawType == DrawType.Rectangle)
                {
                    _DrawRectangle(EventArguments.Graphics, _EntityPrototype.Rectangle, Color.FromArgb(150, _EntityPrototype.BackgroundColor), Color.FromArgb(150, _EntityPrototype.BorderColor));
                }
                else
                {
                    Debug.Assert(_EntityPrototype.DrawType == DrawType.Ellipse);
                    _DrawEllipse(EventArguments.Graphics, _EntityPrototype.Rectangle, Color.FromArgb(150, _EntityPrototype.BackgroundColor), Color.FromArgb(150, _EntityPrototype.BorderColor));
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

        private void _DrawFloatingText(Graphics Graphics, Font Font, FloatingText FloatingText)
        {
            var DrawingLocation = _GetDrawingLocation(FloatingText.Origin) + FloatingText.Offset;
            var Format = new StringFormat();

            Format.Alignment = StringAlignment.Center;
            Graphics.DrawString(FloatingText.Text, Font, new SolidBrush(FloatingText.Color), DrawingLocation.X.ToSingle(), DrawingLocation.Y.ToSingle(), Format);
        }

        #region Coordinate system transformations: Game -> Draw
        private Double _GetDrawingHeight(Double GamingHeight)
        {
            return GamingHeight * Data.BlockHeight * _Zoom;
        }

        private Double _GetDrawingWidth(Double GamingWidth)
        {
            return GamingWidth * Data.BlockWidth * _Zoom;
        }

        private Double _GetDrawingY(Double GamingY)
        {
            return (_DrawingBoard.Height.ToDouble() / 2.0) - (GamingY - _CameraPosition.Y) * Data.BlockHeight * _Zoom;
        }

        private Double _GetDrawingX(Double GamingX)
        {
            return _DrawingBoard.Width.ToDouble() / 2.0 + (GamingX - _CameraPosition.X) * Data.BlockWidth * _Zoom;
        }

        private Rectangle _GetDrawingRectangle(RectangleF GamingRectangle)
        {
            var DrawingPoint = _GetDrawingLocation(GamingRectangle.Location);
            var DrawingSize = _GetDrawingSize(GamingRectangle.Size);

            return new Rectangle(DrawingPoint.X, DrawingPoint.Y - DrawingSize.Height, DrawingSize.Width, DrawingSize.Height);
        }

        private Rectangle _GetDrawingRectangle(Single GamingX, Single GamingY, Single GamingWidth, Single GamingHeight)
        {
            var DrawingHeight = _GetDrawingHeight(GamingHeight).GetNearestInt32();

            return new Rectangle(_GetDrawingX(GamingX).GetNearestInt32(), _GetDrawingY(GamingY).GetNearestInt32() - DrawingHeight, _GetDrawingWidth(GamingWidth).GetNearestInt32(), DrawingHeight);
        }

        private Size _GetDrawingSize(SizeF GamingSize)
        {
            return new Size(_GetDrawingWidth(GamingSize.Width).GetNearestInt32(), _GetDrawingHeight(GamingSize.Height).GetNearestInt32());
        }

        private Point _GetDrawingLocation(PointF GamingLocation)
        {
            return new Point(_GetDrawingX(GamingLocation.X).GetNearestInt32(), _GetDrawingY(GamingLocation.Y).GetNearestInt32());
        }

        private Vector2 _GetDrawingLocation(Vector2 GamingLocation)
        {
            return new Vector2(_GetDrawingX(GamingLocation.X), _GetDrawingY(GamingLocation.Y));
        }
        #endregion

        #region Coordinate system transformations: Draw -> Game
        private Double _GetGamingHeight(Double DrawingHeight)
        {
            return -DrawingHeight / Data.BlockHeight / _Zoom;
        }

        private Double _GetGamingWidth(Double DrawingWidth)
        {
            return DrawingWidth / Data.BlockWidth / _Zoom;
        }

        private Double _GetGamingX(Double DrawingX)
        {
            return _CameraPosition.X + (DrawingX - _DrawingBoard.Width.ToDouble() / 2.0) / Data.BlockWidth / _Zoom;
        }

        private Double _GetGamingY(Double DrawingY)
        {
            return _CameraPosition.Y + (_DrawingBoard.Height.ToDouble() / 2.0 - DrawingY) / Data.BlockHeight / _Zoom;
        }

        private Vector2 _GetGamingLocation(Point DrawingLocation)
        {
            return new Vector2(_GetGamingX(DrawingLocation.X), _GetGamingY(DrawingLocation.Y));
        }
        #endregion

        private void _OnMainWindowResized(Object Sender, EventArgs EventArguments)
        {
            _DrawingBoard.Invalidate();
        }

        private void _OnTimerTicked(Object Sender, EventArgs EventArguments)
        {
            var Now = DateTime.Now;
            var Seconds = (Now - _LastTick).TotalSeconds;

            if((Seconds > 0.0) && (Seconds < 0.05))
            {
                _CameraPosition.X += _CameraVelocity.X * Seconds;
                _CameraPosition.Y += _CameraVelocity.Y * Seconds;
                _Game.Update(Data.GameMinutesPerSecond * Seconds);

                var Index = 0;

                while(Index < _FloatingTexts.Count)
                {
                    _FloatingTexts[Index].Timeout -= Seconds;
                    if(_FloatingTexts[Index].Timeout > 0.0)
                    {
                        _FloatingTexts[Index].SetOffset(new Vector2(_FloatingTexts[Index].Offset.X, _FloatingTexts[Index].Offset.Y - Seconds * Data.FloatingTextSpeed));
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
                    _PlaceCatButton.Text = $"Cat ({_Game.GetCatStock()})";
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
                if(_WPressed == false)
                {
                    _CameraVelocity.Y += 500.0 / Data.BlockHeight;
                    _WPressed = true;
                }
            }
            else if(EventArguments.KeyCode == Keys.A)
            {
                if(_APressed == false)
                {
                    _CameraVelocity.X -= 500.0 / Data.BlockWidth;
                    _APressed = true;
                }
            }
            else if(EventArguments.KeyCode == Keys.S)
            {
                if(_SPressed == false)
                {
                    _CameraVelocity.Y -= 500.0 / Data.BlockHeight;
                    _SPressed = true;
                }
            }
            else if(EventArguments.KeyCode == Keys.D)
            {
                if(_DPressed == false)
                {
                    _CameraVelocity.X += 500.0 / Data.BlockWidth;
                    _DPressed = true;
                }
            }
            else if(EventArguments.KeyCode == Keys.Escape)
            {
                if(_SelectedObject != null)
                {
                    _UnselectObject();
                }
                else
                {
                    _UncheckAllToolButtons();
                }
            }
        }

        private void _DrawingBoardKeyUp(Object Sender, KeyEventArgs EventArguments)
        {
            if(EventArguments.KeyCode == Keys.W)
            {
                _CameraVelocity.Y -= 500.0 / Data.BlockHeight;
                _WPressed = false;
            }
            else if(EventArguments.KeyCode == Keys.A)
            {
                _CameraVelocity.X += 500.0 / Data.BlockWidth;
                _APressed = false;
            }
            else if(EventArguments.KeyCode == Keys.S)
            {
                _CameraVelocity.Y += 500.0 / Data.BlockHeight;
                _SPressed = false;
            }
            else if(EventArguments.KeyCode == Keys.D)
            {
                _CameraVelocity.X -= 500.0 / Data.BlockWidth;
                _DPressed = false;
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
            _UncheckAllToolButtons();
            // places the camera closely above ground with half a block of ground visible
            _CameraPosition = new Vector2(0.0, _DrawingBoard.Height / Data.BlockHeight / 2.0 - 0.5);
            _CameraVelocity = new Vector2(0.0, 0.0);
            _FloatingTexts.Clear();
            _Game.OnEarnMoney += delegate(UInt64 Cents, Vector2 Location)
                                 {
                                     var FloatingText = new FloatingText();

                                     FloatingText.SetColor(Data.EarnMoneyFloatingTextColor);
                                     FloatingText.SetOffset(new Vector2(0.0, 0.0));
                                     FloatingText.SetOrigin(Location);
                                     FloatingText.SetText(_Game.GetMoneyString(Cents));
                                     FloatingText.Timeout = 1.2;
                                     _FloatingTexts.Add(FloatingText);
                                 };
            _Game.OnSpendMoney += delegate(UInt64 Cents, Vector2 Location)
                                  {
                                      var FloatingText = new FloatingText();

                                      FloatingText.SetColor(Data.SpendMoneyFloatingTextColor);
                                      FloatingText.SetOffset(new Vector2(0.0, 0.0));
                                      FloatingText.SetOrigin(Location);
                                      FloatingText.SetText(_Game.GetMoneyString(Cents));
                                      FloatingText.Timeout = 1.2;
                                      _FloatingTexts.Add(FloatingText);
                                  };
            _EntityPrototype = null;
            _DragPoint = null;
            _LastTick = DateTime.MinValue;
            _MoveButton = null;
            _MovePerson = null;
            _Zoom = 1.0f;
        }

        private void _UnselectObject()
        {
            if(_SelectedObject != null)
            {
                _SelectedObject = null;
                _MainSplitContainer.Panel1Collapsed = true;
            }
        }
    }
}

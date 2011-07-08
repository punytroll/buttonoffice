namespace ButtonOffice
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private ButtonOffice.Game _Game;
        private System.Collections.Generic.List<System.Windows.Forms.ToolStripButton> _ToolButtons;
        private System.Nullable<System.Drawing.Point> _DragPoint;
        private System.Drawing.Point _DrawingOffset;
        private ButtonOffice.EntityPrototype _EntityPrototype;
        private System.Windows.Forms.Timer _Timer;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.ToolStrip _MainTools;
        private System.Windows.Forms.ToolStripButton _BuildOfficeButton;
        private System.Windows.Forms.ToolStripButton _HireWorkerButton;
        private System.Windows.Forms.StatusStrip _StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel _TimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel _MoneyLabel;
        private System.Windows.Forms.ToolStripStatusLabel _EmployeesLabel;
        private System.DateTime _LastTick;
        private System.DateTime _NextMinute;
        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private DrawingBoard _DrawingBoard;
        private System.Windows.Forms.ToolStripStatusLabel _PositionLabel;
        private System.Windows.Forms.ToolStripButton _HireITTechButton;
        private ButtonOffice.Person _SelectedPerson;
        private System.Windows.Forms.ToolStripButton _HireJanitorButton;
        private ButtonOffice.Office _SelectedOffice;
    
        public MainWindow()
        {
            _Game = new Game();
            _EntityPrototype = null;
            _DragPoint = new System.Nullable<System.Drawing.Point>();
            _DrawingOffset = new System.Drawing.Point(-ButtonOffice.Data.WorldBlockWidth * ButtonOffice.Data.BlockWidth / 2, 2 * ButtonOffice.Data.BlockHeight);
            _NextMinute = System.DateTime.Now;
            _LastTick = System.DateTime.Now;
            InitializeComponent();
            _ToolButtons = new System.Collections.Generic.List<System.Windows.Forms.ToolStripButton>();
            _ToolButtons.Add(_HireITTechButton);
            _ToolButtons.Add(_HireJanitorButton);
            _ToolButtons.Add(_HireWorkerButton);
            _ToolButtons.Add(_BuildOfficeButton);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusBar = new System.Windows.Forms.StatusStrip();
            this._TimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MoneyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._EmployeesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this._DrawingBoard = new ButtonOffice.DrawingBoard();
            this._MainTools = new System.Windows.Forms.ToolStrip();
            this._BuildOfficeButton = new System.Windows.Forms.ToolStripButton();
            this._HireWorkerButton = new System.Windows.Forms.ToolStripButton();
            this._HireITTechButton = new System.Windows.Forms.ToolStripButton();
            this._HireJanitorButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._StatusBar.SuspendLayout();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            this._MainTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Timer
            // 
            this._Timer.Enabled = true;
            this._Timer.Interval = 10;
            this._Timer.Tick += new System.EventHandler(this._OnTimerTicked);
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.BottomToolStripPanel
            // 
            this._ToolStripContainer.BottomToolStripPanel.Controls.Add(this._StatusBar);
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._MainSplitContainer);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(910, 481);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(910, 528);
            this._ToolStripContainer.TabIndex = 3;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._MainTools);
            // 
            // _StatusBar
            // 
            this._StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this._StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._TimeLabel,
            this._MoneyLabel,
            this._EmployeesLabel,
            this._PositionLabel});
            this._StatusBar.Location = new System.Drawing.Point(0, 0);
            this._StatusBar.Name = "_StatusBar";
            this._StatusBar.Size = new System.Drawing.Size(910, 22);
            this._StatusBar.TabIndex = 0;
            // 
            // _TimeLabel
            // 
            this._TimeLabel.Name = "_TimeLabel";
            this._TimeLabel.Size = new System.Drawing.Size(70, 17);
            this._TimeLabel.Text = "Day && Time";
            // 
            // _MoneyLabel
            // 
            this._MoneyLabel.Name = "_MoneyLabel";
            this._MoneyLabel.Size = new System.Drawing.Size(44, 17);
            this._MoneyLabel.Text = "Money";
            // 
            // _EmployeesLabel
            // 
            this._EmployeesLabel.Name = "_EmployeesLabel";
            this._EmployeesLabel.Size = new System.Drawing.Size(64, 17);
            this._EmployeesLabel.Text = "Employees";
            // 
            // _PositionLabel
            // 
            this._PositionLabel.Name = "_PositionLabel";
            this._PositionLabel.Size = new System.Drawing.Size(50, 17);
            this._PositionLabel.Text = "Position";
            // 
            // _MainSplitContainer
            // 
            this._MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._MainSplitContainer.Name = "_MainSplitContainer";
            // 
            // _MainSplitContainer.Panel1
            // 
            this._MainSplitContainer.Panel1.Controls.Add(this._DrawingBoard);
            this._MainSplitContainer.Panel2Collapsed = true;
            this._MainSplitContainer.Size = new System.Drawing.Size(910, 481);
            this._MainSplitContainer.SplitterDistance = 750;
            this._MainSplitContainer.TabIndex = 3;
            // 
            // _DrawingBoard
            // 
            this._DrawingBoard.BackColor = System.Drawing.SystemColors.ControlDark;
            this._DrawingBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DrawingBoard.Location = new System.Drawing.Point(0, 0);
            this._DrawingBoard.Name = "_DrawingBoard";
            this._DrawingBoard.Size = new System.Drawing.Size(910, 481);
            this._DrawingBoard.TabIndex = 4;
            this._DrawingBoard.Paint += new System.Windows.Forms.PaintEventHandler(this._OnDrawingBoardPaint);
            this._DrawingBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnDrawingBoardMouseMoved);
            this._DrawingBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnDrawingBoardMouseDown);
            this._DrawingBoard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._OnDrawingBoardKeyPressed);
            this._DrawingBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnDrawingBoardMouseUp);
            // 
            // _MainTools
            // 
            this._MainTools.Dock = System.Windows.Forms.DockStyle.None;
            this._MainTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._BuildOfficeButton,
            this._HireWorkerButton,
            this._HireITTechButton,
            this._HireJanitorButton});
            this._MainTools.Location = new System.Drawing.Point(3, 0);
            this._MainTools.Name = "_MainTools";
            this._MainTools.Padding = new System.Windows.Forms.Padding(0);
            this._MainTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._MainTools.Size = new System.Drawing.Size(230, 25);
            this._MainTools.TabIndex = 1;
            this._MainTools.Text = "MainTools";
            // 
            // _BuildOfficeButton
            // 
            this._BuildOfficeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._BuildOfficeButton.Name = "_BuildOfficeButton";
            this._BuildOfficeButton.Size = new System.Drawing.Size(43, 22);
            this._BuildOfficeButton.Text = "Office";
            this._BuildOfficeButton.ToolTipText = "Build office";
            this._BuildOfficeButton.CheckedChanged += new System.EventHandler(this._OnBuildOfficeButtonCheckedChanged);
            this._BuildOfficeButton.Click += new System.EventHandler(this._OnBuildOfficeButtonClicked);
            // 
            // _HireWorkerButton
            // 
            this._HireWorkerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireWorkerButton.Image = ((System.Drawing.Image)(resources.GetObject("_HireWorkerButton.Image")));
            this._HireWorkerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._HireWorkerButton.Name = "_HireWorkerButton";
            this._HireWorkerButton.Size = new System.Drawing.Size(49, 22);
            this._HireWorkerButton.Text = "Worker";
            this._HireWorkerButton.CheckedChanged += new System.EventHandler(this._OnHireWorkerButtonCheckedChanged);
            this._HireWorkerButton.Click += new System.EventHandler(this._OnHireWorkerButtonClicked);
            // 
            // _HireITTechButton
            // 
            this._HireITTechButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireITTechButton.Image = ((System.Drawing.Image)(resources.GetObject("_HireITTechButton.Image")));
            this._HireITTechButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._HireITTechButton.Name = "_HireITTechButton";
            this._HireITTechButton.Size = new System.Drawing.Size(50, 22);
            this._HireITTechButton.Text = "IT Tech";
            this._HireITTechButton.CheckedChanged += new System.EventHandler(this._OnHireITTechButtonCheckedChanged);
            this._HireITTechButton.Click += new System.EventHandler(this._OnHireITTechButtonClicked);
            // 
            // _HireJanitorButton
            // 
            this._HireJanitorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireJanitorButton.Image = ((System.Drawing.Image)(resources.GetObject("_HireJanitorButton.Image")));
            this._HireJanitorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._HireJanitorButton.Name = "_HireJanitorButton";
            this._HireJanitorButton.Size = new System.Drawing.Size(46, 22);
            this._HireJanitorButton.Text = "Janitor";
            this._HireJanitorButton.CheckedChanged += new System.EventHandler(this._OnHireJanitorButtonCheckedChanged);
            this._HireJanitorButton.Click += new System.EventHandler(this._OnHireJanitorButtonClicked);
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(910, 528);
            this.Controls.Add(this._ToolStripContainer);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this._OnMainWindowLoaded);
            this.Resize += new System.EventHandler(this._OnMainWindowResized);
            this._ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._StatusBar.ResumeLayout(false);
            this._StatusBar.PerformLayout();
            this._MainSplitContainer.Panel1.ResumeLayout(false);
            this._MainSplitContainer.ResumeLayout(false);
            this._MainTools.ResumeLayout(false);
            this._MainTools.PerformLayout();
            this.ResumeLayout(false);

        }

        private void _ToggleOneToolButton(System.Collections.Generic.List<System.Windows.Forms.ToolStripButton> Buttons, System.Windows.Forms.ToolStripButton ToggleButton)
        {
            if(ToggleButton.Checked == true)
            {
                ToggleButton.Checked = false;
            }
            else
            {
                // uncheck all other buttons (if necessary)
                foreach(System.Windows.Forms.ToolStripButton Button in Buttons)
                {
                    if(Button != ToggleButton)
                    {
                        if(Button.Checked == true)
                        {
                            Button.Checked = false;
                        }
                    }
                }
                // check the new button
                ToggleButton.Checked = true;
            }
        }

        private void _OnBuildOfficeButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_ToolButtons, _BuildOfficeButton);
        }

        private void _OnBuildOfficeButtonCheckedChanged(System.Object Sender, System.EventArgs EventArguments)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            if(_BuildOfficeButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.Office);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.OfficeBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.OfficeBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.OfficeBlockHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.OfficeBlockWidth);
            }
        }

        private void _OnHireITTechButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_ToolButtons, _HireITTechButton);
        }

        private void _OnHireITTechButtonCheckedChanged(System.Object Sender, System.EventArgs EventArguments)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            else
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.ITTech);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.ITTechBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.ITTechBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.PersonHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.PersonWidth);
            }
        }

        private void _OnHireJanitorButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_ToolButtons, _HireJanitorButton);
        }

        private void _OnHireJanitorButtonCheckedChanged(System.Object Sender, System.EventArgs EventArguments)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            if(_HireJanitorButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.Janitor);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.JanitorBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.JanitorBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.PersonHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.PersonWidth);
            }
        }

        private void _OnHireWorkerButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_ToolButtons, _HireWorkerButton);
        }

        private void _OnHireWorkerButtonCheckedChanged(System.Object Sender, System.EventArgs EventArguments)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            if(_HireWorkerButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.Worker);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.WorkerBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.WorkerBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.PersonHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.PersonWidth);
            }
        }

        private void _OnDrawingBoardMouseMoved(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
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
                    _EntityPrototype.SetLocationFromGamingLocation(_GetGamingLocation(EventArguments.Location).GetTruncated());
                    _DrawingBoard.Invalidate();
                }
            }

            System.Drawing.PointF GamingLocation = _GetGamingLocation(EventArguments.Location);

            _PositionLabel.Text = "Location: " + GamingLocation.X.GetIntegerAsInt32().ToString() + " / " + GamingLocation.Y.GetIntegerAsInt32().ToString();
        }

        private void _OnDrawingBoardMouseDown(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _DragPoint = EventArguments.Location;
            }
            else if(EventArguments.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
                {
                    if(_EntityPrototype.Type == ButtonOffice.Type.Office)
                    {
                        if(_Game.BuildOffice(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _BuildOfficeButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Worker)
                    {
                        if(_Game.HireWorker(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireWorkerButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.ITTech)
                    {
                        if(_Game.HireITTech(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireITTechButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Janitor)
                    {
                        if(_Game.HireJanitor(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireJanitorButton.Checked = false;
                            }
                        }
                    }
                }
                else
                {
                    System.Drawing.PointF GamingLocation = _GetGamingLocation(EventArguments.Location);
                    System.Boolean Selected = false;

                    _SelectedOffice = null;
                    _SelectedPerson = null;
                    if(Selected == false)
                    {
                        foreach(Person Person in _Game.Persons)
                        {
                            if(Person.GetRectangle().Contains(GamingLocation) == true)
                            {
                                _SelectedPerson = Person;
                                Selected = true;
                                _MainSplitContainer.Panel2.Controls.Clear();

                                System.Windows.Forms.Label TypeLabel = new System.Windows.Forms.Label();

                                TypeLabel.Location = new System.Drawing.Point(10, 20);
                                TypeLabel.Size = new System.Drawing.Size(100, 20);
                                TypeLabel.Text = Person.Type.ToString();
                                _MainSplitContainer.Panel2.Controls.Add(TypeLabel);

                                System.Windows.Forms.Label NameCaptionLabel = new System.Windows.Forms.Label();

                                NameCaptionLabel.Location = new System.Drawing.Point(10, 40);
                                NameCaptionLabel.Size = new System.Drawing.Size(100, 20);
                                NameCaptionLabel.Text = "Name:";
                                _MainSplitContainer.Panel2.Controls.Add(NameCaptionLabel);

                                System.Windows.Forms.Label NameLabel = new System.Windows.Forms.Label();

                                NameLabel.Location = new System.Drawing.Point(110, 40);
                                NameLabel.Size = new System.Drawing.Size(100, 20);
                                NameLabel.Text = Person.Name;
                                _MainSplitContainer.Panel2.Controls.Add(NameLabel);

                                break;
                            }
                        }
                    }
                    if(Selected == false)
                    {
                        foreach(ButtonOffice.Office Office in _Game.Offices)
                        {
                            if(Office.GetRectangle().Contains(GamingLocation) == true)
                            {
                                _SelectedOffice = Office;
                                Selected = true;
                                _MainSplitContainer.Panel2.Controls.Clear();

                                System.Windows.Forms.Label NameCaptionLabel = new System.Windows.Forms.Label();

                                NameCaptionLabel.Location = new System.Drawing.Point(10, 20);
                                NameCaptionLabel.Size = new System.Drawing.Size(100, 20);
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

        private void _OnDrawingBoardMouseUp(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            _DragPoint = new System.Nullable<System.Drawing.Point>();
        }

        private System.Drawing.Color _MixToWhite(System.Drawing.Color Color, System.Single Fraction)
        {
            return System.Drawing.Color.FromArgb((Color.R + (255 - Color.R) * Fraction).GetIntegerAsInt32(), (Color.G + (255 - Color.G) * Fraction).GetIntegerAsInt32(), (Color.B + (255 - Color.B) * Fraction).GetIntegerAsInt32());
        }

        private void _OnDrawingBoardPaint(System.Object Sender, System.Windows.Forms.PaintEventArgs EventArguments)
        {
            EventArguments.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.BurlyWood), 0, _GetDrawingY(0), _DrawingBoard.Width, _DrawingBoard.Height);
            foreach(Office Office in _Game.Offices)
            {
                _DrawRectangle(EventArguments.Graphics, Office.GetRectangle(), ButtonOffice.Data.OfficeBackgroundColor, ButtonOffice.Data.OfficeBorderColor);

                System.Drawing.Color LampColor;

                LampColor = System.Drawing.Color.Yellow;
                if(Office.FirstLamp.IsBroken() == true)
                {
                    LampColor = System.Drawing.Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstLamp.GetRectangle(), LampColor, System.Drawing.Color.Black);
                LampColor = System.Drawing.Color.Yellow;
                if(Office.SecondLamp.IsBroken() == true)
                {
                    LampColor = System.Drawing.Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondLamp.GetRectangle(), LampColor, System.Drawing.Color.Black);
                LampColor = System.Drawing.Color.Yellow;
                if(Office.ThirdLamp.IsBroken() == true)
                {
                    LampColor = System.Drawing.Color.Gray;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdLamp.GetRectangle(), LampColor, System.Drawing.Color.Black);
            }
            foreach(Person Person in _Game.Persons)
            {
                if(Person.IsHidden() == false)
                {
                    _DrawRectangle(EventArguments.Graphics, Person.GetRectangle(), _MixToWhite(Person.BackgroundColor, Person.GetAnimationFraction()), Person.BorderColor);
                }
            }
            foreach(ButtonOffice.Office Office in _Game.Offices)
            {
                System.Drawing.Color PersonColor;
                System.Drawing.Color ComputerColor;

                // first desk
                PersonColor = System.Drawing.Color.White;
                ComputerColor = System.Drawing.Color.Black;
                if(Office.FirstDesk.Person != null)
                {
                    PersonColor = Office.FirstDesk.Person.BackgroundColor;
                }
                if(Office.FirstDesk.IsComputerBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.FirstDesk.GetY() + (Office.FirstDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.FirstDesk.GetY() + Office.FirstDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, System.Drawing.Color.Black);
                // second desk
                PersonColor = System.Drawing.Color.White;
                ComputerColor = System.Drawing.Color.Black;
                if(Office.SecondDesk.Person != null)
                {
                    PersonColor = Office.SecondDesk.Person.BackgroundColor;
                }
                if(Office.SecondDesk.IsComputerBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.SecondDesk.GetY() + (Office.SecondDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.SecondDesk.GetY() + Office.SecondDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, System.Drawing.Color.Black);
                // third desk
                PersonColor = System.Drawing.Color.White;
                ComputerColor = System.Drawing.Color.Black;
                if(Office.ThirdDesk.Person != null)
                {
                    PersonColor = Office.ThirdDesk.Person.BackgroundColor;
                }
                if(Office.ThirdDesk.IsComputerBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.ThirdDesk.GetY() + (Office.ThirdDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.ThirdDesk.GetY() + Office.ThirdDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, System.Drawing.Color.Black);
                // fourth desk
                PersonColor = System.Drawing.Color.White;
                ComputerColor = System.Drawing.Color.Black;
                if(Office.FourthDesk.Person != null)
                {
                    PersonColor = Office.FourthDesk.Person.BackgroundColor;
                }
                if(Office.FourthDesk.IsComputerBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.FourthDesk.GetY() + (Office.FourthDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.FourthDesk.GetY() + Office.FourthDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, System.Drawing.Color.Black);
            }
            if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
            {
                _DrawRectangle(EventArguments.Graphics, _EntityPrototype.Rectangle, System.Drawing.Color.FromArgb(150, _EntityPrototype.BackgroundColor), System.Drawing.Color.FromArgb(150, _EntityPrototype.BorderColor));
            }
        }

        private void _DrawRectangle(System.Drawing.Graphics Graphics, System.Drawing.RectangleF GameRectangle, System.Drawing.Color BackgroundColor, System.Drawing.Color BorderColor)
        {
            System.Drawing.Rectangle BackgroundRectangle = _GetDrawingRectangle(GameRectangle);

            Graphics.FillRectangle(new System.Drawing.SolidBrush(BackgroundColor), BackgroundRectangle);

            System.Drawing.Rectangle ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawRectangle(new System.Drawing.Pen(BorderColor), ForegroundRectangle);
        }

        private void _DrawRectangle(System.Drawing.Graphics Graphics, System.Single GameX, System.Single GameY, System.Single GameWidth, System.Single GameHeight, System.Drawing.Color BackgroundColor, System.Drawing.Color BorderColor)
        {
            System.Drawing.Rectangle BackgroundRectangle = _GetDrawingRectangle(GameX, GameY, GameWidth, GameHeight);

            Graphics.FillRectangle(new System.Drawing.SolidBrush(BackgroundColor), BackgroundRectangle);

            System.Drawing.Rectangle ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawRectangle(new System.Drawing.Pen(BorderColor), ForegroundRectangle);
        }

        #region Coordinate system transformations: Game -> Draw
        private System.Int32 _GetDrawingHeight(System.Single GamingHeight)
        {
            return (GamingHeight * ButtonOffice.Data.BlockHeight.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingWidth(System.Single GamingWidth)
        {
            return (GamingWidth * ButtonOffice.Data.BlockWidth.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingY(System.Single GamingY)
        {
            return (_DrawingBoard.Height.ToSingle() - (GamingY * ButtonOffice.Data.BlockHeight.ToSingle()) - _DrawingOffset.Y.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingX(System.Single GamingX)
        {
            return (GamingX * ButtonOffice.Data.BlockWidth.ToSingle() + _DrawingOffset.X.ToSingle()).GetIntegerAsInt32();
        }

        private System.Drawing.Rectangle _GetDrawingRectangle(System.Drawing.RectangleF GamingRectangle)
        {
            System.Drawing.Point DrawingPoint = _GetDrawingPoint(GamingRectangle.Location);
            System.Drawing.Size DrawingSize = _GetDrawingSize(GamingRectangle.Size);

            return new System.Drawing.Rectangle(DrawingPoint.X, DrawingPoint.Y - DrawingSize.Height, DrawingSize.Width, DrawingSize.Height);
        }

        private System.Drawing.Rectangle _GetDrawingRectangle(System.Single GamingX, System.Single GamingY, System.Single GamingWidth, System.Single GamingHeight)
        {
            System.Int32 DrawingHeight = _GetDrawingHeight(GamingHeight);

            return new System.Drawing.Rectangle(_GetDrawingX(GamingX), _GetDrawingY(GamingY) - DrawingHeight, _GetDrawingWidth(GamingWidth), DrawingHeight);
        }

        private System.Drawing.Size _GetDrawingSize(System.Drawing.SizeF GamingSize)
        {
            return new System.Drawing.Size(_GetDrawingWidth(GamingSize.Width), _GetDrawingHeight(GamingSize.Height));
        }

        private System.Drawing.Point _GetDrawingPoint(System.Drawing.PointF GamingPoint)
        {
            return new System.Drawing.Point(_GetDrawingX(GamingPoint.X), _GetDrawingY(GamingPoint.Y));
        }
        #endregion

        #region Coordinate system transformations: Draw -> Game
        private System.Single _GetGamingX(System.Int32 DrawingX)
        {
            return (DrawingX - _DrawingOffset.X).ToSingle() / ButtonOffice.Data.BlockWidth.ToSingle();
        }

        private System.Single _GetGamingY(System.Int32 DrawingY)
        {
            return (_DrawingBoard.Height - DrawingY - _DrawingOffset.Y).ToSingle() / ButtonOffice.Data.BlockHeight.ToSingle();
        }

        private System.Drawing.PointF _GetGamingLocation(System.Drawing.Point DrawingLocation)
        {
            return new System.Drawing.PointF(_GetGamingX(DrawingLocation.X), _GetGamingY(DrawingLocation.Y));
        }
        #endregion

        private void _OnMainWindowResized(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.Invalidate();
        }

        private void _OnTimerTicked(System.Object Sender, System.EventArgs EventArguments)
        {
            System.DateTime Now = System.DateTime.Now;

            _Game.Move(ButtonOffice.Data.GameMinutesPerSecond * (Now - _LastTick).TotalSeconds.ToSingle());
            _TimeLabel.Text = "Day && Time: " + new System.TimeSpan(_Game.GetDay().ToInt32(), 0, _Game.GetMinuteOfDay().ToInt32(), 0).ToString();
            _MoneyLabel.Text = "Money: " + _Game.GetEuros().ToString() + "." + _Game.GetCents().ToString("00") + "€";
            _EmployeesLabel.Text = "Employees: " + _Game.Persons.Count.ToString();
            _LastTick = Now;
            _DrawingBoard.Invalidate();
        }

        private void _OnMainWindowLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.BackColor = ButtonOffice.Data.BackgroundColor;
        }

        private void _OnDrawingBoardKeyPressed(System.Object Sender, System.Windows.Forms.KeyPressEventArgs EventArguments)
        {
            if(EventArguments.KeyChar == 'w')
            {
                _DrawingOffset.Y -= 50;
            }
            else if(EventArguments.KeyChar == 'a')
            {
                _DrawingOffset.X += 50;
            }
            else if(EventArguments.KeyChar == 's')
            {
                _DrawingOffset.Y += 50;
            }
            else if(EventArguments.KeyChar == 'd')
            {
                _DrawingOffset.X -= 50;
            }
        }
    }
}

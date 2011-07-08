namespace ButtonOffice
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Collections.Generic.List<System.Windows.Forms.ToolStripButton> _ToolButtons;
        private System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>> _BrokenThings;
        private System.Nullable<System.Drawing.Point> _DragPoint;
        private System.Drawing.Point _DrawingOffset;
        private ButtonOffice.EntityPrototype _EntityPrototype;
        private System.Collections.Generic.List<ButtonOffice.Office> _Offices;
        private System.Windows.Forms.Timer _Timer;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.ToolStrip _MainTools;
        private System.Windows.Forms.ToolStripButton _BuildOfficeButton;
        private System.Windows.Forms.ToolStripButton _HireWorkerButton;
        private System.Windows.Forms.StatusStrip _StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel _TimeLabel;
        private System.Collections.Generic.List<ButtonOffice.Person> _Persons;
        private System.Windows.Forms.ToolStripStatusLabel _MoneyLabel;
        private System.Windows.Forms.ToolStripStatusLabel _EmployeesLabel;
        private System.UInt64 _Minutes;
        private System.UInt64 _Cents;
        private System.DateTime _LastTick;
        private System.DateTime _NextMinute;
        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private DrawingBoard _DrawingBoard;
        private System.Collections.Generic.List<System.Collections.BitArray> _FreeSpace;
        private System.Windows.Forms.ToolStripStatusLabel _PositionLabel;
        private System.Windows.Forms.ToolStripButton _HireITTechButton;
        private System.Collections.Generic.List<System.Collections.BitArray> _WalkSpace;
        private ButtonOffice.Person _SelectedPerson;
        private System.Windows.Forms.ToolStripButton _HireJanitorButton;
        private ButtonOffice.Office _SelectedOffice;
    
        public MainWindow()
        {
            _BrokenThings = new System.Collections.Generic.Queue<System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>>();
            _EntityPrototype = null;
            _DragPoint = new System.Nullable<System.Drawing.Point>();
            _DrawingOffset = new System.Drawing.Point(-ButtonOffice.Data.WorldBlockWidth * ButtonOffice.Data.BlockWidth / 2, 2 * ButtonOffice.Data.BlockHeight);
            _Offices = new System.Collections.Generic.List<Office>();
            _NextMinute = System.DateTime.Now;
            _LastTick = System.DateTime.Now;
            _Persons = new System.Collections.Generic.List<Person>();
            _FreeSpace = new System.Collections.Generic.List<System.Collections.BitArray>();
            for(System.Int32 Index = 0; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                _FreeSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, true));
            }
            _WalkSpace = new System.Collections.Generic.List<System.Collections.BitArray>();
            _WalkSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, true));
            for(System.Int32 Index = 1; Index < ButtonOffice.Data.WorldBlockHeight; ++Index)
            {
                _WalkSpace.Add(new System.Collections.BitArray(ButtonOffice.Data.WorldBlockWidth, false));
            }
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
                _EntityPrototype.Height = ButtonOffice.Data.OfficeBlockHeight;
                _EntityPrototype.Width = ButtonOffice.Data.OfficeBlockWidth;
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
                _EntityPrototype.Height = ButtonOffice.Data.PersonHeight;
                _EntityPrototype.Width = ButtonOffice.Data.PersonWidth;
                _EntityPrototype.BorderColor = ButtonOffice.Data.ITTechBorderColor;
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
                _EntityPrototype.Height = ButtonOffice.Data.PersonHeight;
                _EntityPrototype.Width = ButtonOffice.Data.PersonWidth;
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
                _EntityPrototype.Height = ButtonOffice.Data.PersonHeight;
                _EntityPrototype.Width = ButtonOffice.Data.PersonWidth;
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
                    _EntityPrototype.SetLocationFromMouseLocation(_GetGameBlockCoordinates(EventArguments.Location));
                    _DrawingBoard.Invalidate();
                }
            }

            System.Drawing.Point GameBlockCoordinates = _GetGameBlockCoordinates(EventArguments.Location);

            _PositionLabel.Text = "Location: " + GameBlockCoordinates.X.ToString() + " / " + GameBlockCoordinates.Y.ToString();
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
                        System.Boolean BuildAllowed = true;

                        for(System.Int32 Column = 0; Column < _EntityPrototype.Width; ++Column)
                        {
                            BuildAllowed &= _FreeSpace[_EntityPrototype.Location.Y][_EntityPrototype.Location.X + Column];
                            if(_EntityPrototype.Location.Y > 0)
                            {
                                BuildAllowed &= !_FreeSpace[_EntityPrototype.Location.Y - 1][_EntityPrototype.Location.X + Column];
                            }
                        }
                        if((BuildAllowed == true) && (_Cents >= ButtonOffice.Data.OfficeBuildCost))
                        {
                            _Cents -= ButtonOffice.Data.OfficeBuildCost;
                            for(System.Int32 Column = 0; Column < _EntityPrototype.Width; ++Column)
                            {
                                _FreeSpace[_EntityPrototype.Location.Y][_EntityPrototype.Location.X + Column] = false;
                            }
                            for(System.Int32 Column = 0; Column < _EntityPrototype.Width; ++Column)
                            {
                                _WalkSpace[_EntityPrototype.Location.Y][_EntityPrototype.Location.X + Column] = true;
                            }

                            Office Office = new Office();

                            Office.SetWidth(ButtonOffice.Data.OfficeBlockWidth);
                            Office.SetHeight(ButtonOffice.Data.OfficeBlockHeight);
                            Office.SetX(_EntityPrototype.Location.X);
                            Office.SetY(_EntityPrototype.Location.Y);

                            System.Random Random = new System.Random();

                            Office.FirstDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                            Office.SecondDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                            Office.ThirdDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                            Office.FourthDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));
                            Office.FirstLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                            Office.SecondLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                            Office.ThirdLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));
                            _Offices.Add(Office);
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _BuildOfficeButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Worker)
                    {
                        Office Office = _GetOffice(_GetGameBlockCoordinates(EventArguments.Location));

                        if((_Cents >= ButtonOffice.Data.WorkerHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
                        {
                            _Cents -= ButtonOffice.Data.WorkerHireCost;

                            ButtonOffice.Worker Worker = new ButtonOffice.Worker();
                            System.Random Random = new System.Random();

                            Worker.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;
                            Worker.WorkMinutes = ButtonOffice.Data.WorkerWorkMinutes;
                            _PlanNextWorkDay(Worker);
                            Worker.ActionState = ButtonOffice.ActionState.AtHome;
                            Worker.AnimationState = ButtonOffice.AnimationState.Hidden;
                            Worker.AnimationFraction = 0.0f;
                            Worker.BackgroundColor = _EntityPrototype.BackgroundColor;
                            Worker.BorderColor = _EntityPrototype.BorderColor;
                            Worker.Wage = ButtonOffice.Data.WorkerWage;
                            Worker.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                            Worker.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.5f));
                            if(new System.Random().NextDouble() < 0.5)
                            {
                                Worker.LivingSide = ButtonOffice.LivingSide.Left;
                                Worker.SetLocation(-10.0f, 0.0f);
                            }
                            else
                            {
                                Worker.LivingSide = ButtonOffice.LivingSide.Right;
                                Worker.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                            }

                            ButtonOffice.Desk Desk = Office.GetFreeDesk();

                            Desk.Person = Worker;
                            Worker.Desk = Desk;
                            _Persons.Add(Worker);
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireWorkerButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.ITTech)
                    {
                        Office Office = _GetOffice(_GetGameBlockCoordinates(EventArguments.Location));

                        if((_Cents >= ButtonOffice.Data.ITTechHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
                        {
                            _Cents -= ButtonOffice.Data.ITTechHireCost;

                            ButtonOffice.ITTech ITTech = new ButtonOffice.ITTech();
                            System.Random Random = new System.Random();

                            ITTech.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.ITTechStartMinute, 300) % 1440;
                            ITTech.WorkMinutes = ButtonOffice.Data.ITTechWorkMinutes;
                            _PlanNextWorkDay(ITTech);
                            ITTech.ActionState = ButtonOffice.ActionState.AtHome;
                            ITTech.AnimationState = ButtonOffice.AnimationState.Hidden;
                            ITTech.AnimationFraction = 0.0f;
                            ITTech.BackgroundColor = _EntityPrototype.BackgroundColor;
                            ITTech.BorderColor = _EntityPrototype.BorderColor;
                            ITTech.Wage = ButtonOffice.Data.ITTechWage;
                            ITTech.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                            ITTech.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.8f));
                            if(new System.Random().NextDouble() < 0.5)
                            {
                                ITTech.LivingSide = ButtonOffice.LivingSide.Left;
                                ITTech.SetLocation(-10.0f, 0.0f);
                            }
                            else
                            {
                                ITTech.LivingSide = ButtonOffice.LivingSide.Right;
                                ITTech.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                            }

                            ButtonOffice.Desk Desk = Office.GetFreeDesk();

                            Desk.Person = ITTech;
                            ITTech.Desk = Desk;
                            ITTech.SetX(Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f);
                            ITTech.SetY(Desk.GetY());
                            _Persons.Add(ITTech);
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireITTechButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Janitor)
                    {
                        Office Office = _GetOffice(_GetGameBlockCoordinates(EventArguments.Location));

                        if((_Cents >= ButtonOffice.Data.JanitorHireCost) && (Office != null) && (Office.HasFreeDesk() == true))
                        {
                            _Cents -= ButtonOffice.Data.JanitorHireCost;

                            ButtonOffice.Janitor Janitor = new ButtonOffice.Janitor();
                            System.Random Random = new System.Random();

                            Janitor.ArrivesAtDayMinute = Random.NextUInt32(ButtonOffice.Data.JanitorStartMinute, 300) % 1440;
                            Janitor.WorkMinutes = ButtonOffice.Data.JanitorWorkMinutes;
                            _PlanNextWorkDay(Janitor);
                            Janitor.ActionState = ButtonOffice.ActionState.AtHome;
                            Janitor.AnimationState = ButtonOffice.AnimationState.Hidden;
                            Janitor.AnimationFraction = 0.0f;
                            Janitor.BackgroundColor = _EntityPrototype.BackgroundColor;
                            Janitor.BorderColor = _EntityPrototype.BorderColor;
                            Janitor.Wage = ButtonOffice.Data.JanitorWage;
                            Janitor.SetHeight(Random.NextSingle(ButtonOffice.Data.PersonHeight, 0.3f));
                            Janitor.SetWidth(Random.NextSingle(ButtonOffice.Data.PersonWidth, 0.5f));
                            if(new System.Random().NextDouble() < 0.5)
                            {
                                Janitor.LivingSide = ButtonOffice.LivingSide.Left;
                                Janitor.SetLocation(-10.0f, 0.0f);
                            }
                            else
                            {
                                Janitor.LivingSide = ButtonOffice.LivingSide.Right;
                                Janitor.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                            }

                            ButtonOffice.Desk Desk = Office.GetFreeDesk();

                            Desk.Person = Janitor;
                            Janitor.Desk = Desk;
                            _Persons.Add(Janitor);
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireJanitorButton.Checked = false;
                            }
                        }
                    }
                }
                else
                {
                    System.Drawing.PointF GameCoordinates = _GetGameCoordinates(EventArguments.Location);
                    System.Boolean Selected = false;

                    _SelectedOffice = null;
                    _SelectedPerson = null;
                    if(Selected == false)
                    {
                        foreach(Person Person in _Persons)
                        {
                            if(Person.GetRectangle().Contains(GameCoordinates) == true)
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
                        foreach(ButtonOffice.Office Office in _Offices)
                        {
                            if(Office.GetRectangle().Contains(GameCoordinates) == true)
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
            foreach(Office Office in _Offices)
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
            foreach(Person Person in _Persons)
            {
                if(Person.AnimationState != ButtonOffice.AnimationState.Hidden)
                {
                    _DrawRectangle(EventArguments.Graphics, Person.GetRectangle(), _MixToWhite(Person.BackgroundColor, Person.AnimationFraction), Person.BorderColor);
                }
            }
            foreach(ButtonOffice.Office Office in _Offices)
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
                _DrawRectangle(EventArguments.Graphics, _GetGameRectangle(_EntityPrototype.Location, _EntityPrototype.Width, _EntityPrototype.Height), _EntityPrototype.BackgroundColor, _EntityPrototype.BorderColor);
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

        #region coordinate system transformations

        private System.Int32 _GetDrawingHeight(System.Single GameHeight)
        {
            return (GameHeight * ButtonOffice.Data.BlockHeight.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingWidth(System.Single GameWidth)
        {
            return (GameWidth * ButtonOffice.Data.BlockWidth.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingY(System.Single GameY)
        {
            return (_DrawingBoard.Height.ToSingle() - (GameY * ButtonOffice.Data.BlockHeight.ToSingle()) - _DrawingOffset.Y.ToSingle()).GetIntegerAsInt32();
        }

        private System.Int32 _GetDrawingX(System.Single GameX)
        {
            return (GameX * ButtonOffice.Data.BlockWidth.ToSingle() + _DrawingOffset.X.ToSingle()).GetIntegerAsInt32();
        }

        private System.Drawing.Point _GetGameBlockCoordinates(System.Drawing.Point DrawingCoordinates)
        {
            return new System.Drawing.Point(((DrawingCoordinates.X - _DrawingOffset.X).ToSingle() / ButtonOffice.Data.BlockWidth.ToSingle()).GetIntegerAsInt32(), ((_DrawingBoard.Height - DrawingCoordinates.Y - _DrawingOffset.Y).ToSingle() / ButtonOffice.Data.BlockHeight.ToSingle()).GetIntegerAsInt32());
        }

        private System.Drawing.PointF _GetGameCoordinates(System.Drawing.Point DrawingCoordinates)
        {
            return new System.Drawing.PointF((DrawingCoordinates.X - _DrawingOffset.X).ToSingle() / ButtonOffice.Data.BlockWidth.ToSingle(), (_DrawingBoard.Height - DrawingCoordinates.Y - _DrawingOffset.Y).ToSingle() / ButtonOffice.Data.BlockHeight.ToSingle());
        }

        private System.Drawing.Rectangle _GetDrawingRectangle(System.Drawing.RectangleF GameRectangle)
        {
            System.Drawing.Point DrawingPoint = _GetDrawingPoint(GameRectangle.Location);
            System.Drawing.Size DrawingSize = _GetDrawingSize(GameRectangle.Size);

            return new System.Drawing.Rectangle(DrawingPoint.X, DrawingPoint.Y - DrawingSize.Height, DrawingSize.Width, DrawingSize.Height);
        }

        private System.Drawing.Rectangle _GetDrawingRectangle(System.Single GameX, System.Single GameY, System.Single GameWidth, System.Single GameHeight)
        {
            System.Int32 DrawingHeight = _GetDrawingHeight(GameHeight);

            return new System.Drawing.Rectangle(_GetDrawingX(GameX), _GetDrawingY(GameY) - DrawingHeight, _GetDrawingWidth(GameWidth), DrawingHeight);
        }

        private System.Drawing.Size _GetDrawingSize(System.Drawing.SizeF GameSize)
        {
            return new System.Drawing.Size((GameSize.Width * ButtonOffice.Data.BlockWidth.ToSingle()).GetIntegerAsInt32(), (GameSize.Height * ButtonOffice.Data.BlockHeight.ToSingle()).GetIntegerAsInt32());
        }

        private System.Drawing.Point _GetDrawingPoint(System.Drawing.PointF GamePoint)
        {
            return new System.Drawing.Point((GamePoint.X * ButtonOffice.Data.BlockWidth.ToSingle() + _DrawingOffset.X).GetIntegerAsInt32(), _DrawingBoard.Height - (GamePoint.Y * ButtonOffice.Data.BlockHeight.ToSingle() + _DrawingOffset.Y).GetIntegerAsInt32());
        }

        private System.Drawing.RectangleF _GetGameRectangle(System.Drawing.PointF Location, System.Single Width, System.Single Height)
        {
            return new System.Drawing.RectangleF(Location.X, Location.Y, Width, Height);
        }

        private System.Drawing.RectangleF _GetGameRectangle(System.Single X, System.Single Y, System.Single Width, System.Single Height)
        {
            return new System.Drawing.RectangleF(X, Y, Width, Height);
        }

        private ButtonOffice.Office _GetOffice(System.Drawing.Point GameCoordinates)
        {
            foreach(ButtonOffice.Office Office in _Offices)
            {
                if((GameCoordinates.X >= Office.GetX()) && (GameCoordinates.X < Office.GetX() + ButtonOffice.Data.OfficeBlockWidth) && (GameCoordinates.Y >= Office.GetY()) && (GameCoordinates.Y < Office.GetY() + 1))
                {
                    return Office;
                }
            }

            return null;
        }

        #endregion

        private void _OnMainWindowResized(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.Invalidate();
        }

        private System.UInt64 _GetDay()
        {
            return _Minutes / 1440;
        }

        private System.UInt64 _GetMinuteOfDay()
        {
            return _Minutes % 1440;
        }

        private System.UInt64 _GetFirstMinuteOfDay(System.UInt64 Day)
        {
            return Day * 1440;
        }

        private System.UInt64 _GetFirstMinuteOfToday()
        {
            return _GetDay() * 1440;
        }

        private System.UInt64 _GetFirstMinuteOfTomorrow()
        {
            return (_GetDay() + 1) * 1440;
        }

        private void _OnTimerTicked(System.Object Sender, System.EventArgs EventArguments)
        {
            System.DateTime Now = System.DateTime.Now;

            if(Now > _NextMinute)
            {
                _Minutes += 1;
                _NextMinute = System.DateTime.Now.AddMilliseconds(166);
            }
            _TimeLabel.Text = "Day && Time: " + new System.TimeSpan(_GetDay().ToInt32(), 0, _GetMinuteOfDay().ToInt32(), 0).ToString();
            _MoneyLabel.Text = "Money: " + (_Cents / 100).ToString() + "." + (_Cents % 100).ToString("00") + "€";
            _EmployeesLabel.Text = "Employees: " + _Persons.Count.ToString();
            _Move(ButtonOffice.Data.GameMinutesPerSecond * (Now - _LastTick).TotalSeconds.ToSingle());
            _LastTick = Now;
            _DrawingBoard.Invalidate();
        }

        private void _Move(System.Single GameMinutes)
        {
            foreach(ButtonOffice.Office Office in _Offices)
            {
                if(Office.FirstLamp.IsBroken() == false)
                {
                    Office.FirstLamp.SetMinutesUntilBroken(Office.FirstLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.FirstLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.FirstLamp));
                    }
                }
                if(Office.SecondLamp.IsBroken() == false)
                {
                    Office.SecondLamp.SetMinutesUntilBroken(Office.SecondLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.SecondLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.SecondLamp));
                    }
                }
                if(Office.ThirdLamp.IsBroken() == false)
                {
                    Office.ThirdLamp.SetMinutesUntilBroken(Office.ThirdLamp.GetMinutesUntilBroken() - GameMinutes);
                    if(Office.ThirdLamp.IsBroken() == true)
                    {
                        _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Office, ButtonOffice.BrokenThing.ThirdLamp));
                    }
                }
            }
            foreach(ButtonOffice.Person Person in _Persons)
            {
                switch(Person.Type)
                {
                case ButtonOffice.Type.ITTech:
                    {
                        _MoveITTech(Person as ButtonOffice.ITTech, GameMinutes);

                        break;
                    }
                case ButtonOffice.Type.Janitor:
                    {
                        _MoveJanitor(Person as ButtonOffice.Janitor, GameMinutes);

                        break;
                    }
                case ButtonOffice.Type.Worker:
                    {
                        _MoveWorker(Person as ButtonOffice.Worker, GameMinutes);

                        break;
                    }
                }
            }
        }

        private void _MoveITTech(ButtonOffice.ITTech ITTech, System.Single GameMinutes)
        {
            switch(ITTech.ActionState)
            {
            case ButtonOffice.ActionState.Working:
                {
                    ITTech.ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > ITTech.ArrivesAtMinute)
                    {
                        ITTech.ActionState = ButtonOffice.ActionState.Arriving;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(ITTech.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            ITTech.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            ITTech.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        ITTech.WalkTo = new System.Drawing.PointF(ITTech.Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f, ITTech.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.Working;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Standing;
                        ITTech.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.ActionState = ButtonOffice.ActionState.AtHome;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(ITTech);
                    }

                    break;
                }
            case ButtonOffice.ActionState.WaitingForBrokenThings:
                {
                    if(_Minutes > ITTech.LeavesAtMinute)
                    {
                        if(ITTech.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            ITTech.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            ITTech.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        ITTech.ActionState = ButtonOffice.ActionState.Leaving;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        ITTech.AnimationFraction = 0.0f;
                        _Cents -= ITTech.Wage;
                    }
                    else
                    {
                        if(_BrokenThings.Count > 0)
                        {
                            System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing> BrokenThing = _BrokenThings.Dequeue();

                            switch(BrokenThing.Second)
                            {
                            case ButtonOffice.BrokenThing.FirstComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.FirstDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.SecondDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.ThirdDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FourthComputer:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.FourthDesk.GetX(), BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.FirstLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampOneX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.SecondLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampTwoX, BrokenThing.First.GetY());

                                    break;
                                }
                            case ButtonOffice.BrokenThing.ThirdLamp:
                                {
                                    ITTech.WalkTo = new System.Drawing.PointF(BrokenThing.First.GetX() + ButtonOffice.Data.LampThreeX, BrokenThing.First.GetY());

                                    break;
                                }
                            }
                            ITTech.SetRepairingTarget(BrokenThing.First, BrokenThing.Second);
                            ITTech.ActionState = ButtonOffice.ActionState.GoingToRepair;
                            ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                            ITTech.AnimationFraction = 0.0f;
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToRepair:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.Repairing;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Repairing;
                        ITTech.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Repairing:
                {
                    ITTech.AnimationFraction += ButtonOffice.Data.ITTechRepairSpeed * GameMinutes;
                    if(ITTech.AnimationFraction >= 1.0f)
                    {
                        System.Random Random = new System.Random();

                        switch(ITTech.GetBrokenThing())
                        {
                        case ButtonOffice.BrokenThing.FirstComputer:
                            {
                                ITTech.GetOffice().FirstDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondComputer:
                            {
                                ITTech.GetOffice().SecondDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdComputer:
                            {
                                ITTech.GetOffice().ThirdDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FourthComputer:
                            {
                                ITTech.GetOffice().FourthDesk.SetMinutesUntilComputerBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenComputer));

                                break;
                            }
                        case ButtonOffice.BrokenThing.FirstLamp:
                            {
                                ITTech.GetOffice().FirstLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.SecondLamp:
                            {
                                ITTech.GetOffice().SecondLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        case ButtonOffice.BrokenThing.ThirdLamp:
                            {
                                ITTech.GetOffice().ThirdLamp.SetMinutesUntilBroken(Random.GetSingleFromExponentialDistribution(ButtonOffice.Data.MeanMinutesToBrokenLamp));

                                break;
                            }
                        }
                        ITTech.DropRepairingTarget();
                        ITTech.ActionState = ButtonOffice.ActionState.GoingToDesk;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Walking;
                        ITTech.AnimationFraction = 0.0f;
                        ITTech.WalkTo = new System.Drawing.PointF(ITTech.Desk.GetX() + (ButtonOffice.Data.DeskWidth - ITTech.GetWidth()) / 2.0f, ITTech.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToDesk:
                {
                    System.Single DeltaX = ITTech.WalkTo.X - ITTech.GetX();
                    System.Single DeltaY = ITTech.WalkTo.Y - ITTech.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        ITTech.SetLocation(ITTech.GetX() + DeltaX, ITTech.GetY() + DeltaY);
                    }
                    else
                    {
                        ITTech.SetLocation(ITTech.WalkTo);
                        ITTech.ActionState = ButtonOffice.ActionState.WaitingForBrokenThings;
                        ITTech.AnimationState = ButtonOffice.AnimationState.Standing;
                        ITTech.AnimationFraction = 0.0f;
                        ITTech.Desk.TrashLevel += 1.0f;
                    }

                    break;
                }
            }
        }

        private void _MoveJanitor(ButtonOffice.Janitor Janitor, System.Single GameMinutes)
        {
            switch(Janitor.ActionState)
            {
            case ButtonOffice.ActionState.GoingToClean:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.Cleaning;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Cleaning;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Cleaning:
                {
                    ButtonOffice.Desk Desk = Janitor.PeekFirstCleaningTarget();

                    if(Desk.Janitor == null)
                    {
                        Desk.Janitor = Janitor;
                    }
                    if(Desk.Janitor == Janitor)
                    {
                        if(Desk.TrashLevel > 0.0f)
                        {
                            Janitor.AnimationFraction += ButtonOffice.Data.JanitorCleanSpeed * GameMinutes;
                            while(Janitor.AnimationFraction > 1.0f)
                            {
                                Desk.TrashLevel -= ButtonOffice.Data.JanitorCleanAmount;
                                Janitor.AnimationFraction -= 1.0f;
                            }
                        }
                        if(Desk.TrashLevel <= 0.0f)
                        {
                            Desk.Janitor = null;
                            Desk.TrashLevel = 0.0f;
                            Janitor.DropFirstCleaningTarget();
                            Janitor.ActionState = ButtonOffice.ActionState.PickTrash;
                            Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                            Janitor.AnimationFraction = 0.0f;
                        }
                    }
                    else
                    {
                        Janitor.DropFirstCleaningTarget();
                        Janitor.ActionState = ButtonOffice.ActionState.PickTrash;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.GoingToDesk:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.WaitingToGoHome;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.PickTrash:
                {
                    if(_Minutes > Janitor.LeavesAtMinute)
                    {
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.ActionState = ButtonOffice.ActionState.Leaving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                        Janitor.DropAllCleaningTargets();
                        _Cents -= Janitor.Wage;
                    }
                    else
                    {
                        if(Janitor.GetNumberOfCleaningTargets() > 0)
                        {
                            Janitor.WalkTo = Janitor.PeekFirstCleaningTarget().GetLocation();
                            Janitor.ActionState = ButtonOffice.ActionState.GoingToClean;
                        }
                        else
                        {
                            Janitor.WalkTo = Janitor.Desk.GetLocation();
                            Janitor.ActionState = ButtonOffice.ActionState.GoingToDesk;
                        }
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.WaitingToGoHome:
                {
                    if(_Minutes > Janitor.LeavesAtMinute)
                    {
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.ActionState = ButtonOffice.ActionState.Leaving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        Janitor.AnimationFraction = 0.0f;
                        Janitor.DropAllCleaningTargets();
                        _Cents -= Janitor.Wage;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    foreach(ButtonOffice.Office Office in _Offices)
                    {
                        Janitor.EnqueueCleaningTarget(Office.FirstDesk);
                        Janitor.EnqueueCleaningTarget(Office.SecondDesk);
                        Janitor.EnqueueCleaningTarget(Office.ThirdDesk);
                        Janitor.EnqueueCleaningTarget(Office.FourthDesk);
                    }
                    Janitor.ActionState = ButtonOffice.ActionState.PickTrash;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > Janitor.ArrivesAtMinute)
                    {
                        Janitor.ActionState = ButtonOffice.ActionState.Arriving;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(Janitor.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Janitor.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            Janitor.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Janitor.WalkTo = new System.Drawing.PointF(Janitor.Desk.GetX() + (ButtonOffice.Data.DeskWidth - Janitor.GetWidth()) / 2.0f, Janitor.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.SetLocation(Janitor.WalkTo);
                        Janitor.ActionState = ButtonOffice.ActionState.Working;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Standing;
                        Janitor.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = Janitor.WalkTo.X - Janitor.GetX();
                    System.Single DeltaY = Janitor.WalkTo.Y - Janitor.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Janitor.SetLocation(Janitor.GetX() + DeltaX, Janitor.GetY() + DeltaY);
                    }
                    else
                    {
                        Janitor.ActionState = ButtonOffice.ActionState.AtHome;
                        Janitor.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(Janitor);
                    }

                    break;
                }
            }
        }

        private void _MoveWorker(ButtonOffice.Worker Worker, System.Single GameMinutes)
        {
            switch(Worker.ActionState)
            {
            case ButtonOffice.ActionState.PushingButton:
                {
                    if(_Minutes > Worker.LeavesAtMinute)
                    {
                        if(Worker.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Worker.WalkTo = new System.Drawing.PointF(-10.0f, 0.0f);
                        }
                        else
                        {
                            Worker.WalkTo = new System.Drawing.PointF(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Worker.ActionState = ButtonOffice.ActionState.Leaving;
                        Worker.AnimationState = ButtonOffice.AnimationState.Walking;
                        Worker.AnimationFraction = 0.0f;
                        _Cents -= Worker.Wage;
                    }
                    else
                    {
                        if(Worker.Desk.IsComputerBroken() == false)
                        {
                            Worker.Desk.SetMinutesUntilComputerBroken(Worker.Desk.GetMinutesUntilComputerBroken() - GameMinutes);
                            if(Worker.Desk.IsComputerBroken() == true)
                            {
                                Worker.AnimationState = ButtonOffice.AnimationState.Standing;
                                Worker.AnimationFraction = 0.0f;
                                if(Worker.Desk == Worker.Desk.Office.FirstDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.FirstComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.SecondDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.SecondComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.ThirdDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.ThirdComputer));
                                }
                                else if(Worker.Desk == Worker.Desk.Office.FourthDesk)
                                {
                                    _BrokenThings.Enqueue(new System.Pair<ButtonOffice.Office, ButtonOffice.BrokenThing>(Worker.Desk.Office, ButtonOffice.BrokenThing.FourthComputer));
                                }
                            }
                            else
                            {
                                Worker.AnimationFraction += ButtonOffice.Data.WorkerWorkSpeed * GameMinutes;
                                while(Worker.AnimationFraction >= 1.0f)
                                {
                                    Worker.AnimationFraction -= 1.0f;
                                    Worker.Desk.TrashLevel += 1.0f;
                                    _Cents += 100;
                                }
                            }
                        }
                    }

                    break;
                }
            case ButtonOffice.ActionState.Working:
                {
                    Worker.ActionState = ButtonOffice.ActionState.PushingButton;
                    Worker.AnimationState = ButtonOffice.AnimationState.PushingButton;
                    Worker.AnimationFraction = 0.0f;

                    break;
                }
            case ButtonOffice.ActionState.AtHome:
                {
                    if(_Minutes > Worker.ArrivesAtMinute)
                    {
                        Worker.ActionState = ButtonOffice.ActionState.Arriving;
                        Worker.AnimationState = ButtonOffice.AnimationState.Walking;
                        if(Worker.LivingSide == ButtonOffice.LivingSide.Left)
                        {
                            Worker.SetLocation(-10.0f, 0.0f);
                        }
                        else
                        {
                            Worker.SetLocation(ButtonOffice.Data.WorldBlockWidth + 10.0f, 0.0f);
                        }
                        Worker.WalkTo = new System.Drawing.PointF(Worker.Desk.GetX() + (ButtonOffice.Data.DeskWidth - Worker.GetWidth()) / 2.0f, Worker.Desk.GetY());
                    }

                    break;
                }
            case ButtonOffice.ActionState.Arriving:
                {
                    System.Single DeltaX = Worker.WalkTo.X - Worker.GetX();
                    System.Single DeltaY = Worker.WalkTo.Y - Worker.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Worker.SetLocation(Worker.GetX() + DeltaX, Worker.GetY() + DeltaY);
                    }
                    else
                    {
                        Worker.SetLocation(Worker.WalkTo);
                        Worker.ActionState = ButtonOffice.ActionState.Working;
                        Worker.AnimationState = ButtonOffice.AnimationState.Standing;
                        Worker.AnimationFraction = 0.0f;
                    }

                    break;
                }
            case ButtonOffice.ActionState.Leaving:
                {
                    System.Single DeltaX = Worker.WalkTo.X - Worker.GetX();
                    System.Single DeltaY = Worker.WalkTo.Y - Worker.GetY();
                    System.Single Norm = System.Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY).ToSingle();

                    if(Norm > 0.1)
                    {
                        DeltaX = DeltaX / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        DeltaY = DeltaY / Norm * ButtonOffice.Data.PersonSpeed * GameMinutes;
                        Worker.SetLocation(Worker.GetX() + DeltaX, Worker.GetY() + DeltaY);
                    }
                    else
                    {
                        Worker.ActionState = ButtonOffice.ActionState.AtHome;
                        Worker.AnimationState = ButtonOffice.AnimationState.Hidden;
                        _PlanNextWorkDay(Worker);
                    }

                    break;
                }
            }
        }

        private void _PlanNextWorkDay(ButtonOffice.Person Person)
        {
            System.UInt64 MinuteOfDay = _GetMinuteOfDay();

            Person.ArrivesAtMinute = _GetFirstMinuteOfToday() + Person.ArrivesAtDayMinute;
            if(Person.ArrivesAtMinute + Person.WorkMinutes < _Minutes)
            {
                Person.ArrivesAtMinute += 1440;
            }
            Person.LeavesAtMinute = Person.ArrivesAtMinute + Person.WorkMinutes;
        }

        private void _OnMainWindowLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.BackColor = ButtonOffice.Data.BackgroundColor;
            _Cents = ButtonOffice.Data.StartCents;
            _Minutes = 480;
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

    internal class EntityPrototype
    {
        private System.Drawing.Color _BackgroundColor;
        private System.Single _Height;
        private System.Single _Width;
        private System.Drawing.Color _BorderColor;
        private System.Nullable<System.Drawing.Point> _Location;
        private ButtonOffice.Type _Type;

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

        public System.Single Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }

        public System.Single Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
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

        public System.Drawing.Point Location
        {
            get
            {
                return _Location.Value;
            }
        }

        public ButtonOffice.Type Type
        {
            get
            {
                return _Type;
            }
        }

        public EntityPrototype(ButtonOffice.Type Type)
        {
            _Location = new System.Nullable<System.Drawing.Point>();
            _Type = Type;
        }

        public void SetLocationFromMouseLocation(System.Drawing.Point MouseLocation)
        {
            _Location = new System.Drawing.Point(MouseLocation.X - (_Width / 2.0f).GetIntegerAsInt32(), MouseLocation.Y - (_Height / 2.0f).GetIntegerAsInt32());
        }

        public System.Boolean HasLocation()
        {
            return _Location.HasValue;
        }
    }
}

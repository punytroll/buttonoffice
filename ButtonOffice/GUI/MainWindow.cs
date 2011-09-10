namespace ButtonOffice
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Drawing.PointF _CameraVelocity;
        private System.Collections.Generic.List<ButtonOffice.FloatingText> _FloatingTexts;
        private System.Windows.Forms.CheckBox _MoveButton;
        private ButtonOffice.Person _MovePerson;
        private ButtonOffice.Game _Game;
        private System.Collections.Generic.List<System.Windows.Forms.ToolStripButton> _ToolButtons;
        private System.Nullable<System.Drawing.Point> _DragPoint;
        private System.Drawing.Point _DrawingOffset;
        private ButtonOffice.EntityPrototype _EntityPrototype;
        private System.Windows.Forms.Timer _Timer;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.ToolStrip _GameTools;
        private System.Windows.Forms.ToolStripButton _BuildOfficeButton;
        private System.Windows.Forms.ToolStripButton _HireWorkerButton;
        private System.Windows.Forms.StatusStrip _StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel _TimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel _MoneyLabel;
        private System.Windows.Forms.ToolStripStatusLabel _EmployeesLabel;
        private System.DateTime _LastTick;
        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private DrawingBoard _DrawingBoard;
        private System.Windows.Forms.ToolStripStatusLabel _PositionLabel;
        private System.Windows.Forms.ToolStripButton _HireITTechButton;
        private ButtonOffice.Person _SelectedPerson;
        private System.Windows.Forms.ToolStripButton _HireJanitorButton;
        private System.Windows.Forms.ToolStripButton _PlaceCatButton;
        private ButtonOffice.Office _SelectedOffice;
        private System.Windows.Forms.ToolStrip _SystemTools;
        private System.Windows.Forms.ToolStripDropDownButton _LoadButton;
        private System.Windows.Forms.ToolStripMenuItem _LoadGameButton;
        private System.Windows.Forms.ToolStripMenuItem _NewGameButton;
        private System.Windows.Forms.ToolStripMenuItem _QuitApplicationButton;
        private System.Windows.Forms.ToolStripMenuItem _SaveGameButton;
        private System.Windows.Forms.ToolStripButton _HireAccountantButton;
        private System.Single _Zoom;
    
        public MainWindow()
        {
            InitializeComponent();
            _ToolButtons = new System.Collections.Generic.List<System.Windows.Forms.ToolStripButton>();
            _ToolButtons.Add(_HireITTechButton);
            _ToolButtons.Add(_HireJanitorButton);
            _ToolButtons.Add(_HireWorkerButton);
            _ToolButtons.Add(_BuildOfficeButton);
            _ToolButtons.Add(_HireAccountantButton);
            _ToolButtons.Add(_PlaceCatButton);
            _FloatingTexts = new System.Collections.Generic.List<ButtonOffice.FloatingText>();
            _Game = ButtonOffice.Game.CreateNew();
            _OnNewGame();
            _DrawingBoard.MouseWheel += delegate(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator Separator1;
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._StatusBar = new System.Windows.Forms.StatusStrip();
            this._TimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MoneyLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._EmployeesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._PositionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this._DrawingBoard = new ButtonOffice.DrawingBoard();
            this._SystemTools = new System.Windows.Forms.ToolStrip();
            this._LoadButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._NewGameButton = new System.Windows.Forms.ToolStripMenuItem();
            this._SaveGameButton = new System.Windows.Forms.ToolStripMenuItem();
            this._LoadGameButton = new System.Windows.Forms.ToolStripMenuItem();
            this._QuitApplicationButton = new System.Windows.Forms.ToolStripMenuItem();
            this._GameTools = new System.Windows.Forms.ToolStrip();
            this._BuildOfficeButton = new System.Windows.Forms.ToolStripButton();
            this._HireWorkerButton = new System.Windows.Forms.ToolStripButton();
            this._HireITTechButton = new System.Windows.Forms.ToolStripButton();
            this._HireJanitorButton = new System.Windows.Forms.ToolStripButton();
            this._HireAccountantButton = new System.Windows.Forms.ToolStripButton();
            this._PlaceCatButton = new System.Windows.Forms.ToolStripButton();
            Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._StatusBar.SuspendLayout();
            this._MainSplitContainer.Panel1.SuspendLayout();
            this._MainSplitContainer.SuspendLayout();
            this._SystemTools.SuspendLayout();
            this._GameTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // Separator1
            // 
            Separator1.Name = "Separator1";
            Separator1.Size = new System.Drawing.Size(97, 6);
            // 
            // _Timer
            // 
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
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._SystemTools);
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._GameTools);
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
            this._DrawingBoard.KeyUp += new System.Windows.Forms.KeyEventHandler(this._DrawingBoardKeyUp);
            this._DrawingBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnDrawingBoardMouseDown);
            this._DrawingBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnDrawingBoardMouseUp);
            this._DrawingBoard.KeyDown += new System.Windows.Forms.KeyEventHandler(this._DrawingBoardKeyDown);
            // 
            // _SystemTools
            // 
            this._SystemTools.Dock = System.Windows.Forms.DockStyle.None;
            this._SystemTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._LoadButton});
            this._SystemTools.Location = new System.Drawing.Point(3, 0);
            this._SystemTools.Name = "_SystemTools";
            this._SystemTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._SystemTools.Size = new System.Drawing.Size(54, 25);
            this._SystemTools.TabIndex = 2;
            // 
            // _LoadButton
            // 
            this._LoadButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._LoadButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._NewGameButton,
            this._SaveGameButton,
            this._LoadGameButton,
            Separator1,
            this._QuitApplicationButton});
            this._LoadButton.Name = "_LoadButton";
            this._LoadButton.ShowDropDownArrow = false;
            this._LoadButton.Size = new System.Drawing.Size(42, 22);
            this._LoadButton.Text = "Game";
            // 
            // _NewGameButton
            // 
            this._NewGameButton.Name = "_NewGameButton";
            this._NewGameButton.Size = new System.Drawing.Size(100, 22);
            this._NewGameButton.Text = "New";
            this._NewGameButton.Click += new System.EventHandler(this._OnNewGameButtonClicked);
            // 
            // _SaveGameButton
            // 
            this._SaveGameButton.Name = "_SaveGameButton";
            this._SaveGameButton.Size = new System.Drawing.Size(100, 22);
            this._SaveGameButton.Text = "Save";
            this._SaveGameButton.Click += new System.EventHandler(this._OnSaveGameButtonClicked);
            // 
            // _LoadGameButton
            // 
            this._LoadGameButton.Name = "_LoadGameButton";
            this._LoadGameButton.Size = new System.Drawing.Size(100, 22);
            this._LoadGameButton.Text = "Load";
            this._LoadGameButton.Click += new System.EventHandler(this._OnLoadGameButtonClicked);
            // 
            // _QuitApplicationButton
            // 
            this._QuitApplicationButton.Name = "_QuitApplicationButton";
            this._QuitApplicationButton.Size = new System.Drawing.Size(100, 22);
            this._QuitApplicationButton.Text = "Quit";
            this._QuitApplicationButton.Click += new System.EventHandler(this._OnQuitApplicationButtonClicked);
            // 
            // _GameTools
            // 
            this._GameTools.Dock = System.Windows.Forms.DockStyle.None;
            this._GameTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._BuildOfficeButton,
            this._HireWorkerButton,
            this._HireITTechButton,
            this._HireJanitorButton,
            this._HireAccountantButton,
            this._PlaceCatButton});
            this._GameTools.Location = new System.Drawing.Point(57, 0);
            this._GameTools.Name = "_GameTools";
            this._GameTools.Padding = new System.Windows.Forms.Padding(0);
            this._GameTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._GameTools.Size = new System.Drawing.Size(301, 25);
            this._GameTools.TabIndex = 1;
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
            this._HireWorkerButton.Name = "_HireWorkerButton";
            this._HireWorkerButton.Size = new System.Drawing.Size(49, 22);
            this._HireWorkerButton.Text = "Worker";
            this._HireWorkerButton.CheckedChanged += new System.EventHandler(this._OnHireWorkerButtonCheckedChanged);
            this._HireWorkerButton.Click += new System.EventHandler(this._OnHireWorkerButtonClicked);
            // 
            // _HireITTechButton
            // 
            this._HireITTechButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireITTechButton.Name = "_HireITTechButton";
            this._HireITTechButton.Size = new System.Drawing.Size(50, 22);
            this._HireITTechButton.Text = "IT Tech";
            this._HireITTechButton.CheckedChanged += new System.EventHandler(this._OnHireITTechButtonCheckedChanged);
            this._HireITTechButton.Click += new System.EventHandler(this._OnHireITTechButtonClicked);
            // 
            // _HireJanitorButton
            // 
            this._HireJanitorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireJanitorButton.Name = "_HireJanitorButton";
            this._HireJanitorButton.Size = new System.Drawing.Size(46, 22);
            this._HireJanitorButton.Text = "Janitor";
            this._HireJanitorButton.CheckedChanged += new System.EventHandler(this._OnHireJanitorButtonCheckedChanged);
            this._HireJanitorButton.Click += new System.EventHandler(this._OnHireJanitorButtonClicked);
            // 
            // _HireAccountantButton
            // 
            this._HireAccountantButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireAccountantButton.Name = "_HireAccountantButton";
            this._HireAccountantButton.Size = new System.Drawing.Size(73, 22);
            this._HireAccountantButton.Text = "Accountant";
            this._HireAccountantButton.CheckedChanged += new System.EventHandler(this._OnHireAccountantButtonCheckedChanged);
            this._HireAccountantButton.Click += new System.EventHandler(this._OnHireAccountantButtonClicked);
            // 
            // _PlaceCatButton
            // 
            this._PlaceCatButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._PlaceCatButton.Name = "_PlaceCatButton";
            this._PlaceCatButton.Size = new System.Drawing.Size(29, 22);
            this._PlaceCatButton.Text = "Cat";
            this._PlaceCatButton.CheckedChanged += new System.EventHandler(this._OnPlaceCatButtonCheckedChanged);
            this._PlaceCatButton.Click += new System.EventHandler(this._OnPlaceCatButtonClicked);
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
            this._SystemTools.ResumeLayout(false);
            this._SystemTools.PerformLayout();
            this._GameTools.ResumeLayout(false);
            this._GameTools.PerformLayout();
            this.ResumeLayout(false);

        }

        private void _UncheckAllToolButtons()
        {
            foreach(System.Windows.Forms.ToolStripButton Button in _ToolButtons)
            {
                Button.Checked = false;
            }
        }

        private void _ToggleOneToolButton(System.Windows.Forms.ToolStripButton ToggleButton)
        {
            if(ToggleButton.Checked == true)
            {
                ToggleButton.Checked = false;
            }
            else
            {
                // uncheck all other buttons (if necessary)
                foreach(System.Windows.Forms.ToolStripButton Button in _ToolButtons)
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

        private void _OnBuildOfficeButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_BuildOfficeButton);
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
            _ToggleOneToolButton(_HireITTechButton);
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
            _ToggleOneToolButton(_HireJanitorButton);
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
            _ToggleOneToolButton(_HireWorkerButton);
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

        private void _OnHireAccountantButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_HireAccountantButton);
        }

        private void _OnHireAccountantButtonCheckedChanged(System.Object Sender, System.EventArgs EventArguments)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            if(_HireAccountantButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.Accountant);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.AccountantBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.AccountantBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.PersonHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.PersonWidth);
            }
        }

        private void _OnPlaceCatButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ToggleOneToolButton(_PlaceCatButton);
        }

        private void _OnPlaceCatButtonCheckedChanged(object sender, System.EventArgs e)
        {
            if(_EntityPrototype != null)
            {
                _EntityPrototype = null;
            }
            if(_PlaceCatButton.Checked == true)
            {
                _EntityPrototype = new EntityPrototype(ButtonOffice.Type.Cat);
                _EntityPrototype.BackgroundColor = ButtonOffice.Data.CatBackgroundColor;
                _EntityPrototype.BorderColor = ButtonOffice.Data.CatBorderColor;
                _EntityPrototype.SetHeight(ButtonOffice.Data.CatHeight);
                _EntityPrototype.SetWidth(ButtonOffice.Data.CatWidth);
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
                    _EntityPrototype.SetLocationFromGamingLocation(_GetGamingLocation(EventArguments.Location).GetFloored());
                    _DrawingBoard.Invalidate();
                }
            }

            System.Drawing.PointF GamingLocation = _GetGamingLocation(EventArguments.Location);

            _PositionLabel.Text = "Location: " + GamingLocation.X.GetFlooredAsInt32().ToString() + " / " + GamingLocation.Y.GetFlooredAsInt32().ToString();
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
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Accountant)
                    {
                        if(_Game.HireAccountant(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _HireAccountantButton.Checked = false;
                            }
                        }
                    }
                    else if(_EntityPrototype.Type == ButtonOffice.Type.Cat)
                    {
                        if(_Game.PlaceCat(_EntityPrototype.Rectangle) == true)
                        {
                            if(System.Windows.Forms.Control.ModifierKeys != System.Windows.Forms.Keys.Shift)
                            {
                                _PlaceCatButton.Checked = false;
                            }
                        }
                    }
                }
                else if(_MovePerson != null)
                {
                    System.Drawing.PointF GamingLocation = _GetGamingLocation(EventArguments.Location);
                    ButtonOffice.Desk Desk = _Game.GetDesk(GamingLocation);

                    if((Desk != null) && (Desk.IsFree() == true))
                    {
                        _Game.MovePerson(_MovePerson, Desk);
                        _MoveButton.Checked = false;
                    }
                }
                else
                {
                    System.Drawing.PointF GamingLocation = _GetGamingLocation(EventArguments.Location);
                    System.Boolean Selected = false;

                    _MainSplitContainer.Panel2.Controls.Clear();
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

                                System.Windows.Forms.Button FireButton = new System.Windows.Forms.Button();

                                FireButton.Location = new System.Drawing.Point(10, 80);
                                FireButton.Size = new System.Drawing.Size(100, 20);
                                FireButton.Text = "Fire";
                                FireButton.Click += delegate(System.Object DelegateSender, System.EventArgs DelegateEventArguments)
                                {
                                    _Game.FirePerson(Person);
                                    _MainSplitContainer.Panel2Collapsed = true;
                                    _MainSplitContainer.Panel2.Controls.Clear();
                                };
                                _MainSplitContainer.Panel2.Controls.Add(FireButton);
                                _MoveButton = new System.Windows.Forms.CheckBox();
                                _MoveButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                                _MoveButton.Location = new System.Drawing.Point(10, 120);
                                _MoveButton.Size = new System.Drawing.Size(100, 20);
                                _MoveButton.Text = "Move";
                                _MoveButton.Appearance = System.Windows.Forms.Appearance.Button;
                                _MoveButton.Checked = false;
                                _MoveButton.CheckedChanged += delegate(System.Object DelegateSender, System.EventArgs DelegateEventArguments)
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
                        foreach(ButtonOffice.Office Office in _Game.Offices)
                        {
                            if(Office.GetRectangle().Contains(GamingLocation) == true)
                            {
                                _SelectedOffice = Office;
                                Selected = true;

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
            return System.Drawing.Color.FromArgb((Color.R + (255 - Color.R) * Fraction).GetTruncatedAsInt32(), (Color.G + (255 - Color.G) * Fraction).GetTruncatedAsInt32(), (Color.B + (255 - Color.B) * Fraction).GetTruncatedAsInt32());
        }

        private void _OnDrawingBoardPaint(System.Object Sender, System.Windows.Forms.PaintEventArgs EventArguments)
        {
            _DrawingOffset.X += _CameraVelocity.X.GetFlooredAsInt32();
            _DrawingOffset.Y += _CameraVelocity.Y.GetFlooredAsInt32();
            for(System.Int32 Row = 0; Row < ButtonOffice.Data.WorldBlockHeight; ++Row)
            {
                System.Pair<System.Int32, System.Int32> BuildingMinimumMaximum = _Game.GetBuildingMinimumMaximum(Row);

                if(BuildingMinimumMaximum.Second.ToInt64() - BuildingMinimumMaximum.First.ToInt64() > 0)
                {
                    _DrawRectangle(EventArguments.Graphics, new System.Drawing.RectangleF(BuildingMinimumMaximum.First.ToSingle(), Row.ToSingle(), (BuildingMinimumMaximum.Second - BuildingMinimumMaximum.First).ToSingle(), 1.0f), ButtonOffice.Data.BuildingBackgroundColor, ButtonOffice.Data.BuildingBorderColor);
                }
                else
                {
                    break;
                }
            }
            EventArguments.Graphics.FillRectangle(new System.Drawing.SolidBrush(ButtonOffice.Data.GroundColor), 0, _GetDrawingY(0), _DrawingBoard.Width, _DrawingBoard.Height);
            foreach(Office Office in _Game.Offices)
            {
                _DrawRectangle(EventArguments.Graphics, Office.GetRectangle(), Office.BackgroundColor, Office.BorderColor);

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
                    _DrawRectangle(EventArguments.Graphics, Person.GetRectangle(), _MixToWhite(Person.BackgroundColor, Person.GetActionFraction()), _MixToWhite(Person.BorderColor, Person.GetAnimationFraction()));
                }
            }
            foreach(ButtonOffice.Office Office in _Game.Offices)
            {
                System.Drawing.Color PersonAtDeskColor;
                System.Drawing.Color PersonColor;
                System.Drawing.Color ComputerColor;

                // first desk
                PersonAtDeskColor = System.Drawing.Color.White;
                PersonColor = System.Drawing.Color.White;
                ComputerColor = ButtonOffice.Data.ComputerBackgroundColor;
                if(Office.FirstDesk.IsFree() == false)
                {
                    PersonColor = Office.FirstDesk.GetPerson().BackgroundColor;
                    if(Office.FirstDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.FirstDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.FirstDesk.GetY() + (Office.FirstDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FirstDesk.GetX() + (Office.FirstDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.FirstDesk.GetY() + Office.FirstDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, ButtonOffice.Data.ComputerBorderColor);
                // second desk
                PersonAtDeskColor = System.Drawing.Color.White;
                PersonColor = System.Drawing.Color.White;
                ComputerColor = ButtonOffice.Data.ComputerBackgroundColor;
                if(Office.SecondDesk.IsFree() == false)
                {
                    PersonColor = Office.SecondDesk.GetPerson().BackgroundColor;
                    if(Office.SecondDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.SecondDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.SecondDesk.GetY() + (Office.SecondDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.SecondDesk.GetX() + (Office.SecondDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.SecondDesk.GetY() + Office.SecondDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, ButtonOffice.Data.ComputerBorderColor);
                // third desk
                PersonAtDeskColor = System.Drawing.Color.White;
                PersonColor = System.Drawing.Color.White;
                ComputerColor = ButtonOffice.Data.ComputerBackgroundColor;
                if(Office.ThirdDesk.IsFree() == false)
                {
                    PersonColor = Office.ThirdDesk.GetPerson().BackgroundColor;
                    if(Office.ThirdDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.ThirdDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.ThirdDesk.GetY() + (Office.ThirdDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.ThirdDesk.GetX() + (Office.ThirdDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.ThirdDesk.GetY() + Office.ThirdDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, ButtonOffice.Data.ComputerBorderColor);
                // fourth desk
                PersonAtDeskColor = System.Drawing.Color.White;
                PersonColor = System.Drawing.Color.White;
                ComputerColor = ButtonOffice.Data.ComputerBackgroundColor;
                if(Office.FourthDesk.IsFree() == false)
                {
                    PersonColor = Office.FourthDesk.GetPerson().BackgroundColor;
                    if(Office.FourthDesk.GetPerson().GetAtDesk() == true)
                    {
                        PersonAtDeskColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        PersonAtDeskColor = PersonColor;
                    }
                }
                if(Office.FourthDesk.GetComputer().IsBroken() == true)
                {
                    ComputerColor = System.Drawing.Color.Red;
                }
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetRectangle(), ButtonOffice.Data.DeskBackgroundColor, System.Drawing.Color.Black);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - ButtonOffice.Data.PersonTagWidth) / 2.0f, Office.FourthDesk.GetY() + (Office.FourthDesk.GetHeight() - ButtonOffice.Data.PersonTagHeight) / 2.0f, ButtonOffice.Data.PersonTagWidth, ButtonOffice.Data.PersonTagHeight, PersonColor, PersonAtDeskColor);
                _DrawRectangle(EventArguments.Graphics, Office.FourthDesk.GetX() + (Office.FourthDesk.GetWidth() - ButtonOffice.Data.ComputerWidth) / 2.0f, Office.FourthDesk.GetY() + Office.FourthDesk.GetHeight() + 0.04f, ButtonOffice.Data.ComputerWidth, ButtonOffice.Data.ComputerHeight, ComputerColor, ButtonOffice.Data.ComputerBorderColor);
                // cat
                if(Office.Cat != null)
                {
                    _DrawEllipse(EventArguments.Graphics, Office.Cat.GetRectangle(), Office.Cat.BackgroundColor, Office.Cat.BorderColor);
                }
            }

            System.Drawing.Font Font = new System.Drawing.Font("Arial", 16.0f);
            System.Drawing.StringFormat Format = new System.Drawing.StringFormat();

            Format.Alignment = System.Drawing.StringAlignment.Center;
            foreach(ButtonOffice.FloatingText FloatingText in _FloatingTexts)
            {
                EventArguments.Graphics.DrawString(FloatingText.Text, Font, new System.Drawing.SolidBrush(FloatingText.Color), _MovePointByOffset(_GetDrawingLocation(FloatingText.Origin), FloatingText.Offset), Format);
            }
            if((_EntityPrototype != null) && (_EntityPrototype.HasLocation() == true))
            {
                if(_EntityPrototype.Type == ButtonOffice.Type.Cat)
                {
                    _DrawEllipse(EventArguments.Graphics, _EntityPrototype.Rectangle, System.Drawing.Color.FromArgb(150, _EntityPrototype.BackgroundColor), System.Drawing.Color.FromArgb(150, _EntityPrototype.BorderColor));
                }
                else
                {
                    _DrawRectangle(EventArguments.Graphics, _EntityPrototype.Rectangle, System.Drawing.Color.FromArgb(150, _EntityPrototype.BackgroundColor), System.Drawing.Color.FromArgb(150, _EntityPrototype.BorderColor));
                }
            }
        }

        private void _DrawEllipse(System.Drawing.Graphics Graphics, System.Drawing.RectangleF GameRectangle, System.Drawing.Color BackgroundColor, System.Drawing.Color BorderColor)
        {
            System.Drawing.Rectangle BackgroundRectangle = _GetDrawingRectangle(GameRectangle);

            Graphics.FillEllipse(new System.Drawing.SolidBrush(BackgroundColor), BackgroundRectangle);

            System.Drawing.Rectangle ForegroundRectangle = BackgroundRectangle;

            ForegroundRectangle.Width -= 1;
            ForegroundRectangle.Height -= 1;
            Graphics.DrawEllipse(new System.Drawing.Pen(BorderColor), ForegroundRectangle);
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
            return (GamingHeight * ButtonOffice.Data.BlockHeight.ToSingle() * _Zoom).GetFlooredAsInt32();
        }

        private System.Int32 _GetDrawingWidth(System.Single GamingWidth)
        {
            return (GamingWidth * ButtonOffice.Data.BlockWidth.ToSingle() * _Zoom).GetFlooredAsInt32();
        }

        private System.Int32 _GetDrawingY(System.Single GamingY)
        {
            return (_DrawingBoard.Height.ToSingle() - (GamingY * ButtonOffice.Data.BlockHeight.ToSingle() * _Zoom) - _DrawingOffset.Y.ToSingle()).GetFlooredAsInt32();
        }

        private System.Int32 _GetDrawingX(System.Single GamingX)
        {
            return (GamingX * ButtonOffice.Data.BlockWidth.ToSingle() * _Zoom + _DrawingOffset.X.ToSingle()).GetFlooredAsInt32();
        }

        private System.Drawing.Rectangle _GetDrawingRectangle(System.Drawing.RectangleF GamingRectangle)
        {
            System.Drawing.Point DrawingPoint = _GetDrawingLocation(GamingRectangle.Location);
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

        private System.Drawing.Point _GetDrawingLocation(System.Drawing.PointF GamingLocation)
        {
            return new System.Drawing.Point(_GetDrawingX(GamingLocation.X), _GetDrawingY(GamingLocation.Y));
        }
        #endregion

        #region Coordinate system transformations: Draw -> Game
        private System.Single _GetGamingX(System.Int32 DrawingX)
        {
            return (DrawingX - _DrawingOffset.X).ToSingle() / ButtonOffice.Data.BlockWidth.ToSingle() / _Zoom;
        }

        private System.Single _GetGamingY(System.Int32 DrawingY)
        {
            return (_DrawingBoard.Height - DrawingY - _DrawingOffset.Y).ToSingle() / ButtonOffice.Data.BlockHeight.ToSingle() / _Zoom;
        }

        private System.Drawing.PointF _GetGamingLocation(System.Drawing.Point DrawingLocation)
        {
            return new System.Drawing.PointF(_GetGamingX(DrawingLocation.X), _GetGamingY(DrawingLocation.Y));
        }
        #endregion

        #region Coordinate system helper functions
        private System.Drawing.PointF _MovePointByOffset(System.Drawing.PointF Point, System.Drawing.PointF Offset)
        {
            return new System.Drawing.PointF(Point.X + Offset.X, Point.Y + Offset.Y);
        }
        #endregion

        private void _OnMainWindowResized(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.Invalidate();
        }

        private void _OnTimerTicked(System.Object Sender, System.EventArgs EventArguments)
        {
            System.DateTime Now = System.DateTime.Now;
            System.Single Seconds = (Now - _LastTick).TotalSeconds.ToSingle();

            if((Seconds > 0.0f) && (Seconds < 0.05f))
            {
                _Game.Move(ButtonOffice.Data.GameMinutesPerSecond * Seconds);

                System.Int32 Index = 0;

                while(Index < _FloatingTexts.Count)
                {
                    _FloatingTexts[Index].SetTimeout(_FloatingTexts[Index].Timeout - Seconds);
                    if(_FloatingTexts[Index].Timeout > 0.0f)
                    {
                        _FloatingTexts[Index].SetOffset(new System.Drawing.PointF(_FloatingTexts[Index].Offset.X, _FloatingTexts[Index].Offset.Y - Seconds * ButtonOffice.Data.FloatingTextSpeed));
                        ++Index;
                    }
                    else
                    {
                        _FloatingTexts.RemoveAt(Index);
                    }
                }
                _TimeLabel.Text = "Day && Time: " + new System.TimeSpan(_Game.GetDay().ToInt32(), 0, _Game.GetMinuteOfDay().ToInt32(), 0).ToString();
                _MoneyLabel.Text = "Money: " + _Game.GetMoneyString(_Game.GetCents());
                _EmployeesLabel.Text = "Employees: " + _Game.Persons.Count.ToString();
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
                        _PlaceCatButton.Text = "Cat (" + _Game.GetCatStock().ToString() + ")";
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

        private void _OnMainWindowLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _DrawingBoard.BackColor = ButtonOffice.Data.BackgroundColor;
            _StartGame();
        }

        private void _DrawingBoardKeyDown(System.Object Sender, System.Windows.Forms.KeyEventArgs EventArguments)
        {
            if(EventArguments.KeyCode == System.Windows.Forms.Keys.W)
            {
                _CameraVelocity.Y = -10;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.A)
            {
                _CameraVelocity.X = 10;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.S)
            {
                _CameraVelocity.Y = 10;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.D)
            {
                _CameraVelocity.X = -10;
            }
        }

        private void _DrawingBoardKeyUp(System.Object Sender, System.Windows.Forms.KeyEventArgs EventArguments)
        {
            if(EventArguments.KeyCode == System.Windows.Forms.Keys.W)
            {
                _CameraVelocity.Y = 0;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.A)
            {
                _CameraVelocity.X = 0;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.S)
            {
                _CameraVelocity.Y = 0;
            }
            else if(EventArguments.KeyCode == System.Windows.Forms.Keys.D)
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

        private void _OnNewGameButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _StopGame();
            _Game = ButtonOffice.Game.CreateNew();
            _OnNewGame();
            _StartGame();
        }

        private void _OnSaveGameButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _StopGame();

            System.Windows.Forms.SaveFileDialog SaveFileDialog = new System.Windows.Forms.SaveFileDialog();

            if(SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Game.SaveToFile(SaveFileDialog.FileName);
            }
            _StartGame();
        }

        private void _OnLoadGameButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _StopGame();

            System.Windows.Forms.OpenFileDialog OpenFileDialog = new System.Windows.Forms.OpenFileDialog();

            if(OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _Game = ButtonOffice.Game.LoadFromFileName(OpenFileDialog.FileName);
                _OnNewGame();
            }
            _StartGame();
        }

        private void _OnQuitApplicationButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _StopGame();
            Close();
        }

        private void _OnNewGame()
        {
            // uncheck all buttons
            _UncheckAllToolButtons();
            _CameraVelocity = new System.Drawing.PointF(0.0f, 0.0f);
            _FloatingTexts.Clear();
            _Game.OnEarnMoney += delegate(System.UInt64 Cents, System.Drawing.PointF Location)
            {
                ButtonOffice.FloatingText FloatingText = new ButtonOffice.FloatingText();

                FloatingText.SetColor(ButtonOffice.Data.EarnMoneyFloatingTextColor);
                FloatingText.SetOffset(new System.Drawing.PointF(0.0f, 0.0f));
                FloatingText.SetOrigin(Location);
                FloatingText.SetText(_Game.GetMoneyString(Cents));
                FloatingText.SetTimeout(1.2f);
                _FloatingTexts.Add(FloatingText);
            };
            _Game.OnSpendMoney += delegate(System.UInt64 Cents, System.Drawing.PointF Location)
            {
                ButtonOffice.FloatingText FloatingText = new ButtonOffice.FloatingText();

                FloatingText.SetColor(ButtonOffice.Data.SpendMoneyFloatingTextColor);
                FloatingText.SetOffset(new System.Drawing.PointF(0.0f, 0.0f));
                FloatingText.SetOrigin(Location);
                FloatingText.SetText(_Game.GetMoneyString(Cents));
                FloatingText.SetTimeout(1.2f);
                _FloatingTexts.Add(FloatingText);
            };
            _EntityPrototype = null;
            _DragPoint = new System.Nullable<System.Drawing.Point>();
            _DrawingOffset = new System.Drawing.Point(-ButtonOffice.Data.WorldBlockWidth * ButtonOffice.Data.BlockWidth / 2, 2 * ButtonOffice.Data.BlockHeight);
            _LastTick = System.DateTime.MinValue;
            _MoveButton = null;
            _MovePerson = null;
            _Zoom = 1.0f;
        }
    }
}

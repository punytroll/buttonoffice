using System.Windows.Forms;

namespace ButtonOffice
{
    internal partial class MainWindow : Form
    {
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
            this._BuildBathroomButton = new System.Windows.Forms.ToolStripButton();
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
            this._BuildBathroomButton,
            this._HireWorkerButton,
            this._HireITTechButton,
            this._HireJanitorButton,
            this._HireAccountantButton,
            this._PlaceCatButton});
            this._GameTools.Location = new System.Drawing.Point(57, 0);
            this._GameTools.Name = "_GameTools";
            this._GameTools.Padding = new System.Windows.Forms.Padding(0);
            this._GameTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._GameTools.Size = new System.Drawing.Size(396, 25);
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
            this._BuildOfficeButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _BuildBathroomButton
            // 
            this._BuildBathroomButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._BuildBathroomButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this._BuildBathroomButton.Name = "_BuildBathroomButton";
            this._BuildBathroomButton.Size = new System.Drawing.Size(64, 22);
            this._BuildBathroomButton.Text = "Bathroom";
            this._BuildBathroomButton.CheckedChanged += new System.EventHandler(this._OnBuildBathroomButtonCheckedChanged);
            this._BuildBathroomButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _HireWorkerButton
            // 
            this._HireWorkerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireWorkerButton.Name = "_HireWorkerButton";
            this._HireWorkerButton.Size = new System.Drawing.Size(49, 22);
            this._HireWorkerButton.Text = "Worker";
            this._HireWorkerButton.CheckedChanged += new System.EventHandler(this._OnHireWorkerButtonCheckedChanged);
            this._HireWorkerButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _HireITTechButton
            // 
            this._HireITTechButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireITTechButton.Name = "_HireITTechButton";
            this._HireITTechButton.Size = new System.Drawing.Size(50, 22);
            this._HireITTechButton.Text = "IT Tech";
            this._HireITTechButton.CheckedChanged += new System.EventHandler(this._OnHireITTechButtonCheckedChanged);
            this._HireITTechButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _HireJanitorButton
            // 
            this._HireJanitorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireJanitorButton.Name = "_HireJanitorButton";
            this._HireJanitorButton.Size = new System.Drawing.Size(46, 22);
            this._HireJanitorButton.Text = "Janitor";
            this._HireJanitorButton.CheckedChanged += new System.EventHandler(this._OnHireJanitorButtonCheckedChanged);
            this._HireJanitorButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _HireAccountantButton
            // 
            this._HireAccountantButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._HireAccountantButton.Name = "_HireAccountantButton";
            this._HireAccountantButton.Size = new System.Drawing.Size(73, 22);
            this._HireAccountantButton.Text = "Accountant";
            this._HireAccountantButton.CheckedChanged += new System.EventHandler(this._OnHireAccountantButtonCheckedChanged);
            this._HireAccountantButton.Click += new System.EventHandler(this._OnToolButtonClicked);
            // 
            // _PlaceCatButton
            // 
            this._PlaceCatButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._PlaceCatButton.Name = "_PlaceCatButton";
            this._PlaceCatButton.Size = new System.Drawing.Size(29, 22);
            this._PlaceCatButton.Text = "Cat";
            this._PlaceCatButton.CheckedChanged += new System.EventHandler(this._OnPlaceCatButtonCheckedChanged);
            this._PlaceCatButton.Click += new System.EventHandler(this._OnToolButtonClicked);
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

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.CheckBox _MoveButton;
        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.ToolStripContainer _ToolStripContainer;
        private System.Windows.Forms.ToolStrip _GameTools;
        private System.Windows.Forms.ToolStripButton _BuildOfficeButton;
        private System.Windows.Forms.ToolStripButton _HireWorkerButton;
        private System.Windows.Forms.StatusStrip _StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel _TimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel _MoneyLabel;
        private System.Windows.Forms.ToolStripStatusLabel _EmployeesLabel;
        private System.Windows.Forms.SplitContainer _MainSplitContainer;
        private System.Windows.Forms.ToolStripStatusLabel _PositionLabel;
        private System.Windows.Forms.ToolStripButton _HireITTechButton;
        private System.Windows.Forms.ToolStripButton _HireJanitorButton;
        private System.Windows.Forms.ToolStripButton _PlaceCatButton;
        private System.Windows.Forms.ToolStrip _SystemTools;
        private System.Windows.Forms.ToolStripDropDownButton _LoadButton;
        private System.Windows.Forms.ToolStripMenuItem _LoadGameButton;
        private System.Windows.Forms.ToolStripMenuItem _NewGameButton;
        private System.Windows.Forms.ToolStripMenuItem _QuitApplicationButton;
        private System.Windows.Forms.ToolStripMenuItem _SaveGameButton;
        private System.Windows.Forms.ToolStripButton _HireAccountantButton;
        private System.Windows.Forms.ToolStripButton _BuildBathroomButton;
        private DrawingBoard _DrawingBoard;
    }
}

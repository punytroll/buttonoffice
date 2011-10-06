namespace ButtonOffice
{
    internal class DrawingBoard : System.Windows.Forms.Control
    {
        public DrawingBoard()
        {
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Size = new System.Drawing.Size(100, 100);
            ResumeLayout(false);
        }
    }
}

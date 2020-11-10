using System.Drawing;
using System.Windows.Forms;

namespace ButtonOffice
{
    internal class Canvas : Control
    {
        public Canvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }
        
        private void InitializeComponent()
        {
            SuspendLayout();
            Size = new Size(100, 100);
            ResumeLayout(false);
        }
    }
}

using System;

namespace ButtonOffice
{
    internal class Application
    {
        [STAThread]
        public static void Main(String[] Arguments)
        {
            System.Windows.Forms.Application.Run(new MainWindow());
        }
    }
}

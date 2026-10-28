using System;
using System.Windows.Forms;

namespace Training_Center_Management_System
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain()); 
        }
    }
}

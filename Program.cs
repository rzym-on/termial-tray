using System;
using System.Windows.Forms;
using WindowsTerminalTray;

namespace WindowsTermialTray
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (TerminalTrayIcon ti = new TerminalTrayIcon())
            {
                Application.Run();
            }
        }
    }
}

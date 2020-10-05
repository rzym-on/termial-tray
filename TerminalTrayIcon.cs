using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsTermialTray;
using WindowsTermialTray.Keys;
using WindowsTermialTray.Properties;

namespace WindowsTerminalTray
{
    public class TerminalTrayIcon : IDisposable
    {
        // Apps that hides to Tray
        private List<TrayApp> _appTrayList;

        // Tray vars
        public NotifyIcon Ni;

        public TerminalTrayIcon()
        {
            var contextMenu = new ContextMenu();
            var exitItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };
            exitItem.Click += Close;

            contextMenu.MenuItems.Add(exitItem);

            _appTrayList = new List<TrayApp>();
            _appTrayList.AddRange(new []
            {
                new TrayApp("WindowsTerminal", "wt.exe", ModifierKeys.Alt, Keys.Oemtilde)
                // Add your other tray apps here
                // new TrayApp("ProcessName", "exec.exe", ModifierKeys.Ctrl, Keys.Oemtilde)
            });

            Ni = new NotifyIcon
            {
                Visible = true,
                Icon = Resources.terminal_tray,
                Text = $@"Termial Tray!",
                ContextMenu = contextMenu
            };
        }

        private void Close(object Sender, EventArgs e)
        {
            _appTrayList.ForEach(x => x.Dispose());
            Dispose();
            Ni.Visible = false;
            Application.Exit();
        }

        public void Dispose() { }
    }
}
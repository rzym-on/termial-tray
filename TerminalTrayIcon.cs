using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsTermialTray;
using WindowsTermialTray.Config;
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

            var configPath = Application.UserAppDataPath + "\\config.json";
            var config = ConfigBuilder.Create().AddJsonFile(configPath).AddDefault().Build();

            _appTrayList = new List<TrayApp>();
            foreach (var app in config.Apps)
            {
                _appTrayList.AddRange(new[] {
                    new TrayApp(app.ProcessName, app.ExeFilePath, app.ModifierKeys, app.Keys)
                });
            }

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

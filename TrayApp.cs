using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using WindowsTermialTray.Keys;

namespace WindowsTermialTray
{
    public class TrayApp : IDisposable
    {
        // Invisible window to listen for hotkey and firing a hook
        private readonly KeyboardHook _hook = new KeyboardHook();

        // Hotkeys vars
        private ModifierKeys _mk;
        private System.Windows.Forms.Keys _k;

        // Process vars
        private Process _appProcess = null;
        private readonly string _processName;
        private readonly string _exec;

        // Window vars
        private const int SW_HIDE = 0;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private bool hidden = false;
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        [DllImport("User32")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Create new instance of an app, that will be hided to tray
        /// </summary>
        /// <param name="processName">Proper ProcessName.</param>
        /// <param name="exec">Executable to run if app doesn't exist yet as an process.</param>
        /// <param name="mk">Modifier key to be pressed to fire an event.</param>
        /// <param name="k">Key with modifier that fires the event.</param>
        public TrayApp(string processName, string exec, ModifierKeys mk, System.Windows.Forms.Keys k)
        {
            _processName = processName;
            _exec = exec;
            _mk = mk;
            _k = k;
            _hook.KeyPressed += HotKeyPressed;
            _hook.RegisterHotKey(_mk, _k);
        }

        public void HotKeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (_appProcess == null || _appProcess.HasExited)
            {
                RunApp();
                return;
            }

            if (hidden)
            {
                ShowWindow(_appProcess.MainWindowHandle.ToInt32(), SW_MAXIMIZE);
                SetForegroundWindow(_appProcess.MainWindowHandle);
            }
            else
            {
                ShowWindow(_appProcess.MainWindowHandle.ToInt32(), SW_MINIMIZE);
                ShowWindow(_appProcess.MainWindowHandle.ToInt32(), SW_HIDE);
            }
            hidden = !hidden;
        }

        private void RunApp()
        {
            _appProcess = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.Equals(_processName) && !x.HasExited);

            if (_appProcess != null && _appProcess.MainWindowHandle.ToInt32() == 0)
            {
                _appProcess.Kill();
                _appProcess = null;
            }

            if (_appProcess == null)
            {
                Process process = new Process();
                process.StartInfo.FileName = _exec;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();

                var start = DateTime.Now;
                while (_appProcess == null && DateTime.Now - start < TimeSpan.FromMilliseconds(1000))
                {
                    _appProcess = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.Equals(_processName) && !x.HasExited);
                    Thread.Sleep(100);
                }
            };
        }

        public string GetKeys()
        {
            return $"{_processName}: {_mk.ToString()} + {_k.ToString()}\n";
        }

        public void Dispose()
        {
            if (_appProcess != null)
            {
                ShowWindow(_appProcess.MainWindowHandle.ToInt32(), SW_SHOW);
            }
            _hook.Dispose();
        }
    }
}

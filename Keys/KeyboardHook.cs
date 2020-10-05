using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsTermialTray.Keys
{
    // https://stackoverflow.com/a/3654821/10451282

    public sealed class KeyboardHook : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// This is the widnow to retreive messages
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312; // https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-hotkey

            public Window() { CreateHandle(new CreateParams()); }

            /// <summary>
            /// Overridden method to check if we have key pressed in Message.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    var key = (System.Windows.Forms.Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                this.DestroyHandle();
            }
        }

        private Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                    KeyPressed(this, args);
            };
        }

        /// <summary>
        /// Registers a hot key in the system using RegisterHotKey from users32.dll.
        /// </summary>
        /// <param name="modifier">Modifier key.</param>
        /// <param name="key">Key associated with modifier key.</param>
        public void RegisterHotKey(ModifierKeys modifier, System.Windows.Forms.Keys key)
        {
            _currentId = _currentId + 1;

            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                throw new InvalidOperationException("There was an error during key registration.");
        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        /// <summary>
        /// Unregister all registered hot keys and dispose window.
        /// </summary>
        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        private ModifierKeys _modifier;
        private System.Windows.Forms.Keys _key;

        internal KeyPressedEventArgs(ModifierKeys modifier, System.Windows.Forms.Keys key)
        {
            _modifier = modifier;
            _key = key;
        }

        public ModifierKeys Modifier
        {
            get { return _modifier; }
        }

        public System.Windows.Forms.Keys Key
        {
            get { return _key; }
        }
    }

    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
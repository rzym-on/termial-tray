using WindowsTermialTray.Keys;

namespace WindowsTermialTray.Config
{
    public class App
    {
        public string ProcessName { get; set; }
        public string ExeFilePath { get; set; }
        public ModifierKeys ModifierKeys { get; set; }
        public System.Windows.Forms.Keys Keys { get; set; }
    }

    public class Config
    {
        public App[] Apps { get; set; }
    }

    public class DefaultConfig : Config
    {
        public new App[] Apps { get; } = new App[] {
            new App { ProcessName = "WindowsTerminal", ExeFilePath = "wt.exe", ModifierKeys = ModifierKeys.Alt, Keys = System.Windows.Forms.Keys.Oemtilde}
        };
    }
}

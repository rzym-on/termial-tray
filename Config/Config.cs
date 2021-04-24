using WindowsTermialTray.Keys;

namespace WindowsTermialTray.Config
{
    public class App
    {
        string ProcessName { get; set; }
        string ExeFilePath { get; set; }
        ModifierKeys ModifierKeys { get; set; }
    }

    public class Config
    {
        App[] Apps { get; set; }
    }
}

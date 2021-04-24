using System.Text.Json;

namespace WindowsTermialTray.Config.Provider
{
    class JsonProvider : IProvider
    {
        public Config Deserialize(string jsonString)
        {
            return JsonSerializer.Deserialize<Config>(jsonString);
        }
    }
}
